using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MuneoCrepe.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace MuneoCrepe
{
    public class CombinedCrepeController : MonoBehaviour
    {
        #region Inspectors

        [SerializeField] private Image coneImage;
        [SerializeField] private List<GameObject> fruitObjects;
        [SerializeField] private Image syrupImage;
        [SerializeField] private List<GameObject> toppingObjects;
        [SerializeField] private Image handleImage;

        [Header("Sprites")]
        [SerializeField] private List<Sprite> coneSpriteList;
        [SerializeField] private List<Sprite> syrupSpriteList;
        [SerializeField] private List<Sprite> handleSpriteList;

        #endregion

        private RectTransform _rectTransform;
        private RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        private bool _giveFinished;

        private void SetCrepe(Dictionary<IngredientType, int> ingredients)
        {
            // 콘 스프라이트 조절
            if (ingredients[IngredientType.Cone] != 0)
            {
                coneImage.sprite = coneSpriteList[ingredients[IngredientType.Cone] - 1];
                handleImage.sprite = handleSpriteList[ingredients[IngredientType.Cone] - 1];
            }
            
            // 과일 스프라이트 조절
            fruitObjects.ForEach(go => go.SetActive(false));
            if (ingredients[IngredientType.Fruit] != 0)
            {
                fruitObjects[ingredients[IngredientType.Fruit] - 1].SetActive(true);
            }
            
            // 시럽 스프라이트 조절
            if (ingredients[IngredientType.Syrup] != 0)
            {
                syrupImage.enabled = true;
                syrupImage.sprite = syrupSpriteList[ingredients[IngredientType.Syrup] - 1];
            }
            else
            {
                syrupImage.enabled = false;
            }

            // 토핑 스프라이트 조절
            toppingObjects.ForEach(go => go.SetActive(false));
            if (ingredients[IngredientType.Topping] != 0)
            {
                toppingObjects[ingredients[IngredientType.Topping] - 1].SetActive(true);
            }
            
            transform.SetParent(UIManager.Instance.CrepeController.transform);
            transform.SetAsLastSibling();
            
            RectTransform.localScale = Vector3.one;
            RectTransform.anchoredPosition = new Vector2(0, -660);
            _giveFinished = false;
            
            gameObject.SetActive(true);
        }

        public async UniTask GiveToMuneo(Dictionary<IngredientType, int> ingredients)
        {
            SetCrepe(ingredients);
            await UniTask.WaitUntil(() => _giveFinished);
        }

        public void MoveToMuneoLayer()
        {
            transform.SetParent(UIManager.Instance.CrepeController.nowMuneo.transform);
        }

        public void MoveToVeryBackLayer()
        {
            transform.SetAsFirstSibling();
        }

        public void GiveFinish()
        {
            _giveFinished = true;
        }
    }
}
