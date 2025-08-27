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
                
        private readonly Item[] _items = new Item[Define.INVNETORY_COUNT];
        private readonly AudioClip _levelUpClip;
        
        private string _id;
        private int _gold;
        private int _exp;
        private int _hp;
        private float _currentHp;
        private float _atkDamage;
        private float _currentAtkDamage;

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

        private Player()
        {
            _levelUpClip = Resources.Load<AudioClip>(Define.LEVEL_UP_PATH);
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
        
        public void SetHUDData()
        {
            _idText.text = _id;
            _levelText.text = $"{GetLevel()}";
            _goldText.text = $"{_gold}";
        }

        public bool FindPlayer(string inputId)
        {
            GetInstance()._id = inputId;
            
            var isUserFind = GameDataManager
                .users.TryGetValue(inputId, out var player);
            
            if (isUserFind)
            {
                for (var i = 0; i < Define.INVNETORY_COUNT; i++)
                {
                    var temp = player.GetItem(i).GetId();
                    if (temp == 0)
                    {
                        _items[i] = new Item();
                        continue;
                    }
                    
                    _items[i] = GameDataManager.items[temp];
                }
                
                _id = player.GetId();
                _gold = player.GetGold();
                _exp = player.GetExp();
                _currentHp = player.GetCurrentHp();
            }
            else
            {
                for (var i = 0; i < Define.INVNETORY_COUNT; i++)
                {
                    _items[i] = new Item();
                }
                
                Debug.LogError($"Player not found Id : {inputId}");
            }
            
            var level = GetLevel();
            var isLevel = GameDataManager
                .playerStats
                .TryGetValue(level, out var playerStats);
            
            if (isLevel)
            {
                _hp = playerStats.GetHp();
                _atkDamage = playerStats.GetAtkDamage();
                InitData();
                
                if (!isUserFind)
                    _currentHp = playerStats.GetHp();
            }
            else
            {
                Debug.LogError($"Player not found Stats : {level}");
            }
            
            return isUserFind && isLevel;
        }

        public Item[] GetItems() => _items;
        
        public string GetId() => _id;
        
        public float GetDamage() => _currentAtkDamage;
        
        public float GetCurrentHp() => _currentHp;
        
        public int GetExp() => _exp;
        
        public bool IsFullHp() => _currentHp >= _hp;
        
        public float GetSpeed() => SPEED;
        
        public float GetJumpPower() => JUMP_POWER;
        
        public int GetGold() => _gold;
        
        public Item GetItemByIdx(int idx) => _items[idx];
        
        public float GetHpRatio()
        {
            if (_currentHp == 0)
                return 0;

            return _currentHp / _hp;
        }
        
        public void TakeDamage(float damage) => _currentHp -= damage;
        
        public void RecoveryHp() => _currentHp += Define.RECOVERY_HP;
        
        public void SetHpBar(Image image) => _hpBar = image;
        
        public void BuyItem(int value) => _gold -= value;

        public void GainExp(int gainExp)
        {
            var nowLevel = GetLevel();
            _exp += gainExp;
            var newLevel = GetLevel();

            if (newLevel > nowLevel)
            {
                SetHUDData();
                SoundManager
                    .GetInstance()
                    .PlaySound(Define.SoundType.Event, _levelUpClip);
                Debug.Log($"[SystemNotice] Level Up {GetLevel()}");
            }
        }
        
        public bool GainItem(Item newItem)
        {
            switch (newItem.GetItemType())
            {
                case Define.ItemType.Gold:
                {
                    newItem.Use(Define.IMMEDIATE_PICKUP);
                    return true;
                }
                case Define.ItemType.PotionAtk:
                case Define.ItemType.PotionHp:
                {
                    for (var i = 0; i < Define.INVNETORY_COUNT; i++)
                    {
                        if (_items[i].GetId() != 0)
                            continue;
                
                        _items[i] = newItem;
                        SetInventoryInit();
                        return true;
                    }
                    break;
                }
            }
            return false;
        }

        public void PrepareTownRespawn()
        {
            _currentHp = 1;
        }

        public void UseItem(Define.ItemType itemType, int value, int index)
        {
            Debug.Log($"[SystemNotice] {itemType} 사용({value})");
            
            switch (itemType)
            {
                case Define.ItemType.PotionHp:
                    _currentHp += value;
                    if (_hp < _currentHp)
                        _currentHp = _hp;
                    _hpBar.fillAmount = GetHpRatio();
                    break;
                case Define.ItemType.PotionAtk:
                    Debug.Log($"[Before] {_currentAtkDamage}");
                    _currentAtkDamage += value;
                    Debug.Log($"[After] {_currentAtkDamage}");
                    break;
                case Define.ItemType.Gold:
                    _gold += value;
                    SetHUDData();
                    break;
            }
            
            if (index != Define.IMMEDIATE_PICKUP)
                _items[index] = new Item();
        }

        public void EndBuff(int value)
        {
            _currentAtkDamage -= value;
            Debug.Log($"[End Buff] {_currentAtkDamage}");
        }

        private int GetLevel()
        {
            var level = 1;
            foreach (var info in GameDataManager.exps)
            {
                level = info.Value;

                if (info.Key > _exp)
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
                if (_items[i].GetId() == 0)
                {
                    _inventoryItemIcons[i].Init();
                    continue;
                }

                _inventoryItemIcons[i].AddItem(_items[i], i);
            }
        }

        private void SetHUD(TMP_Text idText, TMP_Text levelText, TMP_Text goldText)
        {
            _idText = idText;
            _levelText = levelText;
            _goldText = goldText;
        }
        
        private void InitData()
        {
            if (_isDataInit)
                return;
            
            _isDataInit = true;
            _currentAtkDamage = _atkDamage;
        }
    }
}