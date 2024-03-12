using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _speed;

    private Vector3 _moveDirection;
    
    private void Update()
    {
        _moveDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W))
            _moveDirection.z = 1;
        if (Input.GetKeyDown(KeyCode.S))
            _moveDirection.z = -1;
        if (Input.GetKeyDown(KeyCode.A))
            _moveDirection.x = 1;
        if (Input.GetKeyDown(KeyCode.D))
            _moveDirection.x = -1;

        _controller.Move(_moveDirection * _speed * Time.deltaTime);
    }
}
