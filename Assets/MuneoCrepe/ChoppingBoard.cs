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
        
        private Dictionary<IngredientType, int> _ingredients;
        private Button _button;
        private Animator _animator;
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
        public bool IsReadyToCombine { get; private set; }

        public void SetCrepeDough(int cone, int fruit, int syrup, int topping)
        {
            _ingredients = new Dictionary<IngredientType, int>
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
            _ingredients = new Dictionary<IngredientType, int>
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
            if (_ingredients[IngredientType.Cone] == 0)
            {
                transform.GetChild(0).gameObject.SetActive(false);

                return;
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }

            // 콘 스프라이트 조절
            coneImage.sprite = coneSpriteList[_ingredients[IngredientType.Cone] - 1];

            // 과일 스프라이트 조절
            if (_ingredients[IngredientType.Fruit] != 0)
            {
                fruitImage.enabled = true;
                fruitImage.sprite = fruitSpriteList[_ingredients[IngredientType.Fruit] - 1];
            }
            else
            {
                fruitImage.enabled = false;
            }
            
            // 시럽 스프라이트 조절
            if (_ingredients[IngredientType.Syrup] != 0)
            {
                syrupImage.enabled = true;
                syrupImage.sprite = syrupSpriteList[_ingredients[IngredientType.Syrup] - 1];
            }
            else
            {
                syrupImage.enabled = false;
            }

            // 토핑 스프라이트 조절
            if (_ingredients[IngredientType.Topping] != 0)
            {
                toppingImage.enabled = true;
                toppingImage.sprite = toppingSpriteList[_ingredients[IngredientType.Topping] - 1];
            }
            else
            {
                toppingImage.enabled = false;
            }
        }
    }
}
