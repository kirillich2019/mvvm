using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
  /// <summary>
  /// Коллбек на нажатие кнопки в UI
  /// </summary>
  [RequireComponent(typeof(Button))]
  public class UIOnClickCallback : MonoBehaviour
  {
    [SerializeField] protected BindingData bindingData;

    [SerializeField, HideInInspector] private Button button;

    protected MethodInfo targetMethod;
    private bool bindingSuccess;

    private void OnValidate()
    {
      if(button == null) button = GetComponent<Button>();
    }

    private void Start()
    {
      targetMethod = bindingData.ViewModel.StoredViewModel.GetType().GetMethod(bindingData.BindingTarget);
      bindingSuccess = targetMethod != null;

      if (!bindingSuccess)
      {
        throw new Exception($"Метода {bindingData.BindingTarget} в модели {bindingData.ViewModel.GetType().Name} не найдено. " +
                            $"Привязка не прошла.");
      }
      
      button.onClick.AddListener(OnClickCallback);
    }

    private void OnDestroy()
    {
      if(!bindingSuccess) return;
      
      button.onClick.RemoveListener(OnClickCallback);
    }

    protected virtual void OnClickCallback()
    {
      targetMethod.Invoke(bindingData.ViewModel.StoredViewModel, Array.Empty<object>());
    }
  }
}