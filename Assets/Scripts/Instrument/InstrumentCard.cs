using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstrumentCard : Card
{
    public KeyResources resources;
    public List<KeyBehavior> keys = new List<KeyBehavior>();
    public Vector3 keyCenterOffset;

    public UnityEvent<KeyBehavior> onKeyClickEvent = new UnityEvent<KeyBehavior>();

    public void Play(int index)
    {
        keys[index].HitKey();
        onKeyClickEvent.Invoke(keys[index]);
    }


}
