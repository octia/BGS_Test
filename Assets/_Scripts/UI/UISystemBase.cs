using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UISystemBase : MonoBehaviour
{
    [field:SerializeField]
    public bool IsExclusive {get; private set;}

    public bool IsOpen{get; private set;}

    public virtual void Initialize()
    {
        IsOpen = false;
    }

    public virtual void Open()
    {
        if (IsOpen)
        {
            return;
        }
        gameObject.SetActive(true);
        IsOpen = true;
        OnOpen();
        OnOpenClose.Invoke(this, true);
    }
    public virtual void Close()
    {
        if (!IsOpen)
        {
            return;
        }
        gameObject.SetActive(false);
        IsOpen = false;
        OnClose();
        OnOpenClose.Invoke(this, false);
    }
    
    public virtual void OnOpen(){}
    public virtual void OnClose(){}

    /// <summary>
    /// Fires when system is opened (true) or closed (false)
    /// Fires after OnOpen and OnClose 
    /// </summary>
    public event Action<UISystemBase, bool> OnOpenClose = delegate{};

}
