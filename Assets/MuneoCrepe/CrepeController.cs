using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MuneoCrepe
{
    public class CrepeController : MonoBehaviour
    {
        #region Inspectors

        [SerializeField] private List<IngredientGroup> ingredientGroups;
        [SerializeField] private ChoppingBoard choppingBoard;
        [SerializeField] private Muneo nowMuneo;

        #endregion

        public void SetActive(bool value, bool auto = false)
        {
            
        }
        
        public void GameStart(int day)
        {
            for (var i = 0; i < ingredientGroups.Count; i++)
            {
                ingredientGroups[i].Initialize(i < day);
            }
            choppingBoard.Initialize();
            GenerateRandomMuneo();
        }

        public void OnClickIngredient(IngredientType type, int index)
        {
            if (!choppingBoard.IsReadyToCombine) return;
            
            choppingBoard.SetIngredients(type, index);
        }

        public async UniTask OnClickChoppingBoard(Dictionary<IngredientType, int> ingredients)
        {
            if (!choppingBoard.IsReadyToCombine) return;

            await nowMuneo.GiveCrepe(ingredients);

            // 도마 초기화
            choppingBoard.Initialize();
            // 문어 초기화
            GenerateRandomMuneo();
        }

        private void GenerateRandomMuneo()
        {
            var color = Random.Range(1, 4);
            var hat = ingredientGroups[1].Unlock ? Random.Range(1, 4) : 0;
            var dyeing = ingredientGroups[2].Unlock ? Random.Range(1, 4) : 0;
            var eye = ingredientGroups[3].Unlock ? Random.Range(1, 4) : 0;
            nowMuneo.Initialize(color, hat, dyeing, eye);
        }
    }
}
