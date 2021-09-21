using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuneoCrepe
{
    public class IngredientGroup : MonoBehaviour
    {
        [SerializeField] private IngredientType ingredientType;
        [SerializeField] private List<Button> ingredientsButtons;

        public bool Unlock { get; private set; }

        public void Initialize(bool unlock)
        {
            Unlock = unlock;
            
            for (var i = 0; i < ingredientsButtons.Count; i++)
            {
                ingredientsButtons[i].onClick.RemoveAllListeners();
                if (unlock)
                {
                    var index = i + 1;
                    ingredientsButtons[i].onClick
                        .AddListener(() => UIManager.Instance.CrepeController.OnClickIngredient(ingredientType, index));
                }

                ingredientsButtons[i].gameObject.SetActive(unlock);
            }
        }
    }
}
