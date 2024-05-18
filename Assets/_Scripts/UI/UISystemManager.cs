using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    /// <summary>
    /// Class managing all different UI Screens. I don't need fancy UI stuff for this project, so a simple manager is enough.
    /// </summary>
    public class UISystemManager : MonoBehaviour
    {
        #region Singleton
        public UISystemManager Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        /// <summary>
        /// Fired when gameplay is paused (true) or resumed (false)
        /// </summary>
        public event Action<bool> OnGameplayPaused; // should be in a separate system perhaps, but I'm running out of time for this project.

        private Dictionary<Type, UISystemBase> _uiSystems;
        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.TryGetComponent<UISystemBase>(out var systemToAdd))
                {
                    _uiSystems.Add(systemToAdd.GetType(), systemToAdd);
                    systemToAdd.Initialize();
                }
            }
        }

        /// <summary>
        /// Tries to open a given UI system, by type.
        /// </summary>
        /// <typeparam name="T">Type of the UI system to open.</typeparam>
        /// <returns>System if found, null if not found.</returns>
        public T OpenUISystem<T>() where T : UISystemBase
        {
            if (_uiSystems.TryGetValue(typeof(T), out var result))
            {
                foreach (var system in _uiSystems.Values)
                {
                    if (result.IsExclusive || system.IsExclusive)
                    {
                        system.Close();
                    }
                }
                result.Open();
                return (T)result;
            }
            return null;
        }
    }
}