using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualCard : MonoBehaviour
{
    public Card card;

    public bool isMoveable = true; // move by mouse
    [SerializeField]
    private bool fliped_;

    public bool IsFront
    {
        get => !fliped_;
    }

    public bool IsBack
    {
        get => fliped_;
    }

    private float flipAnimationTime = 0.0f;

    public void Flip()
    {
        flipAnimationTime = 1.0f;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (flipAnimationTime > 0)
        {
            float factor = Time.deltaTime * (1.2f - flipAnimationTime);
            float dt = Mathf.Min(flipAnimationTime, factor);
            transform.rotation *=
                Quaternion.AngleAxis(dt * 180.0f, Vector3.right);
            flipAnimationTime -= dt;
        }
        bool oldFlip = fliped_;
        fliped_ = Vector3.Dot(transform.up, Camera.main.transform.forward) > 0.0f;
        if(oldFlip!=fliped_)
        {
            card.OnFlip();
        }

        if(isDragging)
        {
            Vector3 dp = Input.mousePosition - lastMousePosition;
            lastMousePosition = Input.mousePosition;
            card.transform.position += dp * 0.0018f* distanceView;
        }
    }


    private bool isDragging = false;
    private Vector3 lastMousePosition;
    private float distanceView = 0.0f;

    public void OnDragBegin(RaycastHit hit)
    {
        if(!isMoveable)
        {
            return;
        }
        isDragging = true;
        lastMousePosition = Input.mousePosition;
        distanceView = hit.distance;

        CameraControl cc = FindObjectOfType<CameraControl>();
        if(cc!=null)
        {
            cc.enableRight = false;
        }
    }

    public void OnDragEnd()
    {
        isDragging = false;
        CameraControl cc = FindObjectOfType<CameraControl>();
        if (cc != null)
        {
            cc.enableRight = true;
        }
    }
}
