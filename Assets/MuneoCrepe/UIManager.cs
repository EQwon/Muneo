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

        #endregion

        private static UIManager _instance;
        public static UIManager Instance => _instance;

        public CrepeController CrepeController => crepeController;

        private const int MAXIMUM_DAY = 4;
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

            _day = 1;
        }

        public void StartNextDay()
        {
            _day += 1;
            tvController.SetDay(_day);
        }
    }
}