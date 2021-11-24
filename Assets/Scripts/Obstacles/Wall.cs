using System;
using System.Collections;
using Managers;
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

        #region Movement Related Variables

        [Header("Movement Settings")] 
        public float moveSpeed;
        public Axis moveAxis;
        public bool moveToNegative;
        public float distance;
        
        private Vector3 _movementVector;
        private Vector3 _startingPosition;
        private Vector3 _targetPosition;
        private bool _movingToTarget = true;
        
        #endregion

        #region Rotate Related Variables

        [Header("Rotation Settings")]
        public float rotateSpeed;
        public Axis rotateAxis;
        public bool rotateClockwise;

        private Vector3 _rotationVector;
        
        #endregion

        #region Freeze Related Variables
        
        [Header("Freeze Related")]
        [SerializeField]private GameObject freezeParticles;
        [SerializeField]private Material frozenWallMaterial;
        private Material _mainMaterial;
        
        private bool _isFrozen;
        private Coroutine _freezeCoroutine;

        #endregion

        private MeshRenderer _meshRenderer;

        private void Start()
        {
            _startingPosition = transform.position;
            _meshRenderer = gameObject.GetComponent<MeshRenderer>();

            #region Setting Movement Vector and Target Position

            switch (moveAxis)
            {
                case Axis.X:
                    _movementVector = Vector3.right * (moveToNegative ? -1 : 1);
                    _targetPosition = _startingPosition + Vector3.right * distance * (moveToNegative ? -1 : 1);
                    break;
                case Axis.Y:
                    _movementVector = Vector3.up * (moveToNegative ? -1 : 1);
                    _targetPosition = _startingPosition + Vector3.up * distance * (moveToNegative ? -1 : 1);
                    break;
                case Axis.Z:
                    _movementVector = Vector3.forward * (moveToNegative ? -1 : 1);
                    _targetPosition = _startingPosition + Vector3.forward * distance * (moveToNegative ? -1 : 1);
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            #endregion
            
            #region Setting Rotation Vector

            _rotationVector = rotateAxis switch
            {
                Axis.X => Vector3.right * (rotateClockwise ? 1 : -1),
                Axis.Y => Vector3.up * (rotateClockwise ? 1 : -1),
                Axis.Z => Vector3.forward * (rotateClockwise ? 1 : -1),
                _ => _rotationVector
            };

            #endregion

            #region Freeze Related Teachings

            _mainMaterial = _meshRenderer.material;

            #endregion

        }

        private void Update()
        {
            var transform1 = transform;

            #region Moving and Rotating the Wall

            if (moveSpeed > 0)
            {
                transform1.position += _movementVector * (moveSpeed * Time.deltaTime * (_movingToTarget ? 1 : -1) * (_isFrozen ? 0 : 1));
            }
            
            if (rotateSpeed > 0)
            {
                transform1.Rotate(_rotationVector * (rotateSpeed * Time.deltaTime * (_isFrozen ? 0 : 1)));
            }

            #endregion

            #region Switch Direction After Reaching Target
            
            var position = transform1.position;
            
            _movingToTarget = _movingToTarget switch
            {
                true when Vector3.Distance(position, _targetPosition) < .3f => false,
                false when Vector3.Distance(position, _startingPosition) < .3f => true,
                _ => _movingToTarget
            };
            
            #endregion

        }

        private void OnEnable()
        {
            EventManager.GlobalFreeze += Freeze;
        }

        private void OnDisable()
        {
            EventManager.GlobalFreeze -= Freeze;
        }

        #region Freeze Related Functions

        public void Freeze(float freezeTime)
        {
            UnFreeze();
            _freezeCoroutine = StartCoroutine(FreezeCoroutine(freezeTime));
        }

        private IEnumerator FreezeCoroutine(float freezeTime)
        {
            _isFrozen = true;
            freezeParticles.SetActive(true);
            _meshRenderer.material = frozenWallMaterial;

            yield return new WaitForSeconds(freezeTime);
            
            UnFreeze();
        }

        public void UnFreeze()
        {
            if(_freezeCoroutine != null) StopCoroutine(_freezeCoroutine);
            
            _isFrozen = false;
            freezeParticles.SetActive(false);
            _meshRenderer.material = _mainMaterial;
        }

        #endregion
    }
}
