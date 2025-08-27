using System.Collections;
using Knight.Adventure;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Knight
{
    public abstract class BaseMonster : MonoBehaviour
    {
        [SerializeField] protected Image hpBar;
        [SerializeField] private Define.MonsterState state = Define.MonsterState.Idle;
        
        private float _hp;
        private float _traceDistance;
        private float _attackDistance;
       
        private float _currentHp;
        private float _speed;
        private float _attackTime;
        private float _atkDamage;
        
        private float _timer;
        private float _idleTime; 
        private float _patrolTime;
        private bool _isAttack;

        private int _xMin;
        private int _xMax;

        private int _gainExp;
        
        private Transform _playerTransform;
        private Transform _transform;
        
        private Animator _animator;
        private Collider2D _collider2D;
        
        private AudioClip _deathClip;
        
        private float _toMonsterDistance;
        private bool _isTrace;
        private bool _configured;

        protected abstract void Awake();

        public void TakeDamage(float damage)
        {
            _currentHp -= damage;
            hpBar.fillAmount = _currentHp / _hp;
            
            if (_currentHp <= 0)
                Death();
        }

        public IEnumerator SpawnMonster()
        {
            gameObject.transform.parent.gameObject.SetActive(true);
            
            yield return null;
            
            RandomPosition();
            
            yield return null;
            
            _collider2D.enabled = true;
            _currentHp = _hp;
            _isTrace = false;
        }

        #region 이벤트 함수
        private void Update()
        {
            // 두 위치(플레이어 -> 몬스터) 간의 벡터(방향 + 거리 포함)
            var toMonster = _transform.position - _playerTransform.position;
            
            // 거리 추출, 항상 양수
            _toMonsterDistance = toMonster.magnitude;

            // 정규화된 방향 벡터(길이는 1, 방향 정보만 유지)
            var toMonsterDirection = toMonster.normalized;
            
            // 몬스터가 바라보는 방향 계산
            // localScale.x가 양수면 오른쪽, 음수면 왼쪽을 바라보는 것으로 판단
            // Vector3.right는 +X축 방향
            var moveDirection = Vector3.right * _transform.localScale.x;
            
            float dotValue = Vector3.Dot(moveDirection, toMonsterDirection);
            
            // 몬스터 기준, 플레이어가 시야각 안에 있는지 확인
            _isTrace = dotValue < -0.5f;
            
            switch (state)
            {
                case Define.MonsterState.Idle:
                    Idle();
                    break;
                case Define.MonsterState.Patrol:
                    Patrol();
                    break;
                case Define.MonsterState.Trace:
                    Trace();
                    break;
                case Define.MonsterState.Attack:
                    Attack();
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.gameObject.tag)
            {
                case Define.Tag.PLAYER:
                {
                    other
                        .gameObject
                        .GetComponent<KnightControllerKeyboard>()
                        .TakeDamage(_atkDamage, other.GetComponent<BasePlayer>());
                
                    var scaleX = _transform.localScale.x * -1;
                    other.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
                    break;
                }
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Define.Tag.GROUND) 
                || other.gameObject.CompareTag(Define.Tag.PLAYER))
                return;
            
            var scaleX = _transform.localScale.x * -1;
            _transform.localScale = new Vector3(scaleX, 1f, 1f);
        }
        #endregion
        
        protected void Init(
            float hp, float speed, float attackTime, float damage, 
            float traceDistance, float attackDistance,
            int gainExp)
        {
            if (!_configured)
            {
                _playerTransform = 
                    GameObject.FindGameObjectWithTag(Define.Tag.PLAYER).transform;
            
                _animator = GetComponent<Animator>();
                _collider2D = GetComponent<Collider2D>();
                _transform = _animator.transform;
            
                _deathClip = Resources.Load<AudioClip>(Define.MONSTER_DEATH_PATH);
                _configured = true;
            }

            _hp = hp;
            _traceDistance = traceDistance;
            _attackDistance = attackDistance;
            
            _currentHp = _hp;
            _speed = speed;
            _attackTime = attackTime;
            _atkDamage = damage;
            _gainExp = gainExp;

            _currentHp = _hp;
            
            hpBar.fillAmount = _currentHp / _hp;
            
            _collider2D.enabled = false;
            _isTrace = false;
        }

        private void ChangeState(Define.MonsterState newState)
        {
            state = newState;
        }
        
        private void Idle()
        {
            _timer += Time.deltaTime;

            // 정찰
            if (_timer >= _idleTime)
            {
                var scaleX = Random.Range(0, 2) == 1? 1 : -1;
                _transform.localScale = new Vector3(scaleX, 1, 1);
                
                _timer = 0f;
                _patrolTime = Random.Range(1f, 5f);
                _animator.SetBool(Define.AnimatorParameter.isRun, true);
                
                ChangeState(Define.MonsterState.Patrol);
            }
            
            // 추격
            if (_toMonsterDistance <= _traceDistance && _isTrace)
            {
                _timer = 0f;
                _animator.SetBool(Define.AnimatorParameter.isRun, true);
                
                ChangeState(Define.MonsterState.Trace);
            }
        }
        
        private void Patrol()
        {
            var x = _transform.position.x;

            if (x < _xMin)
                _transform.localScale = new Vector3(1, 1, 1);
            
            else if (_xMax < x)
                _transform.localScale = new Vector3(-1, 1, 1);
            
            _transform.position += 
                Vector3.right * _transform.localScale.x * _speed * Time.deltaTime;
            
            _timer += Time.deltaTime;
            
            // 정찰 시간 끝
            if (_timer >= _patrolTime)
            {
                _timer = 0f;
                _idleTime = Random.Range(1f, 5f);
                _animator
                    .SetBool(Define.AnimatorParameter.isRun, false);
                ChangeState(Define.MonsterState.Idle);
                return;
            }
            
            // 추격
            if (_toMonsterDistance <= _traceDistance && _isTrace)
            {
                _timer = 0f;
                ChangeState(Define.MonsterState.Trace);
            }
        }
        
        private void Trace()
        {
            // 몬스터 -> 플레이어 방향
            var toPlayer = (_playerTransform.position - _transform.position).normalized;
            var scaleX = toPlayer.x < 0f ? -1f : 1f;
            
            _transform.position += Vector3.right * scaleX * _speed * Time.deltaTime;
            _transform.localScale = new Vector3(scaleX, 1f, 1f);
            
            if (_toMonsterDistance > _traceDistance)
            {
                _animator.SetBool(Define.AnimatorParameter.isRun, false);
                ChangeState(Define.MonsterState.Idle);
                return;
            }
            
            if (_toMonsterDistance < _attackDistance)
            {
                ChangeState(Define.MonsterState.Attack);
            }
        }
        
        private void Attack()
        {
            if (!_isAttack)
                StartCoroutine(AttackRoutine());
        }
        
        private void Death()
        {
            _animator
                .SetTrigger(Define.AnimatorParameter.death);
            
            SoundManager
                .GetInstance()
                .PlaySound(Define.SoundType.Event, _deathClip);

            DropItem
                .CreateDropItem(_transform.position);
            
            Player
                .GetInstance()
                .GainExp(_gainExp);

            _collider2D.enabled = false;
            gameObject.transform.parent.gameObject.SetActive(false);
            
            SpawnManager.GetInstance().DeadMonster();
        }
        
        private void RandomPosition()
        {
            var idx = Random.Range(0, Define.monsterPosCnt);
            var area = Define.monsterPositions[idx];
            var y = area.yPos;
            
            _xMin = area.xMin;
            _xMax = area.xMax;
            
            var spawnX = Random.Range(_xMin, _xMax);
            _transform.position = new Vector3(spawnX, y, 0f);
        }

        private IEnumerator AttackRoutine()
        {
            _isAttack = true;
            _animator
                .SetTrigger(Define.AnimatorParameter.attack);

            // 트리거 설정 직후에는 Animator의 상태 전환이 아직 적용되지 않았을 수 있음
            // 1프레임 대기 후 상태 정보를 가져오는 것이 안전함
            yield return null;
            
            var currentAnimationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
            
            yield return new WaitForSeconds(currentAnimationLength);
            
            _animator.SetBool(Define.AnimatorParameter.isRun, false);

            // 벡터 연산의 방향성은 끝점 - 시작점 이다
            // 종점(플레이어) - 시작점(몬스터)
            // 몬스터 -> 플레이어 방향
            var toPlayer = (_playerTransform.position - _transform.position).normalized;
            var scaleX = toPlayer.x > 0 ? 1 : -1;
            _transform.localScale = new Vector3(scaleX, 1, 1);

            yield return new WaitForSeconds(_attackTime - 1f);
            
            _isAttack = false;
            _animator
                .SetBool(Define.AnimatorParameter.isRun, true);
            ChangeState(Define.MonsterState.Idle);
        }
    }
}