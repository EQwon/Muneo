using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Muneo
{
    public class IngredientGroup : MonoBehaviour
    {
        [SerializeField] private IngredientType ingredientType;
        [SerializeField] private List<Button> ingredientsButtons;

        public void Initialize(UIManager uiManager, bool unlock)
        {
            for (var i = 0; i < ingredientsButtons.Count; i++)
            {
                ingredientsButtons[i].onClick.RemoveAllListeners();
                if (unlock)
                {
                    var index = i + 1;
                    ingredientsButtons[i].onClick
                        .AddListener(() => uiManager.OnClickIngredient(ingredientType, index));
                }

                ingredientsButtons[i].gameObject.SetActive(unlock);
            }
        }
    }
}
