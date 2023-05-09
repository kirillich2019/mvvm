using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UISystem.Logic
{
    /// <summary>
    /// Хендлер нажатий на поинтер
    /// </summary>
    public class PointerDownUpHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Action<PointerEventData> onPointerDownAction;
        private Action<PointerEventData> onPointerUpAction;

        public void Init(Action<PointerEventData> onPointerDownAction, Action<PointerEventData> onPointerUpAction)
        {
            this.onPointerDownAction = onPointerDownAction;
            this.onPointerUpAction = onPointerUpAction;
        }

        public void Deinit()
        {
            onPointerDownAction = null;
            onPointerUpAction = null;
        }
        
        public void OnPointerDown(PointerEventData eventData) => onPointerDownAction?.Invoke(eventData);

        public void OnPointerUp(PointerEventData eventData) => onPointerUpAction?.Invoke(eventData);
    }
}