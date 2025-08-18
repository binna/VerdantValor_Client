using System.Collections;
using TMPro;
using UnityEngine;

namespace Knight.Town
{
    public class DialogTyper : MonoBehaviour
    {
        private TextMeshProUGUI _messageUI;
        
        private string _fullText;

        #region 이벤트 함수
        private void Awake()
        {
            var children = transform
                .GetComponentsInChildren<TextMeshProUGUI>(true);

            foreach (var child in children)
            {
                if (!child.name.Equals(Define.UiObjectName.TXT_MESSAGE))
                    continue;
                
                _messageUI = child;
                break;
            }
            
            if (_messageUI != null)
                _fullText = _messageUI.text;
        }

        private void OnEnable()
        {
            _messageUI.text = string.Empty;

            StartCoroutine(TypeTextRoutine());
        }
        #endregion
        
        private IEnumerator TypeTextRoutine()
        {
            var textLength = _fullText.Length;

            for (var i = 0; i < textLength; i++)
            {
                _messageUI.text += _fullText[i];
                yield return new WaitForSeconds(Define.DIALOG_TYPING_SPEED);
            }
        }
    }
}