using UnityEngine;

namespace Knight.Town
{
    public class InitSetting : MonoBehaviour
    {
        private void Start()
        {
            SoundManager
                .GetInstance()
                .PlaySound(
                    Define.SoundType.Bgm, 
                    Resources.Load<AudioClip>(Define.TOWN_BGM_PATH));

            #region 상점 데이터 세팅
            var shopItemIcons = UIManager
                .GetInstance()
                .FindUIComponentsByName<ShopItemIcon>($"{Define.UiName.Shop}");

            var showItemIconCount = shopItemIcons.Length;
            
            for (var i = 0; i < showItemIconCount; i++)
            {
                if (GameData.shopItems.TryGetValue(i + 1, out var shopItem))
                {
                    shopItemIcons[i].Init(shopItem);
                }
            }
            #endregion
            
            Player.GetInstance().InitHUD();
        }
    }
}