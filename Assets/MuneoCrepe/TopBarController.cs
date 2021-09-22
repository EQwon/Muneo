using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuneoCrepe
{
    public class TopBarController : MonoBehaviour
    {
        [SerializeField] private Text leftAmountText;
        [SerializeField] private Text leftTimeText;
        [SerializeField] private List<GameObject> heartList;

        public void SetLeftAmount(int amount)
        {
            leftAmountText.text = $"{amount:D2}";
        }

        public void SetLife(int count)
        {
            if (count > 5 || count < 0)
            {
                Debug.LogError($"{count}는 하트 갯수로서 알맞지 않은 숫자입니다.");
                return;
            }

            for (var i = 0; i < heartList.Count; i++)
            {
                heartList[i].SetActive(i < count);
            }
        }

        public void SetTimer()
        {
            if (!UIManager.Instance.IsGameStart)
            {
                leftAmountText.text = "";
                return;
            }

            var leftTime = UIManager.Instance.LeftTime;

            if (leftTime >= 10)
            {
                leftTimeText.text = leftTime.ToString("00");
            }
            else if(leftTime >= 0)
            {
                leftTimeText.text = leftTime.ToString("0.0");
            }
            else
            {
                leftTimeText.text = "0.0";
                UIManager.Instance.Ending(false);
            }
        }
    }
}
