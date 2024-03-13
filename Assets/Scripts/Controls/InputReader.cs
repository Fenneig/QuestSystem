using UnityEngine;

namespace QuestSystem.Controls
{
    public class InputReader
    {
        private PlayerInputs _inputs;
        public InputReader(PlayerInputs inputs) => _inputs = inputs;

        public Vector2 Look => _inputs.OnGround.Look.ReadValue<Vector2>();
        public float MovementX => Movement.x;
        public float MovementY => Movement.y;
        private Vector2 Movement => _inputs.OnGround.Walk.ReadValue<Vector2>();
    }
}