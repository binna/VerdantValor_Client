using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knight
{
    public class InventoryItemIcon : MonoBehaviour
    {
        private Image _icon;
        private Button _button;
        private TextMeshProUGUI _description;

        private bool _isDataInit;
        private bool _isEmpty = true;
        private int _index;
        private Item _item;

        public void Init()
        {
            if (_isDataInit)
                return;

            _isDataInit = true;
            _isEmpty = true;
            _index = 0;
            _item = null;
            
            var children = GetComponentsInChildren<Image>(true);

            foreach (var child in children)
            {
                if (!child.name.Equals(Define.UiObjectName.IMG_ICON))
                    continue;
                    
                _icon = child;
                break;
            }

            _button = GetComponent<Button>();
            _description = GetComponentInChildren<TextMeshProUGUI>();
            _button.onClick.AddListener(UseItem);
                        
            _icon.sprite = null;
            _icon.color = new Color(1f, 1f, 1f, 0f);
            _description.text = string.Empty;
            
            _button.interactable = !_isEmpty;
            _icon.gameObject.SetActive(!_isEmpty);
        }
        
        public void AddItem(Item newItem, int index)
        {
            Init();
            
            _isEmpty = false;
            _item = newItem;
            _index = index;
            
            if (_icon == null || 
                _button == null || 
                _description == null || 
                _item == null)
                return;
            
            _icon.sprite = 
                Resources.Load<Sprite>(
                    $"{Define.IMAGES_PATH}Item/{_item.GetItemName()}");
            _icon.color = new Color(1f, 1f, 1f, 1f);
            _description.text = _item.GetDescription();
            
            _button.interactable = !_isEmpty;
            _icon.gameObject.SetActive(!_isEmpty);
        }
        
        private void UseItem()
        {
            _isEmpty = true;
            _button.interactable = !_isEmpty;
            _icon.gameObject.SetActive(!_isEmpty);
            
            _item.Use(_index);

            if (_item.GetItemType() == Define.ItemType.PotionAtk)
                SoundManager
                    .GetInstance()
                    .StartCoroutine(RevertAtkBuffAfterRoutine(_item.GetValue(), _item.GetTimer()));

            _index = 0;
            _item = null;
            
            _icon.sprite = null;
            _icon.color = new Color(1f, 1f, 1f, 0f);
            _description.text = string.Empty;
        }
        
        private IEnumerator RevertAtkBuffAfterRoutine(int value, float secTimer)
        {
            yield return new WaitForSeconds(secTimer);
            Player.GetInstance().EndBuff(value);
        }
    }
}