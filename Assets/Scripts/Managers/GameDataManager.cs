using System.Collections.Generic;
using UnityEngine;

namespace Knight
{
    public class GameDataManager : MonoBehaviour
    {
        #region 게임 데이터
        public Dictionary<int, Item> items = new();
        public Dictionary<int, ShopItem> shopItems = new();
        public Dictionary<string, User> users = new();
        public Dictionary<int, PlayerStat> playerStats = new();
        public Dictionary<int, int> exps = new();
        #endregion
        
        private static bool _isDataInit;
        
        private static GameDataManager _instance;
        public static GameDataManager GetInstance()
        {
            if (_instance == null)
            {
                GameObject gameDataManager
                    = GameObject.Find(Define.GameObjectName.GAME_DATA_MANAGER);

                if (gameDataManager == null)
                {
                    gameDataManager = new GameObject(Define.GameObjectName.GAME_DATA_MANAGER);
                    gameDataManager.AddComponent<GameDataManager>();
                }
                
                DontDestroyOnLoad(gameDataManager);

                _instance = gameDataManager.AddComponent<GameDataManager>();;
                _instance.Init();
            }

            return _instance;
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
        
        private void Init()
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
                    GetInstance().items.Add(data.items[i].GetId(), data.items[i]);
                }
            }

            {
                JsonUtil.Load<ShopItemGameData>("ShopItem", out var data);
                
                var count = data.shopItems.Count;
                for (var i = 0; i < count; i++)
                {
                    GetInstance().shopItems.Add(data.shopItems[i].GetId(), data.shopItems[i]);
                    GetInstance().shopItems[data.shopItems[i].GetId()].Init();
                }
            }
            
            {
                JsonUtil.Load<ExpGameData>("Exp", out var data);
                
                var count = data.exps.Count;
                for (var i = 0; i < count; i++)
                {
                    GetInstance().exps.Add(data.exps[i].GetExp(), data.exps[i].GetLevel());
                }
            }
            
            {
                JsonUtil.Load<UserGameData>("User", out var data);
                
                var count = data.users.Count;
                for (var i = 0; i < count; i++)
                {
                    GetInstance().users.Add(data.users[i].GetId(), data.users[i]);
                }
            }

            {
                JsonUtil.Load<PlayerStatGameData>("playerStat", out var data);
                
                var count = data.playerStats.Count;
                for (var i = 0; i < count; i++)
                {
                    GetInstance().playerStats.Add(data.playerStats[i].GetLevel(), data.playerStats[i]);
                }
            }
        }

        private void SaveUserDataToJson()
        {
            if (GetInstance()
                .users
                .TryGetValue(Player.GetInstance().GetId(), out var userData))
            {
                userData.SetItems(Player.GetInstance().GetItems());
                userData.SetGold(Player.GetInstance().GetGold());
                userData.SetExp(Player.GetInstance().GetExp());
                userData.SetCurrentHp(Player.GetInstance().GetCurrentHp());
            }
            else
            {
                userData = new User();
                userData.SetItems(Player.GetInstance().GetItems());
                userData.SetGold(Player.GetInstance().GetGold());
                userData.SetExp(Player.GetInstance().GetExp());
                userData.SetCurrentHp(Player.GetInstance().GetCurrentHp());
                GetInstance().users.Add(Player.GetInstance().GetId(), userData);
            }

            var json = new UserGameData { users = new List<User>() };

            foreach (var user in GetInstance().users)
            {
                json.users.Add(user.Value);
            }

            JsonUtil.Save("User", json);
        }
    }
}