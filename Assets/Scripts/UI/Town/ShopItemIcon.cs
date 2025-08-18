using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Knight.Town
{
    public class ShopItemIcon : MonoBehaviour
    {
        private bool _isDataInit;
        
        private Image _icon;
        private Button _button;
        private TextMeshProUGUI _description;
        private ShopItem _item;
        
        public void Init(ShopItem newItem)
        {
            if (!_isDataInit)
            {
                _isDataInit = true;
                
                var children = transform
                    .GetComponentsInChildren<Image>(true);

                foreach (var child in children)
                {
                    if (!child.name.Equals(Define.UiObjectName.IMG_ICON))
                        continue;

                    _icon = child;
                    break;
                }

                _description = GetComponentInChildren<TextMeshProUGUI>();
                _button = transform
                    .GetComponentInChildren<Button>(true);
            }

            _item = newItem;
            SetShopItemData();
        }

        private void SetShopItemData()
        {
            if (_icon == null
                || _description == null
                || _button == null
                || _item == null)
                return;

            _icon.sprite = Resources
                .Load<Sprite>($"{Define.IMAGES_PATH}Item/{_item.GetItemName()}");
            
            _description.text = _item.GetItemDescription();
            _button.onClick.AddListener(_item.BuyItem);
        }
    }
}