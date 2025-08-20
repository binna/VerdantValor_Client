using System;
using UnityEngine;

namespace Knight
{
    [Serializable]
    public class Exp
    {
        [SerializeField] private int level;
        [SerializeField] private int exp;
        
        public int GetLevel() => level;
        public int GetExp() => exp;
    }
}