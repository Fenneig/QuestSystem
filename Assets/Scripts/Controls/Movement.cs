using UnityEngine;

namespace QuestSystem.Controls
{
    public class Movement
    {
        private CharacterController _controller;
        private InputReader _inputReader;
        private float _speed;

        public Movement(CharacterController controller, InputReader inputReader, float speed)
        {
            _controller = controller;
            _inputReader = inputReader;
            _speed = speed;
        }

        public void Move()
        {
            float forwardX = _inputReader.MovementY;
            float forwardY = _inputReader.MovementX;
            
            Vector3 motion = _controller.transform.forward * forwardX + _controller.transform.right * forwardY;
            
            _controller.Move( motion * _speed * Time.deltaTime);
        }
    }
}