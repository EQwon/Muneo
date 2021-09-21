using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace MuneoCrepe
{
    public class ChoppingBoard : MonoBehaviour
    {
        [SerializeField] private Text text;

        private Dictionary<IngredientType, int> _ingredients;

        public void Initialize()
        {
            _ingredients = new Dictionary<IngredientType, int>
            {
                {IngredientType.Cone, 0},
                {IngredientType.Fruit, 0},
                {IngredientType.Syrup, 0},
                {IngredientType.Topping, 0},
            };
            ShowAsString();
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

            ShowAsString();
        }

        // TODO : 이것을 나중에 UI로 바꿔야 함
        private void ShowAsString()
        {
            var sb = new StringBuilder();
            if (_ingredients[IngredientType.Cone] != 0)
            {
                sb.Append($"Cone : {_ingredients[IngredientType.Cone]}");
                sb.AppendLine();
            }
            if (_ingredients[IngredientType.Fruit] != 0)
            {
                sb.Append($"Fruit : {_ingredients[IngredientType.Fruit]}");
                sb.AppendLine();
            }
            if (_ingredients[IngredientType.Syrup] != 0)
            {
                sb.Append($"Syrup : {_ingredients[IngredientType.Syrup]}");
                sb.AppendLine();
            }
            if (_ingredients[IngredientType.Topping] != 0)
            {
                sb.Append($"Topping : {_ingredients[IngredientType.Topping]}");
                sb.AppendLine();
            }

            text.text = sb.ToString();
        }
    }
}
