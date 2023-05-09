using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UISystem.Logic
{
    /// <summary>
    /// Коллбек на нажатие по поинтеру
    /// </summary>
    public class UIOnPointerClickCallback : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected BindingData bindingData;
        
        protected MethodInfo targetMethod;
        private bool bindingSuccess;
        
        private void Start()
        {
            targetMethod = bindingData.ViewModel.StoredViewModel.GetType().GetMethod(bindingData.BindingTarget);
            bindingSuccess = targetMethod != null;

            if (!bindingSuccess)
            {
                throw new Exception($"Метода {bindingData.BindingTarget} в модели {bindingData.ViewModel.GetType().Name} не найдено. " +
                                    $"Привязка не прошла.");
            }
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!bindingSuccess) return;
            
            targetMethod.Invoke(bindingData.ViewModel.StoredViewModel, Array.Empty<object>());
        }
    }
}