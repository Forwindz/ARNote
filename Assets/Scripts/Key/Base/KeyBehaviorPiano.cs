using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviorPiano : KeyBehavior
{

    public float speed = 1.0f;
    public float bounceDegree = 5.0f;
    [Header("Runtime")]
    [SerializeField]
    protected float animationProgress = 0.0f;
    [SerializeField]
    protected float curSpeed = 0.0f;

    public void Update()
    {
        //if(curSpeed != 0.0f)
        {
            float factor = bounceDegree - animationProgress;
            float dt = speed * Time.deltaTime*factor*10.0f;
            //animationProgress -= dt;

            float rx1 = animationProgress;

            curSpeed -= dt * speed;
            animationProgress -= curSpeed * Time.deltaTime;
            if(animationProgress>bounceDegree)
            {
                animationProgress = bounceDegree;
                curSpeed = 0.0f;
            }else if(animationProgress<0.0f)
            {
                animationProgress = 0.0f;
                curSpeed = 0.0f;
            }


            float rdx = animationProgress - rx1;
            Vector3 r = transform.localRotation.eulerAngles;
            transform.localRotation *= Quaternion.AngleAxis(rdx, new Vector3(1.0f, 0.0f, 0.0f));

        }
    }
    /*
    public float animationProgress
    {
        get => animationProgress_;
        set
        {
            float rx1 = Bounce(animationProgress) * bounceDegree;
            animationProgress = value;
            float rx2 = Bounce(animationProgress) * bounceDegree;

            float rdx = rx2 - rx1;
            Vector3 r = transform.localRotation.eulerAngles;
            transform.localRotation *= Quaternion.AngleAxis(rdx, new Vector3(1.0f, 0.0f, 0.0f));
        }
    }*/

    // Map 1->0  to 0.0->1.0->0.0
    protected float Bounce(float x)
    {
        const float FIRST_PHASE = 0.8f;
        
        if(x< FIRST_PHASE)
        {
            // 0.8~0 => 0.0~1.0 
            x = 1.0f - x / FIRST_PHASE;
        }
        else
        {
            // 1.0~0.8 => 1.0~0.0
            x = (x - FIRST_PHASE) / (1.0f - FIRST_PHASE);
        }
        x = x * x;
        return x;

    }
    public override void HitKey()
    {
        base.HitKey();
        curSpeed = 50.0f;
    }
}
