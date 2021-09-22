using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MuneoCrepe
{
    public class Muneo : MonoBehaviour
    {
        #region Inspectors

        [SerializeField] private Image baseBodyImage;
        [SerializeField] private Image baseBodyPattern;
        [SerializeField] private Image reverseBodyImage;
        [SerializeField] private Image reverseBodyPattern;

        [Header("Resources")]
        [SerializeField] private List<Color> baseColorList;
        [SerializeField] private List<Color> reverseColorList;
        [SerializeField] private List<GameObject> hatObjectList;
        [SerializeField] private List<Sprite> patternList;
        [SerializeField] private List<Color> patternColor;
        [SerializeField] private List<GameObject> eyeObjectList;

        #endregion
        
        private Dictionary<MuneoType, int> _characteristics;
        private Animator _animator;
        private bool _isMuneoDisappeared;

        public Animator Animator
        {
            get
            {
                if (_animator == null)
                {
                    _animator = GetComponent<Animator>();
                }

                return _animator;
            }
        }

        public void Initialize((int color, int hat, int dyeing, int eye) characteristics)
        {
            _characteristics = new Dictionary<MuneoType, int>
            {
                {MuneoType.Color, characteristics.color},
                {MuneoType.Hat, characteristics.hat},
                {MuneoType.Dyeing, characteristics.dyeing},
                {MuneoType.Eye, characteristics.eye},
            };

            ShowAsUI();
            _isMuneoDisappeared = false;
        }

        public async UniTask<bool> Reaction(Dictionary<IngredientType, int> ingredients)
        {
            var result = IsFavoriteCrepe(ingredients);
            
            // 성공 : Flip 애니메이션 재생 후 Move Up 애니메이션 재생
            // 실패 : Move Down 애니메이션 재생
            Animator.SetBool("Success", result);
            Animator.SetTrigger("GetCrepe");

            await UniTask.WaitUntil(() => _isMuneoDisappeared);

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

        public void FlipColor()
        {
            baseBodyImage.color = reverseColorList[_characteristics[MuneoType.Color] - 1];
            reverseBodyImage.color = baseColorList[_characteristics[MuneoType.Color] - 1];
        }

        public void DisappearFinished()
        {
            _isMuneoDisappeared = true;
        }

        private void ShowAsUI()
        {
            if (_characteristics[MuneoType.Color] != 0)
            {
                baseBodyImage.color = baseColorList[_characteristics[MuneoType.Color] - 1];
                reverseBodyImage.color = reverseColorList[_characteristics[MuneoType.Color] - 1];
            }
            else
            {
                Debug.LogError("문어는 항상 색이 있어야 합니다!");
            }
            
            hatObjectList.ForEach(go => go.SetActive(false));
            if (_characteristics[MuneoType.Hat] != 0)
            {
                hatObjectList[_characteristics[MuneoType.Hat] - 1].SetActive(true);
            }
            
            if (_characteristics[MuneoType.Dyeing] != 0)
            {
                baseBodyPattern.enabled = true;
                reverseBodyPattern.enabled = true;

                baseBodyPattern.sprite = patternList[_characteristics[MuneoType.Dyeing] - 1];
                baseBodyPattern.color = patternColor[_characteristics[MuneoType.Dyeing] - 1];
                reverseBodyPattern.sprite = patternList[_characteristics[MuneoType.Dyeing] - 1];
                reverseBodyPattern.color = patternColor[_characteristics[MuneoType.Dyeing] - 1];
            }
            else
            {
                baseBodyPattern.enabled = false;
                reverseBodyPattern.enabled = false;
            }
            
            eyeObjectList.ForEach(go => go.SetActive(false));
            eyeObjectList[_characteristics[MuneoType.Eye]].SetActive(true);
        }
    }
}
