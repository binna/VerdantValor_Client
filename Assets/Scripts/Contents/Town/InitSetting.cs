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

            #region 상점 데이터 UI 세팅
            var shopItemIcons = UIManager
                .GetInstance()
                .FindUIComponentsByName<ShopItemIcon>($"{Define.UiName.Shop}");

            var showItemIconCount = shopItemIcons.Length;
            
            for (var i = 0; i < showItemIconCount; i++)
            {
                if (GameDataManager.GetInstance().shopItems.TryGetValue(i + 1, out var shopItem))
                {
                    shopItemIcons[i].Init(shopItem);
                }
            }
            #endregion
            
            // TODO
            #region 삭제할 것
            // TODO 유저 검색하는 부분 필요
            Player.GetInstance().InitData();
            #endregion
            
            Player.GetInstance().InitHUD();
        }
    }
}