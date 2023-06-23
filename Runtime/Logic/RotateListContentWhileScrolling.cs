using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem.Logic
{
    /// <summary>
    /// Будет вращать содержимое списка в заданнуб сторону при скролле
    /// </summary>
    public class RotateListContentWhileScrolling : MonoBehaviour
    {
        [SerializeField] private ScrollRectWihExtensionEvents scrollRect;
        [SerializeField] private float rotateValue;
        [SerializeField] private float maxScrollVelocity = 10;
        [SerializeField] private float rotSpeed = 5;

        private CancellationTokenSource cts;
        private bool rotate;
        private Transform[] transforms;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (scrollRect != null) return;

            scrollRect = GetComponent<ScrollRectWihExtensionEvents>();
        }
#endif

        private void OnEnable()
        {
            scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        }

        private void OnDisable()
        { 
            rotate = false;
            scrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);
            cts?.Cancel();
        }

        public void OnScrollValueChanged(Vector2 newValue)
        {
            if(float.IsNaN(newValue.x) || float.IsNaN(newValue.y)) 
                return;
            
            if (rotate) return;
            cts?.Cancel();

            rotate = true;

            cts = new CancellationTokenSource();
            Rotate(cts.Token).Forget();
        }

        private async UniTask Rotate(CancellationToken token)
        {
            if (scrollRect.content.childCount == 0)
            {
                rotate = false;
                return;
            }
            
            transforms = new Transform[scrollRect.content.childCount];

            for (var i = 0; i < transforms.Length; i++)
            {
                var currentTransform = scrollRect.content.GetChild(i);
                transforms[i] = currentTransform;
            }

            while (!token.IsCancellationRequested)
            {
                await UniTask.Yield(token).SuppressCancellationThrow();
                if (token.IsCancellationRequested) return;

                var velocity = scrollRect.velocity.magnitude > 0.05f ? scrollRect.velocity.x : scrollRect.fakeVelocity.x;

                if (Mathf.Abs(velocity) < 0.05f)
                {
                    foreach (var t in transforms)
                    {
                        t.rotation = Quaternion.Euler(
                            new Vector3(
                                0,
                                0,
                                0)
                        );
                    }

                    rotate = false;
                    return;
                }
                
                var newAngleValue =
                    -Mathf.Sign(velocity)
                    * Mathf.Lerp(
                        0,
                        rotateValue,
                        Mathf.Clamp01(Mathf.Abs(velocity / maxScrollVelocity))
                    );

                if (transforms[0] == null)
                {
                    rotate = false;
                    return;
                }
                
                newAngleValue = Mathf.LerpAngle(transforms[0].rotation.eulerAngles.y, newAngleValue, Time.deltaTime * rotSpeed);
                
                foreach (var t in transforms)
                {
                    t.rotation = Quaternion.Euler(
                        new Vector3(
                            0,
                            newAngleValue,
                            0)
                    );
                }
            }
        }
    }
}