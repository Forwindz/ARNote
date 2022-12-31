using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnClickInvoke : MonoBehaviour
{
    public float doubleClickInterval = 0.5f;

    private float lastClickTime = -100.0f;
    private RaycastHit cacheHit;

    public virtual void OnDoubleClick(RaycastHit hit)
    {

    }

    public virtual void OnClick(RaycastHit hit)
    {

    }

    public virtual void OnReceiveMouseInfo(RaycastHit hit)
    {

    }

    public void ProcessMouseInfo(RaycastHit hit)
    {
        OnReceiveMouseInfo(hit);
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse down :" + gameObject.name);
            OnMouseDownEvent(cacheHit);
            if (lastClickTime>0 && Time.time-lastClickTime<=doubleClickInterval)
            {
                Debug.Log("Double click :" + gameObject.name);
                OnDoubleClick(hit);
                lastClickTime = -1;
            }
            else
            {
                cacheHit = hit;
                lastClickTime = Time.time;
            }
            
        }
    }

    public virtual void OnMouseDownEvent(RaycastHit cacheHit)
    {
    }

    public virtual void OnFocus(RaycastHit cacheHit)
    {

    }

    public virtual void OnLoseFocus(RaycastHit cacheHit)
    {

    }

    public virtual void Update()
    {
        if(lastClickTime > 0 && Time.time-lastClickTime>doubleClickInterval)
        {
            Debug.Log("Mouse click :" + gameObject.name);
            OnClick(cacheHit);
            lastClickTime = -1;
        }
    }
}
