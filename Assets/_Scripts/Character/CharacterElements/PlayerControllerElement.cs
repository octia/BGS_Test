using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BGSTest
{
    [RequireComponent(typeof(Player))]
    public class PlayerControllerElement : CharacterControllerElement
    {
        protected Player playerController => (Player)characterController;
    }
}
