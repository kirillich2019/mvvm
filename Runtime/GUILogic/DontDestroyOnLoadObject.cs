using UnityEngine;

namespace UISystem.GUILogic
{
    public class DontDestroyOnLoadObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}