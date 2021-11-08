using System;
using UnityEngine;

namespace Obstacles
{
    public class Wall : MonoBehaviour
    {
        public enum Axis
        {
            X,
            Y,
            Z
        }
        
        [Header("Movement Settings")] 
        public float moveSpeed;
        public Axis moveAxis;
        public bool moveToNegative;
        public float distance;

        private Vector3 _movementVector;
        private Vector3 _startingPosition;
        private Vector3 _targetPosition;
        private bool _movingToTarget = true;
        
        [Header("Rotation Settings")]
        public float rotateSpeed;
        public Axis rotateAxis;
        public bool rotateClockwise;

        private Vector3 _rotationVector;

        private void Start()
        {
            _startingPosition = transform.position;
            
            switch (moveAxis)
            {
                case Axis.X:
                    _movementVector = Vector3.right;
                    if (moveToNegative)
                    {
                        _targetPosition = _startingPosition + Vector3.right * distance * -1;
                    }
                    else
                    {
                        _targetPosition = _startingPosition + Vector3.right * distance;
                    }
                    break;
                case Axis.Y:
                    _movementVector = Vector3.up;
                    if (moveToNegative)
                    {
                        _targetPosition = _startingPosition + Vector3.up * distance * -1;
                    }
                    else
                    {
                        _targetPosition = _startingPosition + Vector3.up * distance;
                    }
                    break;
                case Axis.Z:
                    _movementVector = Vector3.forward;
                    if (moveToNegative)
                    {
                        _targetPosition = _startingPosition + Vector3.forward * distance * -1;
                    }
                    else
                    {
                        _targetPosition = _startingPosition + Vector3.forward * distance;
                    }
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (moveToNegative) 
                _movementVector *= -1;
            
            
            _rotationVector = rotateAxis switch
            {
                Axis.X => Vector3.right,
                Axis.Y => Vector3.up,
                Axis.Z => Vector3.forward,
                _ => _rotationVector
            };

            if (!rotateClockwise) 
                _rotationVector *= -1;
        }

        private void Update()
        {
            Transform transform1;
            (transform1 = transform).Rotate(_rotationVector * (rotateSpeed * Time.deltaTime));
            transform1.position += _movementVector * (moveSpeed * Time.deltaTime);
            switch (_movingToTarget)
            {
                case true when Vector3.Distance(transform1.position, _targetPosition) < 1:
                    _movementVector *= -1;
                    _movingToTarget = false;
                    break;
                case false when Vector3.Distance(transform1.position, _startingPosition) < 1:
                    _movementVector *= -1;
                    _movingToTarget = true;
                    break;
            }
        }
    }
}
