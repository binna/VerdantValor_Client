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
        [SerializeField] private float timer;
        
        public int GetId() => id;
        
        public Define.ItemType GetItemType() => type;
        
        public string GetItemName() => name;
        
        public int GetValue() => value;
        
        public float GetTimer() => timer;
        
        public string GetDescription() => $"{GetTypeDescription()}({value})";

        public void Use(int index)
        {
            Player.GetInstance().UseItem(type, value, index);
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