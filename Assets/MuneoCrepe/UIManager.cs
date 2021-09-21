using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MuneoCrepe
{
    public class UIManager : MonoBehaviour
    {
        #region Inspectors

        [SerializeField] private List<IngredientGroup> ingredientGroups;
        [SerializeField] private ChoppingBoard choppingBoard;
        [SerializeField] private Muneo nowMuneo;

        #endregion

        private static UIManager _instance;
        public static UIManager Instance => _instance;

        private int _day = 3;
        private const int MAXIMUM_DAY = 4;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            for (var i = 0; i < ingredientGroups.Count; i++)
            {
                ingredientGroups[i].Initialize(i < _day);
            }
            choppingBoard.Initialize();
            GenerateRandomMuneo();
        }

        public void OnClickIngredient(IngredientType type, int index)
        {
            choppingBoard.SetIngredients(type, index);
        }

        public async UniTask OnClickChoppingBoard(Dictionary<IngredientType, int> ingredients)
        {
            var result = nowMuneo.IsFavoriteCrepe(ingredients);
            
            if (result)
            {
                await nowMuneo.BeHappy();
            }
            else
            {
                await nowMuneo.StillSad();
            }
            
            // 도마 초기화
            choppingBoard.Initialize();
            // 문어 초기화
            GenerateRandomMuneo();
        }

        private void GenerateRandomMuneo()
        {
            var color = Random.Range(1, 4);
            var hat = _day >= 2 ? Random.Range(1, 4) : 0;
            var dyeing = _day >= 3 ? Random.Range(1, 4) : 0;
            var eye = _day >= 4 ? Random.Range(1, 4) : 0;
            nowMuneo.Initialize(color, hat, dyeing, eye);
        }
    }
}