using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyClick : OnClickInvoke
{
    public KeyBehavior keyBehavior;
    public InstrumentCard card;
    public override void OnMouseDownEvent(RaycastHit hit)
    {
        card.Play(keyBehavior.index);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(keyBehavior==null)
        {
            Utils.EnsureComp(gameObject, ref keyBehavior);
        }
    }

}
