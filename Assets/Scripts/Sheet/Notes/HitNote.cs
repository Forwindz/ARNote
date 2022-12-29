using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitNote : IBaseNote
{
    public float hitTime;
    public int audioIndex;

    public HitNote(float hitTime, int index)
    {
        this.hitTime = hitTime;
        this.audioIndex = index;
    }

    public int BasicScore => 100;

    public float BeginTime => hitTime;

    public int AudioIndex => audioIndex;

    float IBaseNote.EndTime => hitTime;

}
