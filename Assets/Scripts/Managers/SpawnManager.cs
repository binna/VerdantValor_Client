using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Knight
{
    public class SpawnManager : MonoBehaviour
    {
        private readonly List<GameObject> _monsters = new();
        
        private Transform _monstersFolderTransform;
        
        private int _totalCount;
        private int _nowCount;
        
        private static SpawnManager _instance;
        public static SpawnManager GetInstance() => _instance;

        public void DeadMonster()
        {
            _nowCount--;
        }
        
        #region 이벤트 함수
        private void Awake()
        {
            _instance = this;
            
            var playGame = GameObject.Find(Define.UiObjectName.PLAY_GAME);
            var items = playGame.transform.Find(Define.UiObjectName.MONSTERS);
            if (items == null)
            {
                var newGameObject = new GameObject(Define.UiObjectName.MONSTERS);
                newGameObject.transform.SetParent(playGame.transform, false);
                items = newGameObject.transform;
            }
                
            _monstersFolderTransform = items;

            foreach (var type in Define.monsterType)
            {
                var prefab = Resources.Load<GameObject>(
                    $"{Define.PREFABS_PATH}Monster/{type}");
                var count = Random.Range(Define.MONSTER_LIMIT / 2, Define.MONSTER_LIMIT);
                
                for (var i = 0; i < count; i++)
                {
                    var newMonster =
                        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, _monstersFolderTransform);

                    newMonster.name = prefab.name;
                    _monsters.Add(newMonster);
                }
            }
            
            _totalCount = _monsters.Count;
        }
        
// ReSharper disable IteratorNeverReturns
#pragma warning disable CS0162
        private IEnumerator Start()
        {
            while (true)
            {
                var spawnTime = Random.Range(3f, 15f);
                
                yield return new WaitForSeconds(spawnTime);
            
                if (_nowCount >= _totalCount)
                    continue;
            
                _nowCount++;
                
                while (true)
                {
                    var monsterIdx = Random.Range(0, _totalCount);
                    var monster = _monsters[monsterIdx];
                
                    if (monster.gameObject.activeSelf)
                        continue;
                    
                    StartCoroutine(
                        monster
                            .GetComponentInChildren<BaseMonster>(true)
                            .SpawnMonster()); 
                    break;
                }
            }
        }
#pragma warning restore CS0162
// ReSharper restore IteratorNeverReturns
        #endregion
    }
}