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
    public class UserGameData
    {
        public List<Player> users;
    }
}