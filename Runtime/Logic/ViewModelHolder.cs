using System;

namespace UISystem
{
    /// <summary>
    /// Холдер для вью модели - в него можно установить стороннюю вьюмодельку
    /// </summary>
    public class ViewModelHolder : BaseViewModelBehaviour
    {
        public override Type GetViewModelType => StoredViewModel.GetType();
        public override IBaseViewModel StoredViewModel { get; set; }
    }
}