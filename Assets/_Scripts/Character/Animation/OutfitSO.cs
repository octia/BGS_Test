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

        public List<Sprite> _spritesWalkDown;

        public List<Sprite> _spritesWalkUp;

        public List<Sprite> _spritesWalkRight;

        public List<Sprite> _spritesWalkLeft;

        public Sprite GetIdleFrame(Vector2Int dir)
        {
            if (dir == Vector2Int.up)
            {
                return _spritesWalkUp[_indexIdleFrame];
            }
            else if (dir == Vector2Int.right)
            {
                return _spritesWalkRight[_indexIdleFrame];
            }
            else if (dir == Vector2Int.left)
            {
                return _spritesWalkLeft[_indexIdleFrame];
            }
            else
            {
                return _spritesWalkDown[_indexIdleFrame]; // this is the default if invalid dir passed
            }
        }

        public Sprite GetWalkFrame(Vector2Int dir, int frameNum)
        {
            if (frameNum < 0 || frameNum >= _indexIdleFrame)
            {
                return null;
            }
            if (dir == Vector2Int.up)
            {
                return _spritesWalkUp[frameNum];
            }
            else if (dir == Vector2Int.right)
            {
                return _spritesWalkRight[frameNum];
            }
            else if (dir == Vector2Int.left)
            {
                return _spritesWalkLeft[frameNum];
            }
            else
            {
                return _spritesWalkDown[frameNum]; // this is the default if invalid dir passed
            }
        }

    }
}