using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public VisualCard visualCard; //visualization of the card
    public float approachThreshold;

    public bool IsFront
    {
        get => visualCard.IsFront;
    }

    public bool IsBack
    {
        get => visualCard.IsBack;
    }

    public virtual void OnDoubleClick(RaycastHit hit)
    {
        Flip();
    }

    public virtual void OnReceiveMouseInfo(RaycastHit hit)
    {
    }


    //flip status
    public void Flip()
    {
        visualCard.Flip();
    }

    public virtual void OnMouseDownEvent(RaycastHit cacheHit)
    {
    }



    // Start is called before the first frame update
    protected virtual void Start()
    {
        Utils.FindComp(gameObject, ref visualCard);
        visualCard.card = this;
        CardManager cm = FindObjectOfType<CardManager>();
        if (cm != null)
        {
            cm.AddCard(this);
        }
    }

    protected virtual void OnDestroy()
    {
        CardManager cm = FindObjectOfType<CardManager>();
        if(cm!=null)
        {
            cm.RemoveCard(this);
        }
        
    }

    public virtual void OnCardApproach(Card other,float distance)
    {

    }

    public virtual void OnClick(RaycastHit hit)
    {

    }

    public virtual void OnFlip()
    {

    }

}
