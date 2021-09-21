using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MuneoCrepe
{
    public class Muneo : MonoBehaviour
    {
        private Dictionary<MuneoType, int> _characteristics;

        public void Initialize(int colorIndex = 0, int hatIndex = 0, int dyeingIndex = 0, int eyeIndex = 0)
        {
            _characteristics = new Dictionary<MuneoType, int>
            {
                {MuneoType.Color, colorIndex},
                {MuneoType.Hat, hatIndex},
                {MuneoType.Dyeing, dyeingIndex},
                {MuneoType.Eye, eyeIndex},
            };
        }

        public async UniTask<bool> Reaction(Dictionary<IngredientType, int> ingredients)
        {
            var result = IsFavoriteCrepe(ingredients);
            
            if (result)
            {
                await BeHappy();
            }
            else
            {
                await StillSad();
            }

            return result;
        }

        private bool IsFavoriteCrepe(Dictionary<IngredientType, int> ingredients)
        {
            if (_characteristics[MuneoType.Color] != ingredients[IngredientType.Cone]) return false;
            if (_characteristics[MuneoType.Hat] != ingredients[IngredientType.Fruit]) return false;
            if (_characteristics[MuneoType.Dyeing] != ingredients[IngredientType.Syrup]) return false;
            if (_characteristics[MuneoType.Eye] != ingredients[IngredientType.Topping]) return false;
            
            return true;
        }

        private async UniTask BeHappy()
        {
            // 뒤집히고
            // 슬슬 올라감

            await UniTask.Delay(1000);
        }

        private async UniTask StillSad()
        {
            await UniTask.Delay(1000);
        }

        private void ShowAsUI()
        {
            var sb = new StringBuilder();
            if (_characteristics[MuneoType.Color] != 0)
            {
                sb.Append($"Color : {_characteristics[MuneoType.Color]}");
                sb.AppendLine();
            }
            if (_characteristics[MuneoType.Hat] != 0)
            {
                sb.Append($"Hat : {_characteristics[MuneoType.Hat]}");
                sb.AppendLine();
            }
            if (_characteristics[MuneoType.Dyeing] != 0)
            {
                sb.Append($"Dyeing : {_characteristics[MuneoType.Dyeing]}");
                sb.AppendLine();
            }
            if (_characteristics[MuneoType.Eye] != 0)
            {
                sb.Append($"Eye : {_characteristics[MuneoType.Eye]}");
                sb.AppendLine();
            }
        }
    }
}
