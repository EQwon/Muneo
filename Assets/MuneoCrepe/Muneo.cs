using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MuneoCrepe
{
    public class Muneo : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private CombinedCrepeController combinedCrepe;
        
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

            ShowAsString();
        }

        public async UniTask GiveCrepe(Dictionary<IngredientType, int> ingredients)
        {
            combinedCrepe.SetCrepe(ingredients);
            var result = IsFavoriteCrepe(ingredients);
            
            if (result)
            {
                await BeHappy();
            }
            else
            {
                await StillSad();
            }
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
            text.text = "기쁜 문어가 되었어요~";

            await UniTask.Delay(1000);
        }

        private async UniTask StillSad()
        {
            text.text = "문어는 여전히 슬퍼요 ㅠㅠ";

            await UniTask.Delay(1000);
        }

        // TODO : 이것을 나중에 UI로 바꿔야 함
        private void ShowAsString()
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

            text.text = sb.ToString();
        }
    }
}
