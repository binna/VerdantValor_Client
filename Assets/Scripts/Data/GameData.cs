using System;
using System.Collections.Generic;

namespace Knight
{
    [Serializable]
    public class ItemGameData
    {
        public List<Item> items;
    }
    
    [Serializable]
    public class ShopItemGameData
    {
        public List<ShopItem> shopItems;
    }
    
    [Serializable]
    public class ExpGameData
    {
        public List<Exp> exps;
    }
    
    [Serializable]
    public class UserGameData
    {
        public List<User> users;
    }
    
    [Serializable]
    public class PlayerStatGameData
    {
        public List<PlayerStat> playerStats;
    }
}