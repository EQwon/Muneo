using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Muneo
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<IngredientGroup> ingredientGroups;
        [SerializeField] private ChoppingBoard choppingBoard;

        private int _day = 3;
        private const int MAXIMUM_DAY = 4;
        
        private void Start()
        {
            for (var i = 0; i < ingredientGroups.Count; i++)
            {
                ingredientGroups[i].Initialize(this, i < _day);
            }
            choppingBoard.Initialize();
        }

        public void OnClickIngredient(IngredientType type, int index)
        {
            choppingBoard.SetIngredients(type, index);
        }
    }
}