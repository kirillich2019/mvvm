using DG.Tweening;
using UnityEngine;

namespace UISystem.GUILogic
{
    public class UnlessRotation : MonoBehaviour
    {
        [SerializeField] private float duration;

        private void Start() => transform.DORotate(new Vector3(0,0,-360), duration, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }
}