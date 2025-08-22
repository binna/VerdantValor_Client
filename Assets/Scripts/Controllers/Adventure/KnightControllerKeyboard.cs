using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Knight.Adventure
{
    public class KnightControllerKeyboard : BasePlayer
    {
        [SerializeField] 
        private Image hpBar;
        
        [SerializeField]
        private Vector3 position;       // 마을 이동 위치

        [SerializeField]
        private Vector3 scale;
        
        private readonly Vector2 _crouchOffset = new(0.03912544f, 0.5389824f); 
        private readonly Vector2 _crouchSize = new(0.6767006f, 0.9599333f);
        
        private readonly Vector2 _standOffset = new(0.03912544f, 0.7860222f);
        private readonly Vector2 _standSize = new(0.6767006f, 1.400105f);
        
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private CapsuleCollider2D _collider;

        private Vector3 _inputDir;
        private AudioClip _gameOverClip;

        private bool _isDead;
        private bool _isGround;
        private bool _isAttack;
        private bool _isCombo;
        private bool _isLadder;

        public override void UpdatePosition(Vector3 newPosition, Vector3 newScale)
        {
            _initPosition = newPosition;
            _initScale = newScale;
        }
        
        // 공격 애니메이션에 이벤트로 연결
        public void WaitCombo()
        {
            if (_isCombo)
            {
                Player.GetInstance().SetDamage(5f);
                _animator.SetBool(Define.AnimatorParameter.isCombo, true);
                return;
            }
        
            _isAttack = false;
            _animator.SetBool(Define.AnimatorParameter.isCombo, false);
        }
        
        // 콤보 애니메이션에 이벤트로 연결
        public void EndCombo()
        {
            _isAttack = false;
            _isCombo = false;
            _animator.SetBool(Define.AnimatorParameter.isCombo, false);
        }

        public void TakeDamage(float damage, BasePlayer player)
        {
            if (_isDead)
                return;
            
            if (Player.GetInstance().GetCurrentHp() <= 0f)
            {
                _isDead = true;
                StartCoroutine(Death(player));
                return;
            }
            
            EndCombo();
            Player.GetInstance().TakeDamage(damage);
            hpBar.fillAmount = Player.GetInstance().GetHpRatio();
            _animator.SetTrigger(Define.AnimatorParameter.hit);
        }

        #region 이벤트 함수
        private void Start()
        {
            _isBlocked = false;
            
            _isDead = false;
            _isGround = false;
            _isAttack = false;
            _isCombo = false;
            _isLadder = false;
            
            transform.position = _initPosition;
            transform.localScale = _initScale;
            
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CapsuleCollider2D>();
            
            Player.GetInstance().SetHpBar(hpBar);
            hpBar.fillAmount = Player.GetInstance().GetHpRatio();
            
            _gameOverClip = Resources.Load<AudioClip>(Define.GAMEOVER_PATH);
        }

        private void Update()       // 일반적인 작업
        {
            InputKeyboard();
            Jump();
            Attack();
        }

        private void FixedUpdate()  // 물리적인 작업
        {
            Move();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Define.Tag.GROUND))
            {
                _animator.SetBool(Define.AnimatorParameter.isGround, true);
                _isGround = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Define.Tag.GROUND))
            {
                _animator.SetBool(Define.AnimatorParameter.isGround, false);
                _isGround = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.gameObject.tag)
            {
                case Define.Tag.MONSTER:
                {
                    other
                        .gameObject
                        .GetComponent<BaseMonster>()
                        .TakeDamage(Player.GetInstance().GetDamage());

                    if (_isCombo)
                    {
                        _isAttack = false;
                        _isCombo = false;
                    }

                    var scaleX = transform.localScale.x * -1;
                    other.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
                    break;
                }
                case Define.Tag.LADDER:
                {
                    _isLadder = true;
                    _rigidbody.gravityScale = 0f;
                    _rigidbody.linearVelocity = Vector2.zero;
                    break;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Define.Tag.LADDER))
            {
                _isLadder = false;
                _rigidbody.gravityScale = 2f;
                _rigidbody.linearVelocity = Vector2.zero;
            }
        }
        #endregion

        private void InputKeyboard()
        {
            if (_isDead)
                return;
            
            var horizontal = Input.GetAxisRaw(Define.HORIZONTAL);
            var vertical = Input.GetAxisRaw(Define.VERTICAL);

            _inputDir = new Vector3(horizontal, vertical, 0);
            
            _animator.SetFloat(Define.AnimatorParameter.positionX, _inputDir.x);
            _animator.SetFloat(Define.AnimatorParameter.positionY, _inputDir.y);

            if (vertical < 0)
            {
                _collider.size = _crouchSize;
                _collider.offset = _crouchOffset;
            }
            else
            {
                _collider.size = _standSize;
                _collider.offset = _standOffset;
            }
        }

        private void Move()
        {
            if (_isLadder)
            {
                _rigidbody.position = new Vector2(13.5f, _rigidbody.position.y);
                _rigidbody.linearVelocityY = _inputDir.y * Player.GetInstance().GetSpeed();
            }

            if (_inputDir.x == 0)
                return;
                
            _isLadder = false;
            var scaleX = _inputDir.x > 0 ? 1f : -1f;
            transform.localScale = new Vector3(scaleX, 1, 1);
            
            _rigidbody.linearVelocityX = _inputDir.x * Player.GetInstance().GetSpeed();
        }

        private void Jump()
        {
            if (_isDead)
                return;
            
            if (Input.GetKeyDown(KeyCode.Space) && _isGround)
            {
                EndCombo();
                _animator.SetTrigger(Define.AnimatorParameter.jump);
                _rigidbody.AddForceY(Player.GetInstance().GetJumpPower(), ForceMode2D.Impulse);
            }
        }

        private void Attack()
        {
            if (_isDead)
                return;
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (!_isAttack)
                {
                    _isAttack = true;
                    Player
                        .GetInstance()
                        .SetDamage(3f);
                    _animator.SetTrigger(Define.AnimatorParameter.attack);
                    return;
                }
                
                _isCombo = true;
            }
        }

        private IEnumerator Death(BasePlayer player)
        {
            _animator.SetTrigger(Define.AnimatorParameter.death);
            
            SoundManager
                .GetInstance()
                .StopBGMSound();
            
            SoundManager
                .GetInstance()
                .PlaySound(Define.SoundType.Event, _gameOverClip);

            yield return new WaitForSeconds(1f); 
            
            Player
                .GetInstance()
                .PrepareTownRespawn();
            
            player.UpdatePosition(position, scale);
            SceneManager.LoadScene((int)Define.SceneType.Town);
        }
    }
}