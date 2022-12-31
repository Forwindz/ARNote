using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkRender : MonoBehaviour
{
    public LineRenderer lineRenderer;

    [SerializeField]
    protected Vector3 p1;
    [SerializeField]
    protected Vector3 p2;
    // Start is called before the first frame update
    void Start()
    {
        Utils.FindComp(gameObject, ref lineRenderer);
    }

    public Vector3 Pos1
    {
        get => p1;
        set
        {
            if(p1!=value)
            {
                p1 = value;
                UpdatePosition();
            }
        }
    }

    public Vector3 Pos2
    {
        get => p2;
        set
        {
            if (p2 != value)
            {
                p2 = value;
                UpdatePosition();
            }
        }
    }

    public bool EnableRender
    {
        get => lineRenderer.enabled;
        set => lineRenderer.enabled = value;
    }

    protected void UpdatePosition()
    {
        lineRenderer.SetPosition(0, p1);
        lineRenderer.SetPosition(1, p2);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    
    //}
}
