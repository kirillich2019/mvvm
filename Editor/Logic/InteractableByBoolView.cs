using UnityEngine;
using UnityEngine.UI;

namespace UISystem.Logic
{
    public class InteractableByBoolView: BaseUIView<bool>
    {
        [SerializeField] private Button btn;
        [SerializeField] private bool invert;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if(btn != null) return;

            btn = GetComponent<Button>();
        }
#endif

        protected override void OnValueChanged(bool newValue) => btn.interactable = invert ? !newValue : newValue;
    }
}