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
            Player
                .GetInstance()
                .InitHUD();
        }
    }
}