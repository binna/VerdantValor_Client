using UnityEngine;

namespace Knight
{
    public class DropItem : MonoBehaviour
    {
        private readonly int _count;
        
        private Item _item;

        public static Item RandomItem()
        {
            var idx = Random.Range(0, GameDataManager.GetItemsCount()) + 1;
            return GameDataManager.items[idx];
        }

        public void Init(Item item)
        {
            _item = item;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(Define.Tag.PLAYER))
            {
                if (Player.GetInstance().GainItem(_item))
                {
                    // TODO 소리
                    Debug.Log(other.gameObject.name + " " + _item.GetItemName());
                    Destroy(gameObject);
                }
            }
        }
    }
}