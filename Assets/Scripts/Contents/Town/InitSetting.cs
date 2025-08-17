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

            // TODO
            #region 삭제할 것
            GameData.Init();
            
            var myItems = new Item[Define.INVNETORY_COUNT];
            myItems[0] = GameData.items[1];
            myItems[1] = GameData.items[1];
            myItems[2] = GameData.items[2];
            myItems[3] = GameData.items[2];
            myItems[4] = GameData.items[2];
            
            Player.GetInstance().InitData(
                myItems,
                "shine94", 1000, 150, 100,
                50.5f, 3f, 4f, 12f);
            #endregion
            
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