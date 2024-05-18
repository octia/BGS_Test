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
        OnOpen();
    }
    public virtual void Close()
    {
        if (!IsOpen)
        {
            return;
        }
        gameObject.SetActive(false);
        OnClose();
    }
    
    public virtual void OnOpen(){}
    public virtual void OnClose(){}

}
