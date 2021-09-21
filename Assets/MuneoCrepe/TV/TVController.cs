using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MuneoCrepe.TV
{
    public class TVController : MonoBehaviour
    {
        #region Inspectors

        [SerializeField] private Button backgroundButton;
        [SerializeField] private Image tvImage;
        [SerializeField] private Text subtitleText;
        [SerializeField] private GameObject instruction;

        #endregion

        private static readonly List<int> TVImageCount = new List<int> {4, 3, 3, 3};

        private List<Sprite> _tvImageList;
        private List<string> _subtitleList;
        private int _nowDay;
        private int _nowPage;
        
        public async UniTask SetInactive(bool instant = false)
        {
            if (instant)
            {
                gameObject.SetActive(false);
            }
            else
            {
                // TODO : TV가 사라지는 연출
                gameObject.SetActive(false);
            }
        }

        public void SetDay(int day)
        {
            gameObject.SetActive(true);
            
            backgroundButton.onClick.RemoveAllListeners();
            backgroundButton.onClick.AddListener(NextPage);

            _nowDay = day;
            SetResources(day);
            ShowPage(0).Forget();
        }

        private void NextPage()
        {
            ShowPage(_nowPage + 1).Forget();
        }

        private async UniTask ShowPage(int page)
        {
            if (page == _tvImageList.Count)
            {
                // 끝내고 게임으로 넘어가기!
                await SetInactive();
                UIManager.Instance.CrepeController.GameStart(_nowDay);
                return;
            }

            _nowPage = page;
            tvImage.sprite = _tvImageList[page];
            subtitleText.text = _subtitleList[page];
        }

        private void SetResources(int day)
        {
            var count = TVImageCount[day - 1];

            _tvImageList = new List<Sprite>();
            for (var i = 0; i < count; i++)
            {
                _tvImageList.Add(Resources.Load<Sprite>($"TV/Day{day}_Image_{i + 1}"));
            }

            _subtitleList = TextParser.Text2List(Resources.Load<TextAsset>($"TV/Day{day}_Text"));
        }
    }
}
