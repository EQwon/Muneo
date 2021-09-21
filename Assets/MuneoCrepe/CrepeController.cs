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
        [SerializeField] private CombinedCrepeController combinedCrepe;
        [SerializeField] private Muneo nowMuneo;

        #endregion

        private int _nowCount;
        
        public void SetActive(bool value, bool auto = false)
        {
            
        }
        
        public void GameStart(int day)
        {
            _nowCount = 0;
            for (var i = 0; i < ingredientGroups.Count; i++)
            {
                ingredientGroups[i].Initialize(i < day);
            }
            choppingBoard.Initialize();
            combinedCrepe.gameObject.SetActive(false);
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

            _nowCount += 1;
            
            // 크레페가 말리는 연출
            await choppingBoard.RollUpCrepe();
            await combinedCrepe.GiveToMuneo(ingredients);
            var result = await nowMuneo.Reaction(ingredients);

            if (_nowCount >= UIManager.Instance.TodayGoal)
            {
                // await CloseShop();
                UIManager.Instance.StartNextDay();
                return;
            }

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
