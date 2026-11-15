using Code;
using UnityEngine;

namespace Code
{
    public class InputManager
    {
        private InputActions _actions;
        public InputActions Actions => _actions;

        private static InputManager _instance;
        
        public InputActions.PlayerActions Player => _actions.Player;

        public static InputManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InputManager();
                    _instance._actions = new InputActions();
                    _instance.Player.Enable();
                }
                return _instance;
            }
        }
    }
}
