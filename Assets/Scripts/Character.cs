using QuestSystem.Controls;
using UnityEngine;

namespace QuestSystem
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Transform _followObject;
        [Header("Movement")]
        [SerializeField] private float _speed;
        [Header("Rotation")]
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _maxVerticalAngle;
        [SerializeField] private float _minVerticalAngle;
        private PlayerInputs _input;
        private InputReader _inputReader;
        private Movement _movement;
        private CameraRotator _cameraRotator;

        private void Awake()
        {
            _input = new PlayerInputs();
            _inputReader = new InputReader(_input);
            _movement = new Movement(_controller, _inputReader, _speed);
            _cameraRotator = new CameraRotator(transform, _followObject,  _inputReader, _rotationSpeed, _maxVerticalAngle, _minVerticalAngle);
        }

        private void Update()
        {
            _movement.Move();
            _cameraRotator.Rotate();
        }
        
        private void OnEnable()
        {
            _input.OnGround.Enable();
        }

        private void OnDisable()
        {
            _input.OnGround.Disable();
        }
    }
}
