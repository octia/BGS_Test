using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    /// <summary>
    /// Generic character class, handling movement and rotation
    /// </summary>
    public class Character : MonoBehaviour
    {

        #region MovementActions 
        // = delegate {} prevents the need for null checks.

        // Look direction changes. Can be up, down, left or right only, never 0,0
        public event Action<Vector2Int> OnHeadingChange = delegate { };

        /// <summary>
        /// Fired when movement input direction changes. Normalized, never 0,0
        /// </summary>
        public event Action<Vector2> OnDirectionChange = delegate { };

        /// <summary>
        /// Fired when movement starts (true) or stops (false)
        /// </summary>
        public event Action<bool> OnMovementChange = delegate { };
        #endregion

        protected Vector2Int _currentHeading = Vector2Int.down;
        protected Vector2 _currentDir = Vector2.down;
        protected bool _isMoving = false;

        protected bool _canMove = true;

        private void Start()
        {
            // Some debug stuff
            //OnHeadingChange += (Vector2Int heading) => Debug.Log("New heading = " + heading);
            //OnDirectionChange += (Vector2 dir) => Debug.Log("New dir = " + dir);
            //OnMovementChange += (bool change) => Debug.Log("Moving = " + change);
        }

        public void SetCanMove(bool canMove)
        {
            _canMove = canMove;
        }

        /// <summary>
        /// Called every frame, by any input script. Updates movement/rotation related stuff, and calls relevant events.
        /// </summary>
        /// <param name="dir">Raw direction from input</param>
        public void UpdateMoveDir(Vector2 inputDir)
        {
            if (_canMove)
            {
                Vector2 dir = inputDir;
                if (dir.magnitude > 0.1f) // normalize if not 0,0
                {
                    dir.Normalize();
                }
                else
                {
                    dir = Vector2.zero; // set to zero if close enough to zero
                }

                Vector2Int newHeading = new Vector2Int(Mathf.RoundToInt(dir.x), Mathf.RoundToInt(dir.y));
                if (newHeading.x != 0 && newHeading.y != 0)
                {
                    if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
                    {
                        newHeading.y = 0; // y axis takes priority if it's bigger
                    }
                    else
                    {
                        newHeading.x = 0; // otherwise turn into the direction x points to
                    }
                }


                bool inputIndicatesMovement = (newHeading != Vector2Int.zero);

                if (inputIndicatesMovement != _isMoving)
                {
                    OnMovementChange.Invoke(inputIndicatesMovement);
                    _isMoving = inputIndicatesMovement;
                }
                else
                {
                    bool inputDetected = newHeading != Vector2Int.zero;
                    if (inputDetected)
                    {

                        if (newHeading != _currentHeading)
                        {
                            OnHeadingChange.Invoke(newHeading);
                            _currentHeading = newHeading;
                        }
                        if (_currentDir != dir)
                        {
                            OnDirectionChange.Invoke(dir);
                            _currentDir = dir;
                        }
                    }
                }
            }
        }
    }
}