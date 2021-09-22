using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuneoCrepe
{
    public class TopBarController : MonoBehaviour
    {
        [SerializeField] private Text dayText;
        [SerializeField] private List<GameObject> heartList;

        public void SetDay(int day)
        {
            dayText.text = $"DAY {day:D2}";
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
    }
}
