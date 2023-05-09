using System;
using UnityEngine;

namespace UISystem
{
  [Serializable]
  public sealed class BindingData
  {
    [SerializeField] private BaseViewModelBehaviour viewModel;
    [SerializeField] private string bindingTarget;

    public BaseViewModelBehaviour ViewModel
    {
      get => viewModel;
      set => viewModel = value;
    }

    public string BindingTarget
    {
      get => bindingTarget;
      set => bindingTarget = value;
    }
  }
}