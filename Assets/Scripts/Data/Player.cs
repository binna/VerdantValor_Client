using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knight
{
    public class Player
    {
        private bool _isDataInit;
        
        private const float SPEED = 4.5f;
        private const float JUMP_POWER = 12f;
                
        private Item[] items = new Item[Define.INVNETORY_COUNT];
        private string id;
        private int gold;
        private int exp;
        private int hp;
        private float currentHp;
        private float atkDamage;
        
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

        public bool FindPlayer(string inputId)
        {
            id = inputId;
            
            if (GameDataManager
                .GetInstance()
                .users
                .TryGetValue(inputId, out var player))
            {
                items = player.GetItems();
                id = player.GetId();
                gold = player.GetGold();
                exp = player.GetExp();
                currentHp = player.GetCurrentHp();
            }

            var level = GetLevel();
            if (GameDataManager
                .GetInstance()
                .playerStats
                .TryGetValue(level, out var playerStats))
            {
                hp = playerStats.GetHp();
                atkDamage = playerStats.GetAtkDamage();
                InitData();
                return true;
            }
            
            Debug.LogError($"Player not found Id : {inputId}/ Level : {level}");
            return false;
        }

        public Item[] GetItems() => items;
        
        public string GetId() => id;
        
        public float GetDamage() => _atkDamage;
        
        public float GetCurrentHp() => currentHp;
        
        public int GetExp() => exp;
        
        public bool IsFullHp() => currentHp >= hp;
        
        public float GetSpeed() => SPEED;
        
        public float GetJumpPower() => JUMP_POWER;
        
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
            
            // TODO
            Debug.Log($"hp : {currentHp} / atk : {_atkDamage} / gold : {gold}");
        }
        
        private int GetLevel()
        {
            var level = 1;
            foreach (var info in GameDataManager.GetInstance().exps)
            {
                level = info.Value;

                if (info.Key > exp)
                    break;
            }
            return level;
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
        
        private void InitData()
        {
            if (_isDataInit)
                return;
            
            _isDataInit = true;
            
            _atkDamage = atkDamage;
        }
    }
}