using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Knight.Intro
{
    public class StartButton : MonoBehaviour
    {
        private TMP_InputField _idInputField;
        private bool _isFirst;

        #region 이벤트 함수
        private void Start()
        {
            GetComponent<Button>()
                .onClick
                .AddListener(OnClickStartButton);
           
            _idInputField = gameObject.transform.parent
                .Find("Img_Background/Input_Id")
                .GetComponentInChildren<TMP_InputField>(true);
        }
        #endregion

        private void OnClickStartButton()
        {
            if (string.IsNullOrEmpty(_idInputField.text))
            {
                UIManager
                    .GetInstance()
                    .ShowAlarm("아이디가 입력되지 않았습니다.<br>아이디를 입력해주세요.");
                return;
            }
            
            GetComponent<Button>().interactable = false;

            var id = _idInputField.text;
            Player.GetInstance().FindPlayer(id);
            
            SceneManager.LoadScene((int)Define.SceneType.Town);
        }
    }
}