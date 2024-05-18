using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    public class CharacterInteractor : CharacterControllerElement
    {

        [SerializeField]
        private LayerMask _interactionLayer;

        [SerializeField]
        [Range(0f, 5f)]
        private float _maxInteractionDistance = 2f;

        private Vector2 _currentHeading;

        private bool _interactionPossible = false;

        void Start()
        {
            characterController.OnHeadingChange += OnHeadingChange;
        }

        private void OnHeadingChange(Vector2Int heading)
        {
            _currentHeading = new Vector2(heading.x, heading.y);
        }

        private void AttemptInteraction()
        {
            
        }

        private bool CheckInteractionAvaliability()
        {
            return false;
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.red;
                Debug.Log("heading: " + _currentHeading);
                Gizmos.DrawRay(characterController.transform.position, _currentHeading);
            }
        }

        private bool TryGetInteractable(out IInteractable interactable)
        {
            Vector2 startPos = (Vector2)characterController.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(startPos, _currentHeading, _maxInteractionDistance, _interactionLayer);
            
            if (hit)
            {
                bool foundInteractable = hit.transform.TryGetComponent<IInteractable>(out interactable);
                if (foundInteractable)
                {
                    return true;
                }
                else
                {
                    interactable = null;
                    return false;
                }
            }
            interactable = null;
            return false;
        }

        private void Update()
        {
            if (_interactionPossible)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    AttemptInteraction();
                }
            }
        }

        private void FixedUpdate()
        {
            bool interactableFound = TryGetInteractable(out _);
            if (interactableFound != _interactionPossible)
            {

            }
        }
    }
}