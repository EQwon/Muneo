using System.Collections.Generic;
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

        [Header("Sprites")]
        [SerializeField] private List<Sprite> coneSpriteList;
        [SerializeField] private List<Sprite> syrupSpriteList;

        #endregion
        
        public void SetCrepe(Dictionary<IngredientType, int> ingredients)
        {
            // 콘 스프라이트 조절
            if (ingredients[IngredientType.Cone] != 0)
            {
                coneImage.enabled = true;
                coneImage.sprite = coneSpriteList[ingredients[IngredientType.Cone] - 1];
            }
            else
            {
                coneImage.enabled = false;
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
        }
    }
}