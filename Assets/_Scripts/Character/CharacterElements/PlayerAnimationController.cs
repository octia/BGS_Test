using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    public class PlayerAnimationController : CharacterControllerElement
    {
        
        void Start()
        {
            characterController.OnHeadingChange += OnHeadingChange;
            characterController.OnMovementChange += OnMovementChange;
        }

        private void OnHeadingChange(Vector2Int newHeading)
        {

        }

        private void OnMovementChange(bool movementStarted)
        {

        }
    }
}