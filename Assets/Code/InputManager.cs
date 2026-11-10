using Code;
using UnityEngine;

namespace Code
{
    public class InputManager : MonoBehaviour
    {
        private InputActions _actions;
        public InputActions Actions => _actions;

        private static InputManager _instance;
        public static InputManager Instance => _instance;
        
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                _actions = new InputActions();
            }
        }
        
    }
}
