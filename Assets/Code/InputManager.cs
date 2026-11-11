using Code;
using UnityEngine;

namespace Code
{
    public class InputManager
    {
        private InputActions _actions;
        public InputActions Actions => _actions;

        private static InputManager _instance;

        public static InputManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InputManager();
                    _instance._actions = new InputActions();
                }
                return _instance;
            }
        }
    }
}
