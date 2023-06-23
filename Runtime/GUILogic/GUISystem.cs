using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UISystem.GUILogic
{
    /// <summary>
    /// Системая гуя игры
    /// </summary>
    [CreateAssetMenu(fileName = "GUISystem", menuName = "ScriptableObjects/GUISystem", order = 0)]
    public class GUISystem: ScriptableObject
    {
        [SerializeField, ScreenMarker] private List<BaseViewModelBehaviour> screensList;

        [Inject] private IInstantiator instantiator;

        private GUISystemCanvas currentCanvas;
        
        private Dictionary<Type, BaseViewModelBehaviour> screens = new Dictionary<Type, BaseViewModelBehaviour>();
        private Dictionary<Type, BaseViewModelBehaviour> hidedScreens = new Dictionary<Type, BaseViewModelBehaviour>(); 
        private Dictionary<Type, BaseViewModelBehaviour> openedScreens = new Dictionary<Type, BaseViewModelBehaviour>(); 

        public void Initialize(GUISystemCanvas mainCanvas)
        {
            currentCanvas = mainCanvas;

            foreach (var screen in screensList)
            {
                screens.Add(screen.GetType(), screen);
            }
        }

        public T ShowScreen<T>(int layer = 0) where T : BaseViewModelBehaviour
        {
            var key = typeof(T);

            if (hidedScreens.ContainsKey(key))
            {
                var hidedScreen = hidedScreens[key];
                hidedScreen.gameObject.SetActive(true);
                hidedScreens.Remove(key);
                return (T)hidedScreen;
            }

            if (openedScreens.ContainsKey(key))
                return (T) openedScreens[key];

            var screenPrefab = screens[key];
            var screenInstance = instantiator.InstantiatePrefab(
                screenPrefab, 
                currentCanvas.CanvasLayers[layer].RectTransform)
                .GetComponent<T>();
            openedScreens.Add(key, screenInstance);
            return screenInstance;
        }

        public void CloseScreen<T>() where T : BaseViewModelBehaviour
        {
            var key = typeof(T);

            if (openedScreens.ContainsKey(key))
            {
                var screen = openedScreens[key];
                openedScreens.Remove(key);
                Destroy(screen.gameObject);
            }
            
            if (hidedScreens.ContainsKey(key))
                hidedScreens.Remove(key);
        }

        public void HideScreen<T>() where T : BaseViewModelBehaviour
        {
            var key = typeof(T);

            if (!openedScreens.ContainsKey(key))
            {
                Debug.LogWarning($"Экран {key} ещё не открыт.");
                return;
            }

            var screen = openedScreens[key];
            
            if (hidedScreens.ContainsKey(key))
            {
                Debug.LogWarning($"Экран {key} уже скрыт.");
            }
            
            hidedScreens.Add(key, screen);
            screen.gameObject.SetActive(false);
        }

        public void SetCanvasLayerActive(int layer, bool active) => currentCanvas.CanvasLayers[layer].SetActive(active);

        public bool ScreenIsOpen<T>() where T : BaseViewModelBehaviour
        {
            var key = typeof(T);

            return openedScreens.ContainsKey(key);
        }
    }
}