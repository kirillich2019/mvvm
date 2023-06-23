using System;

namespace UISystem
{
  /// <summary>
  /// Базовый класс поле для ViewModel 
  /// </summary>
  public class ViewModelProperty<T>
  {
    private T value;

    public event Action OnValueChanged;
    
    public T Value
    {
      get => value;
      set
      {
        this.value = value;
        OnValueChanged?.Invoke();
      }
    }

    protected void InvokeOnValueChanged() => OnValueChanged?.Invoke();
  }
}