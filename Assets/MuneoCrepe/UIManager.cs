using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MuneoCrepe.Extensions;
using MuneoCrepe.Sound;
using MuneoCrepe.Title;
using MuneoCrepe.TV;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MuneoCrepe
{
    public class UIManager : MonoBehaviour
    {
        #region Inspectors

        [Header("Controllers")]
        [SerializeField] private TitleController titleController;
        [SerializeField] private TVController tvController;
        [SerializeField] private CrepeController crepeController;
        [SerializeField] private TopBarController topBarController;

        #endregion

        private static UIManager _instance;
        public static UIManager Instance => _instance;

        public CrepeController CrepeController => crepeController;

        public bool IsGameStart
        {
            get => _isGameStart;
            set
            {
                _isGameStart = value;
                if (value)
                {
                    _gameStartTime = Time.time;
                    topBarController.SetLeftAmount(LeftAmount);
                }
                else
                {
                    _gameStartTime = 0f;
                }
            }
        }
        private int LeftAmount => ConfigGame.TargetAmountList[_day - 1] - _correctAmount;
        public float LeftTime => ConfigGame.TimeLimitList[_day - 1] - (Time.time - _gameStartTime);

        private bool IsHatOpen => _day >= 2;
        private bool IsDyeingOpen => _day >= 3;
        private bool IsEyeOpen => _day >= 4;
        
        private bool _isGameStart;
        private float _gameStartTime;
        private int _correctAmount;
        private int _life;
        private int _day;

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

            IsGameStart = false;

            Initialize();
        }

        public void Initialize()
        {
            SoundManager.Instance.PlayBGM();
            
            titleController.SetActive(true);
            tvController.SetInactive(true).Forget();
            crepeController.SetActive(true);

            _day = 0;
            _life = ConfigGame.MaximumLife;
        }

        private void Update()
        {
            topBarController.SetTimer();
        }

        public void StartNextDay()
        {
            IsGameStart = false;
            
            if (_life <= 0)
            {
                Ending(false);
                return;
            }

            _day += 1;

            if (_day >= 5)
            {
                Ending(true);
                return;
            }

            _correctAmount = 0;

            tvController.SetDay(_day);
            topBarController.SetLife(_life);
        }

        public void Ending(bool isGoodEnding)
        {
            IsGameStart = false;

            tvController.SetEnding(isGoodEnding);
        }

        public bool CorrectCrepe()
        {
            _correctAmount += 1;
            topBarController.SetLeftAmount(LeftAmount);
            SoundManager.Instance.PlaySE(true);

            return LeftAmount == 0;
        }

        public bool WrongCrepe()
        {
            _life -= 1;
            topBarController.SetLife(_life);
            SoundManager.Instance.PlaySE(false);

            return _life == 0;
        }

        public (int t1, int t2, int t3, int t4) GenerateWrongCharacteristics(int depth = 0)
        {
            while (true)
            {
                var color = Random.Range(1, 4);
                var hat = IsHatOpen ? Random.Range(1, 4) : 0;
                var dyeing = IsDyeingOpen ? Random.Range(1, 4) : 0;
                var eye = IsEyeOpen ? Random.Range(1, 4) : 0;

                var characteristics = (color, hat, dyeing, eye);

                if (CrepeController.nowMuneo.Characteristics.ConvertToInts() == characteristics || CrepeController.TableController.NowIngredients.ConvertToInts() == characteristics)
                {
                    if (depth >= 100) return characteristics;
                    depth += 1;
                }
                else
                {
                    return characteristics;
                }
            }
        }
        
        public (int t1, int t2, int t3, int t4) GenerateRandomCharacteristics()
        {
            var color = Random.Range(1, 4);
            var hat = IsHatOpen ? Random.Range(1, 4) : 0;
            var dyeing = IsDyeingOpen ? Random.Range(1, 4) : 0;
            var eye = IsEyeOpen ? Random.Range(1, 4) : 0;

            return (color, hat, dyeing, eye);
        }
    }
}