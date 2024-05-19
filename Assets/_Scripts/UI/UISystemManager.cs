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
        public static UISystemManager Instance;
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
            RegisterSystems();
        }
        #endregion

        public bool IsAnyUIOpen
        {
            get
            {
                foreach (var system in _uiSystems.Values)
                {
                    if (system.IsOpen)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Fired when gameplay is paused (true) or resumed (false)
        /// </summary>
        public event Action<bool> OnGameplayPaused = delegate { }; // should be in a separate system perhaps, but I'm running out of time for this project.


        private Dictionary<Type, UISystemBase> _uiSystems = new();
        private void RegisterSystems()
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
                if (result.IsOpen)
                {
                    return null;
                }
                foreach (var system in _uiSystems.Values)
                {
                    // close all exclusive systems that are open right now
                    // close all other systems if exclusive system is being opened
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

        public void CloseUISystem<T>() where T : UISystemBase
        {
            if (_uiSystems.TryGetValue(typeof(T), out var result))
            {
                if (!result.IsOpen)
                {
                    return;
                }
                result.Close();
            }
        }

        public void CloseAllUISystems()
        {
            foreach (var system in _uiSystems.Values)
            {
                system.Close();
            }
        }

        public void RegisterForUIEvent<T>(Action<UISystemBase, bool> toRegister) where T : UISystemBase
        {
            if (_uiSystems.TryGetValue(typeof(T), out var result))
            {
                result.OnOpenClose += toRegister;
            }
        }

        public void UnregisterForUIEvent<T>(Action<UISystemBase, bool> toDeregister) where T : UISystemBase
        {
            if (_uiSystems.TryGetValue(typeof(T), out var result))
            {
                result.OnOpenClose -= toDeregister;
            }
        }
    }
}