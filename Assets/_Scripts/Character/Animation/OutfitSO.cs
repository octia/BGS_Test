using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BGSTest
{

    [CreateAssetMenu(fileName = "New Outfit", menuName = "Scriptables/New Outfit")]
    public class OutfitSO : ScriptableObject
    {

        public string outfitName;


        public int _walkAnimFrames = 6;

        public int _indexIdleFrame = 6;

        [SerializeField]
        private Vector2Int _walkUpIndexRange = new Vector2Int(40, 45);
        [SerializeField]
        private Vector2Int _walkDownIndexRange = new Vector2Int(32,37);
        [SerializeField]
        private Vector2Int _walkRightIndexRange = new Vector2Int(48, 53);
        [SerializeField]
        private Vector2Int _walkLeftIndexRange = new Vector2Int(56,61);
        [SerializeField]
        private int _idleDownIndex = 0;
        [SerializeField]
        private int _idleUpIndex = 8;
        [SerializeField]
        private int _idleRightIndex = 16;
        [SerializeField]
        private int _idleLeftIndex = 24;

        [SerializeField]
        public List<Sprite> _spriteList;

        public Sprite GetIdleFrame(Vector2Int dir)
        {
            if (dir == Vector2Int.up)
            {
                return _spriteList[_idleUpIndex];
            }
            else if (dir == Vector2Int.right)
            {
                return _spriteList[_idleRightIndex];
            }
            else if (dir == Vector2Int.left)
            {
                return _spriteList[_idleLeftIndex];
            }
            else
            {
                return _spriteList[_idleDownIndex]; // this is the default if invalid dir passed
            }
        }

        public Sprite GetWalkFrame(Vector2Int dir, int frameNum)
        {
            Vector2Int selectedFrameSet;
            if (dir == Vector2Int.up)
            {
                selectedFrameSet = _walkUpIndexRange;
            }
            else if (dir == Vector2Int.right)
            {
                selectedFrameSet = _walkRightIndexRange;
            }
            else if (dir == Vector2Int.left)
            {
                selectedFrameSet = _walkLeftIndexRange;
            }
            else
            {
                selectedFrameSet = _walkDownIndexRange; // this is the default if invalid dir passed
            }
            if (frameNum < 0 || frameNum > selectedFrameSet.y - selectedFrameSet.x)
            {
                return _spriteList[_idleDownIndex]; // this is the default if invalid data is passed;
            }
            return _spriteList[selectedFrameSet.x + frameNum];
        }

    }
}