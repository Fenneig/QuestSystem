using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuestSystem.Controls
{
    public class InputReader : IDisposable
    {
        private PlayerInputs _inputs;
        public Vector2 Look => _inputs.OnGround.Look.ReadValue<Vector2>();
        public float MovementX => Movement.x;
        public float MovementY => Movement.y;
        private Vector2 Movement => _inputs.OnGround.Walk.ReadValue<Vector2>();

        public event Action OnInteract;
        
        public InputReader(PlayerInputs inputs)
        {
            _inputs = inputs;
            _inputs.OnGround.Interact.started += OnInteractFired;
        }

        public void Dispose()
        {
            _inputs.OnGround.Interact.started -= OnInteractFired;
        }

        private void OnInteractFired(InputAction.CallbackContext obj) => OnInteract?.Invoke();
    }
}