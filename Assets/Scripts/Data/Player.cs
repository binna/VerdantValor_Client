using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knight
{
    [Serializable]
    public class Player
    {
        private bool _isDataInit;
        
        [SerializeField] private Item[] items = new Item[Define.INVNETORY_COUNT];
        [SerializeField] private string id;
        [SerializeField] private int gold;
        [SerializeField] private int exp;
        [SerializeField] private int hp;
        [SerializeField] private float currentHp;
        [SerializeField] private float atkDamage;
        [SerializeField] private float speed;
        [SerializeField] private float jumpPower;
        
        private float _atkDamage;
        
        private InventoryItemIcon[] _inventoryItemIcons;
        private TMP_Text _idText;
        private TMP_Text _levelText;
        private TMP_Text _goldText;
        private Image _hpBar;

        private static Player _instance;
        public static Player GetInstance()
        {
            if (_instance == null)
                _instance = new Player();

            return _instance;
        }

        public void InitData()
        {
            if (_isDataInit)
                return;
            
            _isDataInit = true;
            
            _atkDamage = atkDamage;
        }

        public void InitHUD()
        {
            SetInventoryInit();
            SetHUD(
                UIManager
                    .GetInstance()
                    .FindUIComponentByName<TMP_Text>(
                        $"{Define.UiName.HUD}", Define.UiObjectName.TXT_ID),
                UIManager
                    .GetInstance()
                    .FindUIComponentByName<TMP_Text>(
                        $"{Define.UiName.HUD}", Define.UiObjectName.TXT_LEVEL),
                UIManager
                    .GetInstance()
                    .FindUIComponentByName<TMP_Text>(
                        $"{Define.UiName.HUD}", Define.UiObjectName.TXT_GOLD));
            SetHUDData();
        }

        public float GetDamage() => _atkDamage;
        
        public float GetCurrentHp() => currentHp;
        
        public bool IsFullHp() => currentHp >= hp;
        
        public float GetSpeed() => speed;
        
        public float GetJumpPower() => jumpPower;
        
        public int GetGold() => gold;
        
        public Item GetItemByIdx(int idx) => items[idx];
        
        public float GetHpRatio()
        {
            if (currentHp == 0)
                return 0;

            return currentHp / hp;
        }
        
        public void SetDamage(float damage) => _atkDamage = damage;
        
        public void TakeDamage(float damage) => currentHp -= damage;
        
        public void RecoveryHp() => currentHp += Define.RECOVERY_HP;
        
        public void SetHpBar(Image image) => _hpBar = image;
        
        public void BuyItem(int value) => gold -= value;

        public void PrepareTownRespawn()
        {
            currentHp = 1;
        }

        public void UseItem(Define.ItemType itemType, int value)
        {
            switch (itemType)
            {
                case Define.ItemType.PotionHp:
                    currentHp += value;
                    _hpBar.fillAmount = GetHpRatio();
                    return;
                case Define.ItemType.PotionAtk:
                    _atkDamage += value;
                    return;
                case Define.ItemType.Gold:
                    gold += value;
                    return;
            }
            
            Debug.Log($"hp : {currentHp} / atk : {_atkDamage} / gold : {gold}");
        }
        
        private int GetLevel()
        {
            return (exp / 100) switch
            {
                0 => 1,
                1 => 2,
                2 => 3,
                3 => 4,
                _ => 5
            };
        }

        private void SetInventoryInit()
        {
            _inventoryItemIcons = UIManager
                .GetInstance()
                .FindUIComponentsByName<InventoryItemIcon>($"{Define.UiName.Inventory}");
            
            for (var i = 0; i < Define.INVNETORY_COUNT; i++)
            {
                if (items[i] == null)
                {
                    _inventoryItemIcons[i].Init();
                    continue;
                }

                _inventoryItemIcons[i].AddItem(items[i]);
            }
        }

        private void SetHUD(TMP_Text idText, TMP_Text levelText, TMP_Text goldText)
        {
            _idText = idText;
            _levelText = levelText;
            _goldText = goldText;
        }
        
        private void SetHUDData()
        {
            _idText.text = id;
            _levelText.text = $"{GetLevel()}";
            _goldText.text = $"{gold}";
        }
    }
}