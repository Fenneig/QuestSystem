using UnityEngine;

namespace QuestSystem.Controls
{
    public class CameraRotator
    {
        private Transform _rotationObjectTransform;
        private Transform _followObjectTransform;
        private InputReader _inputReader;
        private float _maxAngle, _minAngle, _horizontalRotationSpeed, _verticalRotationSpeed;

        public CameraRotator(Transform rotationObject, Transform followObjectTransform,  InputReader inputReader, float horizontalRotationSpeed,float verticalRotationSpeed, float maxAngle, float minAngle)
        {
            _rotationObjectTransform = rotationObject;
            _followObjectTransform = followObjectTransform;
            _inputReader = inputReader;
            _horizontalRotationSpeed = horizontalRotationSpeed;
            _verticalRotationSpeed = verticalRotationSpeed;
            _maxAngle = maxAngle;
            _minAngle = minAngle;
        }

        public void Rotate()
        {
            HorizontalRotate();

            VerticalRotate();
        }

        private void HorizontalRotate()
        {
            _rotationObjectTransform.rotation *= Quaternion.AngleAxis(_inputReader.Look.x * _horizontalRotationSpeed, Vector3.up);
        }

        private void VerticalRotate()
        {
            _followObjectTransform.rotation *= Quaternion.AngleAxis(_inputReader.Look.y * _verticalRotationSpeed, Vector3.right);

            Vector3 angles = _followObjectTransform.localEulerAngles;
            angles.z = 0;
            float angle = angles.x;

            angles.x = angle switch
            {
                > 180 when angle < _maxAngle => _maxAngle,
                < 180 when angle > _minAngle => _minAngle,
                _ => angles.x
            };

            _followObjectTransform.localEulerAngles = angles;
        }
    }
}