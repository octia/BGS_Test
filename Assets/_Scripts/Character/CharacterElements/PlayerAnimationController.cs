using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BGSTest
{
    public class PlayerAnimationController : PlayerControllerElement
    {
        [SerializeField]
        private OutfitSO _bareBodyAnim;

        [SerializeField]
        private SpriteRenderer _bareBodyImage;

        [SerializeField]
        private SpriteRenderer _bodyOutfitImage;

        [SerializeField]
        private SpriteRenderer _headOutfitImage;

        [SerializeField]
        private float _animSpeed = 6; // frames per second

        [SerializeField]
        private int _walkAnimFrames = 6;

        private int _currentFrame = 0;

        private float _frameTimer = 0;

        private float _timeBetweenFrames => 1 / _animSpeed;

        private bool _isWalking = false;

        private bool _shouldUpdateFrame = true;

        private Vector2Int _currentHeading = Vector2Int.down;

        void Start()
        {
            playerController.OnHeadingChange += OnHeadingChange;
            playerController.OnMovementChange += OnMovementChange;
        }

        private void OnHeadingChange(Vector2Int newHeading)
        {
            _currentHeading = newHeading;
            _shouldUpdateFrame = true;
        }

        private void OnMovementChange(bool movementStarted)
        {
            _isWalking = movementStarted;
            if (_isWalking)
            {
                _currentFrame = 0;
            }
            _shouldUpdateFrame = true;
        }


        private void Update()
        {
            if (_shouldUpdateFrame)
            {
                UpdateFrame();
            }
            CycleWalkFrames();
        }

        void CycleWalkFrames()
        {
            if (!_isWalking)
            {
                return;
            }

            if (_frameTimer >= _timeBetweenFrames)
            {
                _frameTimer -= _timeBetweenFrames;
                if (_currentFrame < _walkAnimFrames - 1)
                {
                    _currentFrame++;
                }
                else
                {
                    _currentFrame = 0;
                }
            }
            _frameTimer += Time.deltaTime;
        }

        void UpdateFrame()
        {
            if (_isWalking)
            {
                _bareBodyImage.sprite = _bareBodyAnim.GetWalkFrame(_currentHeading, _currentFrame);
                if (playerController.headOutfit)
                {
                    _headOutfitImage.sprite = playerController.headOutfit.GetWalkFrame(_currentHeading, _currentFrame);
                }
                else
                {
                    _headOutfitImage.sprite = null;
                }
                if (playerController.bodyOutfit)
                {
                    _bodyOutfitImage.sprite = playerController.bodyOutfit.GetWalkFrame(_currentHeading, _currentFrame);
                }
                else
                {
                    _bodyOutfitImage.sprite = null;
                }
            }
            else
            {
                _bareBodyImage.sprite = _bareBodyAnim.GetIdleFrame(_currentHeading);
                if (playerController.headOutfit)
                {
                    _headOutfitImage.sprite = playerController.headOutfit.GetIdleFrame(_currentHeading);
                }
                else
                {
                    _headOutfitImage.sprite = null;
                }
                if (playerController.bodyOutfit)
                {
                    _bodyOutfitImage.sprite = playerController.bodyOutfit.GetIdleFrame(_currentHeading);
                }
                else
                {
                    _bodyOutfitImage.sprite = null;
                }
            }
        }



    }
}