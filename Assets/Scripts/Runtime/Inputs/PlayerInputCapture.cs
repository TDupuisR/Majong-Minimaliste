using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputCapture
{
    public class PlayerInputCapture : MonoBehaviour
    {
        private static PlayerInputCapture? instance;
        public static PlayerInputCapture Instance
        {
            get
            {
                return instance;
            }
        }

        private PlayerInput _inputAction;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
                return;
            }
            else instance = this;
            
            DontDestroyOnLoad(this);
        }



        public event Action<Vector2> OnP1MoveStart;
        public event Action<Vector2> OnP1MoveEnd;

        public event Action<Vector2> OnP2MoveStart;
        public event Action<Vector2> OnP2MoveEnd;

        private void OnEnable() {
            _inputAction = new PlayerInput();
            _inputAction.Gameplay.Enable();

            _inputAction.Gameplay.MovementPlayer1.started += ctx => OnP1MoveStart?.Invoke(ctx.ReadValue<Vector2>());
            _inputAction.Gameplay.MovementPlayer1.canceled += ctx => OnP1MoveEnd?.Invoke(Vector2.zero);

            _inputAction.Gameplay.MovementPlayer2.started += ctx => OnP2MoveStart?.Invoke(ctx.ReadValue<Vector2>());
            _inputAction.Gameplay.MovementPlayer2.canceled += ctx => OnP2MoveEnd?.Invoke(Vector2.zero);
        }

        private void OnDisable()
        {
            _inputAction.Gameplay.Disable();
        }
    }
}
