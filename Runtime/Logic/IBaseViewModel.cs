using System;
using UnityEngine;

namespace UISystem
{
  /// <summary>
  /// Базовый интерфейс для модели представления
  /// </summary>
  public interface IBaseViewModel
  {
  }

  /// <summary>
  /// Базовый класс для модели представления унаследованый от MonoBehaviour
  /// </summary>
  public class BaseViewModelBehaviour : MonoBehaviour, IBaseViewModel
  {
    public virtual Type GetViewModelType => GetType();
    public virtual IBaseViewModel StoredViewModel
    {
      get => this; 
      set {}
    }
  }
}