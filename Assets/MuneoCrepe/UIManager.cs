using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
        public int TodayGoal => _goalCounts[_day];

        private const int MAXIMUM_DAY = 4;
        private const int MAXIMUM_LIFE = 5;
        private readonly List<int> _goalCounts = new List<int> {0, 5, 6, 6, 6};

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
        }

        private void Start()
        {
            titleController.SetActive(true);
            tvController.SetInactive(true).Forget();
            crepeController.SetActive(true, true);

            _day = 0;
            _life = MAXIMUM_LIFE;
        }

        public void StartNextDay()
        {
            _day += 1;
            tvController.SetDay(_day);
            topBarController.SetDay(_day);
            topBarController.SetLife(_life);
        }

        public bool WrongCrepe()
        {
            _life -= 1;
            topBarController.SetLife(_life);

            if (_life == 0)
            {
                // Fail();
                return true;
            }

            return false;
        }
    }
}