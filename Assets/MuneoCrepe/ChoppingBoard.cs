using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MuneoCrepe
{
    public class ChoppingBoard : MonoBehaviour
    {
        #region Inspectors
        
        [SerializeField] private Image coneImage;
        [SerializeField] private Image fruitImage;
        [SerializeField] private Image syrupImage;
        [SerializeField] private Image toppingImage;

        [Header("Resources")]
        [SerializeField] private List<Sprite> coneSpriteList;
        [SerializeField] private List<Sprite> fruitSpriteList;
        [SerializeField] private List<Sprite> syrupSpriteList;
        [SerializeField] private List<Sprite> toppingSpriteList;

        #endregion

        private RectTransform _rectTransform;

        public RectTransform RectTransform
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
        public Dictionary<IngredientType, int> Ingredients { get; private set; }

        public void SetCrepeDough(int cone, int fruit, int syrup, int topping)
        {
            Ingredients = new Dictionary<IngredientType, int>
            {
                {IngredientType.Cone, cone},
                {IngredientType.Fruit, fruit},
                {IngredientType.Syrup, syrup},
                {IngredientType.Topping, topping},
            };

            ShowAsUI();
        }

        public void SetCrepeDough((int cone, int fruit, int syrup, int topping) characteristics)
        {
            Ingredients = new Dictionary<IngredientType, int>
            {
                {IngredientType.Cone, characteristics.cone},
                {IngredientType.Fruit, characteristics.fruit},
                {IngredientType.Syrup, characteristics.syrup},
                {IngredientType.Topping, characteristics.topping},
            };

            ShowAsUI();
        }

        private void ShowAsUI()
        {
            if (Ingredients[IngredientType.Cone] == 0)
            {
                transform.GetChild(0).gameObject.SetActive(false);

                return;
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }

            // 콘 스프라이트 조절
            coneImage.sprite = coneSpriteList[Ingredients[IngredientType.Cone] - 1];

            // 과일 스프라이트 조절
            if (Ingredients[IngredientType.Fruit] != 0)
            {
                fruitImage.enabled = true;
                fruitImage.sprite = fruitSpriteList[Ingredients[IngredientType.Fruit] - 1];
                fruitImage.SetNativeSize();
            }
            else
            {
                fruitImage.enabled = false;
            }
            
            // 시럽 스프라이트 조절
            if (Ingredients[IngredientType.Syrup] != 0)
            {
                syrupImage.enabled = true;
                syrupImage.sprite = syrupSpriteList[Ingredients[IngredientType.Syrup] - 1];
            }
            else
            {
                syrupImage.enabled = false;
            }

            // 토핑 스프라이트 조절
            if (Ingredients[IngredientType.Topping] != 0)
            {
                toppingImage.enabled = true;
                toppingImage.sprite = toppingSpriteList[Ingredients[IngredientType.Topping] - 1];
                toppingImage.SetNativeSize();
            }
            else
            {
                toppingImage.enabled = false;
            }
        }
    }
}
