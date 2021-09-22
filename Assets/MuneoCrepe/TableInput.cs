using System;
using UnityEngine;

namespace MuneoCrepe
{
    public class TableInput : MonoBehaviour
    {
        [SerializeField] private CrepeController crepeController;
        
        private Vector2 _touchBeganPos;
        private Vector2 _touchEndedPos;
        private const float SWIPE_SENSITIVITY = 50f;

        private void Update()
        {
            if (crepeController.CanControl == false) return;
            
            DetectKey();
            DetectSwipe();
        }

        private void DetectSwipe()
        {
            if (Input.touchCount <= 0) return;
            var touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                _touchBeganPos = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                _touchEndedPos = touch.position;
                var touchDiff = _touchEndedPos - _touchBeganPos;

                //x 이동거리나 y 이동거리가 민감도보다 작다면 return
                if (!(Mathf.Abs(touchDiff.y) > SWIPE_SENSITIVITY) &&
                    !(Mathf.Abs(touchDiff.x) > SWIPE_SENSITIVITY)) return;
                
                if (touchDiff.y > 0 && Mathf.Abs(touchDiff.y) > Mathf.Abs(touchDiff.x))
                {
                    Debug.Log("up");
                    crepeController.GiveToMuneo();
                }
                else if (touchDiff.y < 0 && Mathf.Abs(touchDiff.y) > Mathf.Abs(touchDiff.x))
                {
                    Debug.Log("down");
                    crepeController.ThrowAway();
                }
            }
        }

        private void DetectKey()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("up");
                crepeController.GiveToMuneo();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log("down");
                crepeController.ThrowAway();
            }
        }
    }
}
