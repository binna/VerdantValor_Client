using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Knight
{
    public class DropItem : MonoBehaviour
    {
        private static bool _configured;
        
        private static AudioClip _itemPickupClip;
        private static Transform _folderTransform;
        
        private Item _item;

        public static void CreateDropItem(Vector3 pos)
        {
            Init();
            var idx = Random.Range(0, GameDataManager.GetItemsCount()) + 1;
            var item = GameDataManager.items[idx];
            SoundManager.GetInstance().StartCoroutine(DropItemRoutine(pos, item));
        }

        private static IEnumerator DropItemRoutine(Vector3 pos, Item item)
        {
            yield return new WaitForSecondsRealtime(0.5f);

            var prefab = Resources.Load<GameObject>(
                $"{Define.PREFABS_PATH}Item/{item.GetItemName()}");

            var newItem = 
                Instantiate(prefab, pos, Quaternion.identity, _folderTransform);
            newItem.name = prefab.name;

            var dropItem = 
                newItem.GetComponent<DropItem>() ?? newItem.AddComponent<DropItem>();
            dropItem._item = item;
        }

        private static void Init()
        {
            if (!_configured)
            {
                var playGame = GameObject.Find(Define.UiObjectName.PLAY_GAME);
                var items = playGame.transform.Find(Define.UiObjectName.ITEMS);
                if (items == null)
                {
                    var newGameObject = new GameObject(Define.UiObjectName.ITEMS);
                    newGameObject.transform.SetParent(playGame.transform, false);
                    items = newGameObject.transform;
                }

                _folderTransform = items;
                _itemPickupClip = Resources.Load<AudioClip>(Define.ITEM_PICKUP_PATH);
                _configured = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(Define.Tag.PLAYER))
            {
                if (Player.GetInstance().GainItem(_item))
                {
                    SoundManager
                        .GetInstance()
                        .PlaySound(Define.SoundType.Event, _itemPickupClip);
                    Destroy(gameObject);
                }
            }
        }
    }
}