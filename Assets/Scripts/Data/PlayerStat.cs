using System;
using UnityEngine;

namespace Knight
{
    [Serializable]
    public class PlayerStat
    {
        [SerializeField] private int level;
        [SerializeField] private int hp;
        [SerializeField] private int atkDamage;

        public int GetLevel() => level;
        
        public int GetHp() => hp;
        
        public int GetAtkDamage() => atkDamage;
    }
}