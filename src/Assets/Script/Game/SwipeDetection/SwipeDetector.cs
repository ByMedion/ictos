using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.Game.SwipeDetection
{
    public class SwipeDetector : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onSwipeDetected?.Invoke(GetSwipeDirection(eventData.delta));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onSwipeDetected?.Invoke(SwipeDirection.Click);
        }

        public event Action<SwipeDirection> onSwipeDetected;

        private static SwipeDirection GetSwipeDirection(Vector2 vector)
        {
            float angle;
            if (vector.x < 0)
                angle = 360 - Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg * -1;
            else
                angle = Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;

            return (SwipeDirection) (Mathf.RoundToInt(angle / 45) * 45);
        }
    }
}