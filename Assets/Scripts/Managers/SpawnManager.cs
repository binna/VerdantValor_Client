using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Knight
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] 
        private GameObject monsterRoot;
        
        private BaseMonster[] _monsters;
        
        private int _monsterCount;
        
        private static SpawnManager _instance;
        public static SpawnManager GetInstance() => _instance;

        public void DeadMonster()
        {
            _monsterCount--;
        }
        
        #region 이벤트 함수
        private void Awake()
        {
            _instance = this;
            
            var monstersRoot = GameObject.Find(Define.GameObjectName.SPAWN_NAME);

            _monsters = monstersRoot
                .GetComponentsInChildren<BaseMonster>(true);
        }

        private IEnumerator Start()
        {
            while (true)
            {
                var spawnTime = Random.Range(3f, 30f);
                yield return new WaitForSeconds(spawnTime);

                if (_monsterCount >= Define.MONSTER_LIMIT)
                    continue;

                _monsterCount++;

                while (true)
                {
                    var monsterIdx = Random.Range(0, _monsters.Length);
                    var monster = _monsters[monsterIdx];
            
                    if (monster.gameObject.activeSelf)
                        continue;
                    
                    StartCoroutine(monster.SpawnMonster());
                    break;
                }
            }
        }
        #endregion
    }
}