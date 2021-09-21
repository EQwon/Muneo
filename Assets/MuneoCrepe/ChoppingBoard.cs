using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
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

        public bool IsReadyToCombine { get; private set; }

        public void Initialize()
        {
            _ingredients = new Dictionary<IngredientType, int>
            {
                {IngredientType.Cone, 0},
                {IngredientType.Fruit, 0},
                {IngredientType.Syrup, 0},
                {IngredientType.Topping, 0},
            };

            if (_button == null)
            {
                _button = GetComponent<Button>();
            }
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() =>
            {
                UIManager.Instance.CrepeController.OnClickChoppingBoard(_ingredients).Forget();
            });

            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }

            _animator.SetBool("Spread", true);
            IsReadyToCombine = false;

            ShowAsUI();
        }

        public void SetIngredients(IngredientType type, int index)
        {
            if (_ingredients[type] == index)
            {
                Debug.Log($"이미 {type} 타입에 {index}번 재료가 선택되어있습니다.");
                return;
            }

            Debug.Log($"{type} 타입의 {index}번 째 재료를 선택했습니다.");
            _ingredients[type] = index;
            
            ShowAsUI();
        }
        
        private void ShowAsUI()
        {
            // 콘 스프라이트 조절
            if (_ingredients[IngredientType.Cone] != 0)
            {
                coneImage.enabled = true;
                coneImage.sprite = coneSpriteList[_ingredients[IngredientType.Cone] - 1];
            }
            else
            {
                coneImage.enabled = false;
            }
            
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

        /// <summary> Animator에서 Event로 사용 </summary>
        public void ReadyToCombine()
        {
            IsReadyToCombine = true;
        }

        public async UniTask RollUpCrepe()
        {
            _animator.SetBool("Spread", false);
        }
    }
}
