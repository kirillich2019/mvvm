using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace UISystem.Logic
{
    /// <summary>
    /// Добавляет в главную камеру на сцене ui камеру как Overlay 
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class AddUICameraToCameraStack : MonoBehaviour
    {
        [SerializeField] private Camera cameraWhereToAdd;


#if UNITY_EDITOR
        private void OnValidate()
        {
            if(cameraWhereToAdd != null) return;

            cameraWhereToAdd = gameObject.GetComponent<Camera>();
        }
#endif
        
        private void Awake()
        {
            var cameraData = cameraWhereToAdd.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Add(GameObject.FindWithTag("UICamera").GetComponent<Camera>());
        }
    }
}