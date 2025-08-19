using UnityEngine;

namespace Knight.Adventure
{
    public class InitSetting : MonoBehaviour
    {
        private void Start()
        {
            SoundManager
                .GetInstance()
                .PlaySound(
                    Define.SoundType.Bgm, 
                    Resources.Load<AudioClip>(Define.ADVENTURE_BGM_PATH));

            // TODO
            #region 삭제할 것
            // TODO 유저 검색하는 부분 필요
            Player.GetInstance().InitData();
            #endregion
            
            Player.GetInstance().InitHUD();
        }
    }
}