using System;
using System.Reflection;
using UnityEngine;
using Object = System.Object;

namespace UISystem
{
    [DefaultExecutionOrder(-1)]
    public abstract class BaseUIView<T> : MonoBehaviour
    {
        [SerializeField] protected BindingData bindingData;
        [SerializeField] protected bool updateOnDisable; //обновлять ли значение когда компонент выключен

        protected bool bindingSuccess;
        private ViewModelProperty<T> connectedProperty;

        private void Awake()
        {
            OnBindingNewViewModel(bindingData);
        }

        private void OnBindingNewViewModel(BindingData newViewModel)
        {
            bindingData = newViewModel;

            if (bindingData != null
                && bindingData.ViewModel != null
                && !string.IsNullOrEmpty(bindingData.BindingTarget)
                && !string.IsNullOrWhiteSpace(bindingData.BindingTarget))
                Binding();
        }

        protected virtual void OnValueChanged(T newValue)
        {
        }

        private void Binding()
        {
            var propertyInfos = bindingData.ViewModel.GetViewModelType.GetProperties();
            PropertyInfo targetProperty = null;

            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.Name != bindingData.BindingTarget) continue;
                targetProperty = propertyInfo;
            }

            if (targetProperty == null)
                throw new Exception(
                    $"Свойство {bindingData.BindingTarget} в модели {bindingData.ViewModel.GetType().Name} не найдено. " +
                    $"Привязка не прошла.");


            var value = targetProperty.GetValue(bindingData.ViewModel.StoredViewModel);
            
            InnerBind(value);
        }

        protected virtual void InnerBind(Object value)
        {
            connectedProperty = value as ViewModelProperty<T>;

            if (connectedProperty == null)
                throw new Exception(
                    $"Свойство {bindingData.BindingTarget} и тип данных в представлении различаются.");
            
            connectedProperty.OnValueChanged += InvokeOnValueChanged;
            InvokeOnValueChanged();
            bindingSuccess = true;            
        }

        protected virtual void InvokeOnValueChanged()
        {
            if (!gameObject.activeSelf && !updateOnDisable) return;

            OnValueChanged(connectedProperty.Value);
        }

        protected virtual void OnDestroy()
        {
            if (!bindingSuccess) return;

            connectedProperty.OnValueChanged -= InvokeOnValueChanged;
        }
    }
}