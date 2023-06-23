using System;
using UnityEngine;

namespace UISystem.GUILogic
{
    /// <summary>
    /// Канвас для системы гуя
    /// </summary>
    public class GUISystemCanvas : MonoBehaviour
    {
        public CanvasLayer[] CanvasLayers;
    }

    [Serializable]
    public class CanvasLayer
    {
        public RectTransform RectTransform;
        public CanvasGroup CanvasGroup;

        public void SetActive(bool active)
        {
            CanvasGroup.interactable = active;
            CanvasGroup.blocksRaycasts = active;
            CanvasGroup.alpha = active ? 1 : 0;
        }
    }
}