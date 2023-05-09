using System;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.Logic
{
    /// <summary>
    /// Расширенный скролл рект с ивентами на изменение положения
    /// </summary>
    public class ScrollRectWihExtensionEvents : ScrollRect
    {
        private ScrollRectEvent _onNormalizedValueChanged = new ScrollRectEvent();
        
        public ScrollRectEvent onNormalizedValueChanged => _onNormalizedValueChanged;

        private Vector2 normalizedValues;
        private Vector2 contentPrevPos;

        public Vector2 fakeVelocity = Vector2.zero;

        protected override void Start()
        {
            base.Start();

            fakeVelocity = Vector2.zero;
            normalizedValues = new Vector2(horizontalNormalizedPosition, verticalNormalizedPosition);
            contentPrevPos = content.anchoredPosition;
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            
            if (Application.isEditor && !Application.isPlaying) return;
            
            var anchoredPosition = content.anchoredPosition;
            
            if(Math.Abs(normalizedValues[0] - horizontalNormalizedPosition) > 0.01f || 
               Math.Abs(normalizedValues[1] - verticalNormalizedPosition) > 0.01f)
                _onNormalizedValueChanged?.Invoke(new Vector2(horizontalNormalizedPosition, verticalNormalizedPosition));
            
            normalizedValues = new Vector2(horizontalNormalizedPosition, verticalNormalizedPosition);
            
            Vector3 newVelocity = (anchoredPosition - contentPrevPos) / Time.deltaTime;
            contentPrevPos = anchoredPosition;
            
            fakeVelocity = Vector3.Lerp(fakeVelocity, newVelocity, Time.deltaTime * 10);
        }
    }
}