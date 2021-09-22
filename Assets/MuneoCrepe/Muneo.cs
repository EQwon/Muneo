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

        [SerializeField] private CombinedCrepeController combineCrepe;
        
        [Space]
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

        private Animator _animator;
        private bool _isMuneoDisappeared;
        private bool _readyToGetCrepe;

        private Animator Animator
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

        public bool Ready { get; private set; }
        public Dictionary<MuneoType, int> Characteristics { get; private set; }

        public void Initialize()
        {
            (int color, int hat, int dyeing, int eye) characteristics  = UIManager.Instance.GenerateRandomCharacteristics();
            
            Characteristics = new Dictionary<MuneoType, int>
            {
                {MuneoType.Color, characteristics.color},
                {MuneoType.Hat, characteristics.hat},
                {MuneoType.Dyeing, characteristics.dyeing},
                {MuneoType.Eye, characteristics.eye},
            };

            ShowAsUI();
            _isMuneoDisappeared = false;
            _readyToGetCrepe = false;
            Ready = true;
            
            combineCrepe.gameObject.SetActive(false);
        }

        public async UniTask Reaction(Dictionary<IngredientType, int> ingredients)
        {
            Ready = false;
            var result = IsFavoriteCrepe(ingredients);
            var dayEnd = false;

            dayEnd = result ? UIManager.Instance.CorrectCrepe() : UIManager.Instance.WrongCrepe();

            // 성공 : Flip 애니메이션 재생 후 Move Up 애니메이션 재생
            // 실패 : Move Down 애니메이션 재생
            Animator.SetBool("Success", result);
            Animator.SetTrigger("GetCrepe");

            await UniTask.WaitUntil(() => _readyToGetCrepe);

            combineCrepe.SetCrepe(ingredients);

            await UniTask.WaitUntil(() => _isMuneoDisappeared);

            if (dayEnd) UIManager.Instance.StartNextDay();
            Initialize();
        }

        public bool IsFavoriteCrepe(Dictionary<IngredientType, int> ingredients)
        {
            if (Characteristics[MuneoType.Color] != ingredients[IngredientType.Cone]) return false;
            if (Characteristics[MuneoType.Hat] != ingredients[IngredientType.Fruit]) return false;
            if (Characteristics[MuneoType.Dyeing] != ingredients[IngredientType.Syrup]) return false;
            if (Characteristics[MuneoType.Eye] != ingredients[IngredientType.Topping]) return false;
            
            return true;
        }

        public void FlipColor()
        {
            baseBodyImage.color = reverseColorList[Characteristics[MuneoType.Color] - 1];
            reverseBodyImage.color = baseColorList[Characteristics[MuneoType.Color] - 1];
        }

        public void ReadyToGetCrepe()
        {
            _readyToGetCrepe = true;
        }

        public void DisappearFinished()
        {
            _isMuneoDisappeared = true;
        }

        private void ShowAsUI()
        {
            if (Characteristics[MuneoType.Color] != 0)
            {
                baseBodyImage.color = baseColorList[Characteristics[MuneoType.Color] - 1];
                reverseBodyImage.color = reverseColorList[Characteristics[MuneoType.Color] - 1];
            }
            else
            {
                Debug.LogError("문어는 항상 색이 있어야 합니다!");
            }
            
            hatObjectList.ForEach(go => go.SetActive(false));
            if (Characteristics[MuneoType.Hat] != 0)
            {
                hatObjectList[Characteristics[MuneoType.Hat] - 1].SetActive(true);
            }
            
            if (Characteristics[MuneoType.Dyeing] != 0)
            {
                baseBodyPattern.enabled = true;
                reverseBodyPattern.enabled = true;

                baseBodyPattern.sprite = patternList[Characteristics[MuneoType.Dyeing] - 1];
                baseBodyPattern.color = patternColor[Characteristics[MuneoType.Dyeing] - 1];
                reverseBodyPattern.sprite = patternList[Characteristics[MuneoType.Dyeing] - 1];
                reverseBodyPattern.color = patternColor[Characteristics[MuneoType.Dyeing] - 1];
            }
            else
            {
                baseBodyPattern.enabled = false;
                reverseBodyPattern.enabled = false;
            }
            
            eyeObjectList.ForEach(go => go.SetActive(false));
            eyeObjectList[Characteristics[MuneoType.Eye]].SetActive(true);
        }
    }
}
