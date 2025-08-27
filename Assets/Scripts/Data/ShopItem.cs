using System;
using UnityEngine;

namespace Knight
{
    [Serializable]
    public class ShopItem
    {
        [SerializeField] private int id;
        [SerializeField] private string title;
        [SerializeField] private int price;
        [SerializeField] private int itemId;
        
        private Item _item;

        public void Init()
        {
            _item = GameDataManager.items[itemId];
        }

        public int GetId() => id;
        
        public string GetItemName() => _item.GetItemName();
        
        public string GetItemDescription() => $"{title}<br>{price}원";
        
        public int GetPrice() => price;

        public void BuyItem()
        {
            if (Player.GetInstance().GetGold() >= price)
            {
                if (Player.GetInstance().GainItem(_item))
                {
                    Player.GetInstance().BuyItem(price);
                    UIManager
                        .GetInstance()
                        .ShowAlarm("구매에 성공했습니다.");
                }
            }
            else
            {
                UIManager
                    .GetInstance()
                    .ShowAlarm("구매에 실패했습니다.<br>돈이 부족합니다.");
            }
            Player.GetInstance().SetHUDData();
        }
    }
}