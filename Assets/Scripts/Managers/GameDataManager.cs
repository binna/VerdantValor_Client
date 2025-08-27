using System.Collections.Generic;
using UnityEngine;

namespace Knight
{
    public class GameDataManager : MonoBehaviour
    {
        #region 게임 데이터
        public static Dictionary<int, Item> items { get; } = new();
        public static Dictionary<int, ShopItem> shopItems { get; } = new();
        public static Dictionary<string, User> users { get; }  = new();
        public static Dictionary<int, PlayerStat> playerStats { get; } = new();
        public static Dictionary<int, int> exps { get; } = new();
        #endregion

        private static bool _isDataInit;
        private static int _itemsCount;

        public static int GetItemsCount()
        {
            return _itemsCount;
        }
        
        #region 이벤트 함수
        private void Awake()
        {
            Init();
        }
        
        private void OnApplicationQuit()
        {
            SaveUserDataToJson();
        }
        #endregion
        
        private static void Init()
        {
            if (_isDataInit)
                return;

            _isDataInit = true;

            // [중요] 절대 순서가 틀어지면 안됌
            {
                JsonUtil.Load<ItemGameData>("item", out var data);

                var count = data.items.Count;
                for (var i = 0; i < count; i++)
                {
                    items.Add(data.items[i].GetId(), data.items[i]);
                }
                
                _itemsCount = items.Count;
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
            
            {
                JsonUtil.Load<ExpGameData>("Exp", out var data);
                
                var count = data.exps.Count;
                for (var i = 0; i < count; i++)
                {
                    exps.Add(data.exps[i].GetExp(), data.exps[i].GetLevel());
                }
            }
            
            {
                JsonUtil.Load<UserGameData>("User", out var data);
                
                var count = data.users.Count;
                for (var i = 0; i < count; i++)
                {
                    users.Add(data.users[i].GetId(), data.users[i]);
                }
            }

            {
                JsonUtil.Load<PlayerStatGameData>("playerStat", out var data);
                
                var count = data.playerStats.Count;
                for (var i = 0; i < count; i++)
                {
                    playerStats.Add(data.playerStats[i].GetLevel(), data.playerStats[i]);
                }
            }
        }

        private static void SaveUserDataToJson()
        {
            if (users
                .TryGetValue(Player.GetInstance().GetId(), out var userData))
            {
                userData.SetGold(Player.GetInstance().GetGold());
                userData.SetExp(Player.GetInstance().GetExp());
                userData.SetCurrentHp(Player.GetInstance().GetCurrentHp());
            }
            else
            {
                userData = new User();
                userData.SetId(Player.GetInstance().GetId());
                userData.SetGold(Player.GetInstance().GetGold());
                userData.SetExp(Player.GetInstance().GetExp());
                userData.SetCurrentHp(Player.GetInstance().GetCurrentHp());
                users.Add(Player.GetInstance().GetId(), userData);
            }
            
            var newItems =  Player.GetInstance().GetItems();
            var packItems = new User.Item[Define.INVNETORY_COUNT];
            for (var i = 0; i < Define.INVNETORY_COUNT; i++)
            {
                var temp = newItems[i].GetId();
                packItems[i] = new User.Item();
                packItems[i].SetId(temp);
            }
            userData.SetItems(packItems);

            var json = new UserGameData { users = new List<User>() };

            foreach (var user in users)
            {
                json.users.Add(user.Value);
            }

            JsonUtil.Save("User", json);
        }
    }
}