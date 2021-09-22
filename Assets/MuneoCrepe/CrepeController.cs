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

        public void SetActive(bool value, bool auto = false)
        {
            
        }
        
        public void GameStart(int day)
        {
            _nowCount = 0;

            nowMuneo.Initialize();
            table.InitialSetting();
            
            // 3초 세고
            // 게임 시작
            Loop().Forget();
        }

        private async UniTask Loop()
        {
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
            }
        }

        public async UniTask GiveToMuneo()
        {
            _tokenSource.Cancel();
            
            await nowMuneo.Reaction(table.NowIngredients);
        }

        public void ThrowAway()
        {
            _tokenSource.Cancel();
        }
    }
}
