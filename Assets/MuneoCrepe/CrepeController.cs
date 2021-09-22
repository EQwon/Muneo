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
        [SerializeField] private CombinedCrepeController combinedCrepe;
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

            GenerateRandomMuneo();
            table.InitialSetting();
            
            // 3초 세고
            // 게임 시작
            Loop().Forget();
        }
        
        private void GenerateRandomMuneo()
        {
            var characteristics = UIManager.Instance.GenerateCharacteristics();
            nowMuneo.Initialize(characteristics);
            // nowMuneo.Animator.SetTrigger("Next");
            // combinedCrepe.gameObject.SetActive(false);
        }

        private async UniTask Loop()
        {
            while (true)
            {
                await table.MoveConveyorBelt();

                CanControl = true;

                var task = UniTask.Delay(1000, cancellationToken: TaskUtil.RefreshToken(ref _tokenSource));

                while (task.Status != UniTaskStatus.Succeeded && task.Status != UniTaskStatus.Canceled)
                {
                    await UniTask.WaitForEndOfFrame();
                }

                CanControl = false;

                table.CreateNewCrepe();
            }
        }

        public void GiveToMuneo()
        {
            _tokenSource.Cancel();
        }

        public void ThrowAway()
        {
            _tokenSource.Cancel();
        }
    }
}
