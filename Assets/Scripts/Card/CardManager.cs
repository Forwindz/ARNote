using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> cards = new List<Card>();

    public void AddCard(Card c)
    {
        cards.Add(c);
    }

    public void RemoveCard(Card c)
    {
        cards.Remove(c);
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<cards.Count;i++)
        {
            Card ci = cards[i];
            if(ci==null || !ci.enabled)
            {
                continue;
            }
            for(int j=i+1;j<cards.Count;j++)
            {
                Card cj = cards[j];
                if(cj==null || !cj.enabled)
                {
                    continue;
                }
                float distance = Vector3.Distance(ci.visualCard.transform.position, cj.visualCard.transform.position);
                if(distance<=ci.approachThreshold)
                {
                    ci.OnCardApproach(cj, distance);
                }
                if(distance<=cj.approachThreshold)
                {
                    cj.OnCardApproach(ci, distance);
                }
            }
        }
    }
}
