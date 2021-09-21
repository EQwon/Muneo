using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MuneoCrepe.Title
{
    public class TitleController : MonoBehaviour
    {
        #region Inspectors

        [SerializeField] private Image titleImage;
        [SerializeField] private Button startButton;

        #endregion

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
            
            startButton.onClick.RemoveAllListeners();

            if (value)
            {
                startButton.onClick.AddListener(() => OnClickGameStart().Forget());
            }
        }

        private async UniTask OnClickGameStart()
        {
            // 1. 게임 스타트 버튼을 없앤다.
            startButton.gameObject.SetActive(false);
            // 2. 문어가 갈 때까지 기다린다.
            
            // 3. 타이틀을 없애고 시작
            gameObject.SetActive(false);
            UIManager.Instance.StartNextDay();
        }
    }
}
