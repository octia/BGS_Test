using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BGSTest
{
    [RequireComponent(typeof(Character))]
    public abstract class CharacterControllerElement : MonoBehaviour
    {
        protected Character characterController;
        protected virtual void Awake()
        {
            characterController = GetComponent<Character>();
        }
    }
}