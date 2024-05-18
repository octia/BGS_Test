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

        private bool TryGetInteractable(out IInteractable interactable)
        {
            bool anythingHit = Physics.Raycast(characterController.transform.position, _currentHeading, out RaycastHit hit, _maxInteractionDistance, _interactionLayer);
            interactable = null;
            if (anythingHit)
            {
                Debug.Log("Something was hit!");
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
            return false;
        }

        private void FixedUpdate()
        {
            bool interactableFound = TryGetInteractable(out _);
            if (interactableFound)
            {
                Debug.Log("Interactable found!");

            }
        }
    }
}