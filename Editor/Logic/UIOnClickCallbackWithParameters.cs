using UnityEngine;

namespace UISystem
{
    /// <summary>
    /// Коллебек на наэатие кнопки с параметрами
    /// </summary>
    public class UIOnClickCallbackWithParameters : UIOnClickCallback
    {
        [SerializeField] private string parameter;
        
        protected override void OnClickCallback()
        {
            targetMethod.Invoke(bindingData.ViewModel, new object[]{parameter});
        }
    }
}