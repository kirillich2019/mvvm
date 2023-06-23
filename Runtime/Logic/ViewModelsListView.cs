using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace UISystem
{
    /// <summary>
    /// Вьюха для коллекции вью моделей
    /// </summary>
    public class ViewModelsListView<T> : BaseUIView<IList<IBaseViewModel>>
    {
        [SerializeField] private ViewModelHolder listPrefab;
        [SerializeField] private RectTransform container;
        [SerializeField] private GameObject onEmptyGo;
        [Inject] private IInstantiator instantiator;
        
        private ViewModelListProperty<T> listProperty;
        private HashSet<ViewModelHolder> instantiatedViewModels;
        public List<ViewModelHolder> InstantiatedObjects => instantiatedViewModels?.ToList();

        protected override void InnerBind(object value)
        {
            listProperty = value as ViewModelListProperty<T>;

            if (listProperty == null)
                throw new Exception(
                    $"Свойство {bindingData.BindingTarget} в модели {bindingData.ViewModel.GetType().Name} не найдено является " +
                    $"приводимым к типу ViewModelListProperty.");

            listProperty.OnListValueAdded += OnAddNewElement;
            listProperty.OnListValueRemoved += OnRemoveElement;
            listProperty.OnValueChanged += InvokeOnValueChanged;
            
            bindingSuccess = true;
        }

        protected override void InvokeOnValueChanged()
        {
            if (!gameObject.activeSelf && !updateOnDisable) return;

            OnValueChanged(listProperty.Value);
        }

        private void OnValueChanged(IList<T> newValue)
        {
            if(instantiatedViewModels != null && instantiatedViewModels.Count != 0)
                foreach (var viewModelToDestroy in instantiatedViewModels)
                    Destroy(viewModelToDestroy.gameObject);

            instantiatedViewModels = new HashSet<ViewModelHolder>();

            if (newValue == null || newValue.Count == 0)
            {
                if(onEmptyGo != null) onEmptyGo.SetActive(true);
                return;
            } 
            
            if(onEmptyGo != null) onEmptyGo.SetActive(false);
            
            listPrefab.gameObject.SetActive(false);

            foreach (var viewModel in newValue)
            {
                var instance = instantiator.InstantiatePrefab(listPrefab, container).GetComponent<ViewModelHolder>();
                instance.StoredViewModel = viewModel as IBaseViewModel;
                instantiatedViewModels.Add(instance);
                instance.gameObject.SetActive(true);
            }
        }

        private void OnAddNewElement()
        {
            if (!gameObject.activeSelf && !updateOnDisable) return;
        }

        private void OnRemoveElement()
        {
            if (!gameObject.activeSelf && !updateOnDisable) return;
        }
        
        protected override void OnDestroy()
        {
            if(!bindingSuccess) return;
            
            listProperty.OnValueChanged -= InvokeOnValueChanged;
            listProperty.OnListValueAdded -= OnAddNewElement;
            listProperty.OnListValueRemoved -= OnRemoveElement;
        }
    }
}