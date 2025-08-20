using System;
using UnityEngine;

namespace Knight
{
    [Serializable]
    public class User
    {
        [SerializeField] private Item[] items = new Item[Define.INVNETORY_COUNT];
        [SerializeField] private string id;
        [SerializeField] private int gold;
        [SerializeField] private int exp;
        [SerializeField] private float currentHp;

        public Item[] GetItems() => items;
        
        public string GetId() => id;
        
        public int GetGold() => gold;
        
        public int GetExp() => exp;
        
        public float GetCurrentHp() => currentHp;
        
        public void SetItems(Item[] newItem) => items = newItem;
        
        public void SetGold(int newGold) => gold = newGold;
        
        public void SetExp(int newExp) => exp = newExp;
        
        public void SetCurrentHp(float newCurrentHp) => currentHp = newCurrentHp;
    }
}