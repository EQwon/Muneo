using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MuneoCrepe.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MuneoCrepe
{
    public class TableController : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> belts;
        [SerializeField] private List<ChoppingBoard> boards;

        private int _currentBoard;
        private int _passedCount;

        private const int BOARD_WIDTH = 900;
        private const int BELT_WIDTH = 1080;

        private ChoppingBoard PrevBoard
        {
            get
            {
                var prevIndex = _currentBoard - 1;
                if (prevIndex < 0) prevIndex = boards.Count - 1;
                return boards[prevIndex];
            }
        }
        private ChoppingBoard CurrentBoard => boards[_currentBoard];
        private ChoppingBoard NextBoard
        {
            get
            {
                var nextIndex = _currentBoard + 1;
                if (nextIndex >= boards.Count) nextIndex = 0;
                return boards[nextIndex];
            }
        }
        
        private RectTransform PrevBelt
        {
            get
            {
                var prevIndex = _currentBoard - 1;
                if (prevIndex < 0) prevIndex = boards.Count - 1;
                return belts[prevIndex];
            }
        }
        private RectTransform CurrentBelt => belts[_currentBoard];
        private RectTransform NextBelt
        {
            get
            {
                var nextIndex = _currentBoard + 1;
                if (nextIndex >= boards.Count) nextIndex = 0;
                return belts[nextIndex];
            }
        }

        public Dictionary<IngredientType, int> NowIngredients => CurrentBoard.Ingredients;
        
        private void Start()
        {
            _currentBoard = 1;
            _passedCount = 0;
        }

        public void InitialSetting()
        {
            PrevBoard.SetCrepeDough(0,0,0,0);
            CurrentBoard.SetCrepeDough(0,0,0,0);
            CreateNewCrepe();
        }

        private void CreateNewCrepe()
        {
            (int, int, int, int) characteristics;
            var rand = Random.Range(0, 100) % ConfigGame.MaximumPassedCount;
            if (rand == 0 || _passedCount == ConfigGame.MaximumPassedCount)
            {
                characteristics = UIManager.Instance.CrepeController.nowMuneo.Characteristics.ConvertToInts();
                _passedCount = 0;
            }
            else
            {
                characteristics = UIManager.Instance.GenerateWrongCharacteristics();
                _passedCount += 1;
            }

            NextBoard.SetCrepeDough(characteristics);
        }

        public async UniTask MoveConveyorBelt()
        {
            var seq1 = DOTween.Sequence();

            seq1.Append(PrevBoard.RectTransform.DoAnchorLocalMoveXBy(BOARD_WIDTH / 2, ConfigGame.BeltCycleDuration / 2).SetEase(Ease.Linear))
                .Join(CurrentBoard.RectTransform.DoAnchorLocalMoveXBy(BOARD_WIDTH / 2, ConfigGame.BeltCycleDuration / 2).SetEase(Ease.Linear))
                .Join(NextBoard.RectTransform.DoAnchorLocalMoveXBy(BOARD_WIDTH / 2, ConfigGame.BeltCycleDuration / 2).SetEase(Ease.Linear))
                .Join(PrevBelt.DoAnchorLocalMoveXBy(BELT_WIDTH / 2, ConfigGame.BeltCycleDuration / 2).SetEase(Ease.Linear))
                .Join(CurrentBelt.DoAnchorLocalMoveXBy(BELT_WIDTH / 2, ConfigGame.BeltCycleDuration / 2).SetEase(Ease.Linear))
                .Join(NextBelt.DoAnchorLocalMoveXBy(BELT_WIDTH / 2, ConfigGame.BeltCycleDuration / 2).SetEase(Ease.Linear));

            await seq1.WaitAsync();

            var nextBoardPos = NextBoard.RectTransform.anchoredPosition;
            PrevBoard.RectTransform.anchoredPosition = nextBoardPos + new Vector2(BOARD_WIDTH, 0);
            
            var nextBeltPos = NextBelt.anchoredPosition;
            PrevBelt.anchoredPosition = nextBeltPos + new Vector2(BELT_WIDTH, 0);
            
            _currentBoard = (_currentBoard + 1) % boards.Count;
            
            CreateNewCrepe();

            var seq2 = DOTween.Sequence();
            seq2.Append(PrevBoard.RectTransform.DoAnchorLocalMoveXBy(BOARD_WIDTH / 2, ConfigGame.BeltCycleDuration / 2).SetEase(Ease.Linear))
                .Join(CurrentBoard.RectTransform.DoAnchorLocalMoveXBy(BOARD_WIDTH / 2, ConfigGame.BeltCycleDuration / 2).SetEase(Ease.Linear))
                .Join(NextBoard.RectTransform.DoAnchorLocalMoveXBy(BOARD_WIDTH / 2, ConfigGame.BeltCycleDuration / 2).SetEase(Ease.Linear))
                .Join(PrevBelt.DoAnchorLocalMoveXBy(BELT_WIDTH / 2, ConfigGame.BeltCycleDuration / 2).SetEase(Ease.Linear))
                .Join(CurrentBelt.DoAnchorLocalMoveXBy(BELT_WIDTH / 2, ConfigGame.BeltCycleDuration / 2).SetEase(Ease.Linear))
                .Join(NextBelt.DoAnchorLocalMoveXBy(BELT_WIDTH / 2, ConfigGame.BeltCycleDuration / 2).SetEase(Ease.Linear));

            await seq2.WaitAsync();
        }
    }
}
