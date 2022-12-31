using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCardClick : OnClickInvoke
{
    public VisualCard visualCard;
    public override void OnClick(RaycastHit hit)
    {
        visualCard.card.OnClick(hit);
    }

    public override void OnDoubleClick(RaycastHit hit)
    {
        visualCard.card.OnDoubleClick(hit);
    }

    public override void OnReceiveMouseInfo(RaycastHit hit)
    {
        visualCard.card.OnReceiveMouseInfo(hit);
        if(Input.GetMouseButtonDown(1))
        {
            visualCard.OnDragBegin(hit);
        }
        
    }

    public override void OnMouseDownEvent(RaycastHit cacheHit)
    {
        visualCard.card.OnMouseDownEvent(cacheHit);
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonUp(1))
        {
            visualCard.OnDragEnd();
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        Utils.FindComp(gameObject, ref visualCard);
    }
}
