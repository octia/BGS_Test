using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    public class PlayerInput : CharacterControllerElement
    {   
        private Vector2 _inputDir;

        void Update()
        {
            _inputDir = Vector2.zero;
            // TODO: Switch to new input system.
            if (Input.GetKey(KeyCode.W))
            {
                _inputDir += Vector2.up;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _inputDir += Vector2.left;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _inputDir += Vector2.down;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _inputDir += Vector2.right;
            }

            characterController.UpdateMoveDir(_inputDir);
        }
    }
}