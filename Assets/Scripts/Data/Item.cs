using System;
using UnityEngine;

namespace Knight
{
    [Serializable]
    public class Item
    {
        [SerializeField] private int id;
        [SerializeField] private Define.ItemType type;
        [SerializeField] private string name; 
        [SerializeField] private int value;
        
        public int GetId() => id;
        
        public string GetItemName() => name;
        
        public string GetDescription() => $"{GetTypeDescription()}({value})";

        public void Use()
        {
            Player.GetInstance().UseItem(type, value);
        }
        
        private string GetTypeDescription()
        {
            return type switch
            {
                Define.ItemType.PotionHp => "HP",
                Define.ItemType.PotionAtk => "공격력",
                _ => ""
            };
        }
    }
}