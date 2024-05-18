using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{

    /// <summary>
    /// This class handles character movement based on Character events.
    /// TODO: Change to raycast obstacle detection, for use with a kinematic rigidbody. Current method has bugs.
    /// </summary>
    public class BasicMovement : CharacterControllerElement
    {

        [SerializeField]
        [Range(0f, 6f)]
        private float _speed = 2.5f;

        private Rigidbody2D _rb;
        private Vector2 _movementDir = Vector2.zero;

        private bool _isMoving = false;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            characterController.OnDirectionChange += OnDirectionChange;
            characterController.OnMovementChange += OnMovementChange;
        }

        private void OnDirectionChange(Vector2 dir)
        {
            _movementDir = dir;
        }

        private void OnMovementChange(bool movementStarted)
        {
            _isMoving = movementStarted;
        }


        private void FixedUpdate()
        {
            if (_isMoving)
            {
                _rb.velocity = _movementDir * _speed;
            }
            else
            {
                _rb.velocity = Vector2.zero;
            }
        }
    }
}