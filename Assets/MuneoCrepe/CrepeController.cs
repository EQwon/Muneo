using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MuneoCrepe.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MuneoCrepe
{
    public class CrepeController : MonoBehaviour
    {
        #region Inspectors

        [SerializeField] private TableController table;
        public Muneo nowMuneo;

        #endregion

        private CancellationTokenSource _tokenSource;
        private int _nowCount;

        public bool CanControl { get; private set; }
        public TableController TableController => table;

        public void SetActive(bool auto = false)
        {
            nowMuneo.Initialize();
            table.InitialSetting();
            if (auto)
            {
                Loop(true).Forget();
            }
        }
        
        public void GameStart()
        {
            _nowCount = 0;

            nowMuneo.Initialize();
            table.InitialSetting();
            
            // 3초 세고
            // 게임 시작
            Loop().Forget();
        }

        private async UniTask Loop(bool auto = false)
        {
            if (!auto) UIManager.Instance.IsGameStart = true;

            while (true)
            {
                await table.MoveConveyorBelt();

                CanControl = true;

                var task = UniTask.Delay(Mathf.RoundToInt(ConfigGame.InputDuration * 1000f),
                    cancellationToken: TaskUtil.RefreshToken(ref _tokenSource));

                while (task.Status != UniTaskStatus.Succeeded && task.Status != UniTaskStatus.Canceled)
                {
                    await UniTask.WaitForEndOfFrame();
                }

                if (task.Status == UniTaskStatus.Succeeded)
                {
                    CanControl = false;
                }
                else if (task.Status == UniTaskStatus.Canceled)
                {
                    await UniTask.WaitUntil(() => nowMuneo.Ready);
                }

                if (UIManager.Instance.IsGameStart == false)
                {
                    break;
                }
            }
        }

        public async UniTask GiveToMuneo()
        {
            _tokenSource.Cancel();
            CanControl = false;

            await nowMuneo.Reaction(table.NowIngredients);
        }

        public void ThrowAway()
        {
            _tokenSource.Cancel();
            CanControl = false;
        }
    }
}
