using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MuneoCrepe.Title
{
    public class TitleController : MonoBehaviour
    {
        #region Inspectors

        [SerializeField] private Button startButton;

        #endregion

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
            
            startButton.onClick.RemoveAllListeners();

            if (value)
            {
                startButton.onClick.AddListener(() => OnClickGameStart());
            }
        }

        private void OnClickGameStart()
        {
            gameObject.SetActive(false);
            UIManager.Instance.StartNextDay();
        }
    }
}
