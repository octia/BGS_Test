using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BGSTest
{
    public class EscHandler : MonoBehaviour
    {
        private void Update()
        {
            if (Application.isPlaying)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (UISystemManager.Instance)
                    {
                        if (UISystemManager.Instance.IsAnyUIOpen)
                        {
                            UISystemManager.Instance.CloseAllUISystems();
                            return;
                        }
                    }
                    #if PLATFORM_STANDALONE
                    Application.Quit();
                    #endif
                }
            }
        }
    }
}