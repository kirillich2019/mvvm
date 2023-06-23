using System;
using System.Collections.Generic;

namespace UISystem
{
    /// <summary>
    /// ViewModel пропертя содержащаяя в себе коллекцию вью моделей
    /// </summary>
    public class ViewModelListProperty<T> : ViewModelProperty<IList<T>>
    {
        public event Action OnListValueAdded;
        public event Action OnListValueRemoved;

        public void Add(T item)
        {
            Value ??= new List<T>();
            
            Value.Add(item);
            OnListValueAdded?.Invoke();
        }

        public void Remove(T item)
        {
            Value?.Remove(item);
            OnListValueRemoved?.Invoke();
        }

        public void Clear()
        {
            Value?.Clear();
            InvokeOnValueChanged();
        }
    }
}