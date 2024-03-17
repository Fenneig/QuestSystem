using QuestSystem.Controls;
using QuestSystem.Interacts;
using UnityEngine;

namespace QuestSystem.Characters
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Transform _followObject;
        [Header("Movement")]
        [SerializeField] private float _speed;
        [Header("Rotation")]
        [SerializeField] private float _horizontalRotationSpeed;
        [SerializeField] private float _verticalRotationSpeed;
        [SerializeField] private float _maxVerticalAngle;
        [SerializeField] private float _minVerticalAngle;
        [Header("Interact")] 
        [SerializeField] private InteractArea _interactArea;
        private PlayerInputs _input;
        private InputReader _inputReader;
        private Movement _movement;
        private CameraRotator _cameraRotator;

        private void Awake()
        {
            _input = new PlayerInputs();
            _inputReader = new InputReader(_input);
            _movement = new Movement(_controller, _inputReader, _speed);
            _cameraRotator = new CameraRotator(transform, _followObject,  _inputReader, _horizontalRotationSpeed, _verticalRotationSpeed, _maxVerticalAngle, _minVerticalAngle);
            _inputReader.OnInteract += _interactArea.Interact;
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

        private void OnDestroy()
        {
            _inputReader.OnInteract -= _interactArea.Interact;
            _input.Dispose();
        }
    }
}
