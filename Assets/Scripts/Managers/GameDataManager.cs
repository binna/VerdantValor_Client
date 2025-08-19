using System.Collections.Generic;
using UnityEngine;

namespace Knight
{
    public class GameDataManager : MonoBehaviour
    {
        public Dictionary<int, Item> items = new();
        public Dictionary<int, ShopItem> shopItems = new();
        
        private static bool _isDataInit;
        
        private static GameDataManager _instance;
        public static GameDataManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameDataManager();
                _instance.Init();
            }

            return _instance;
        }

        #region 이벤트 함수
        private void Awake()
        {
            Init();
        }
        #endregion
        
        private void Init()
        {
            if (_isDataInit)
                return;

            _isDataInit = true;

            {
                JsonUtil.Load<ItemGameData>("item", out var data);

                var count = data.items.Count;
                for (var i = 0; i < count; i++)
                {
                    items.Add(data.items[i].GetId(), data.items[i]);
                }
            }

            {
                JsonUtil.Load<ShopItemGameData>("ShopItem", out var data);
                
                var count = data.shopItems.Count;
                for (var i = 0; i < count; i++)
                {
                    shopItems.Add(data.shopItems[i].GetId(), data.shopItems[i]);
                    shopItems[data.shopItems[i].GetId()].Init();
                }
            }
            
            // TODO 경험치 정보
            {
            }
        }
    }
}