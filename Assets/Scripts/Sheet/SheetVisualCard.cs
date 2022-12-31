using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SheetVisualCard : VisualCard
{
    public SheetCard sheetCard
    {
        get => (SheetCard)card;
    }
    public MeshRenderer meshRenderer;
    public Material emptySheetMaterial;
    public Material fullSheetMaterial;
    public TextMeshPro frontText, backText;
    public LinkRender linkRenderer;
    // Start is called before the first frame update
    void Start()
    {
        Utils.FindComp(gameObject, ref meshRenderer);
        Utils.FindComp(gameObject, ref frontText, "FrontInfo");
        Utils.FindComp(gameObject, ref backText, "BackInfo");
        Utils.FindComp(gameObject, ref linkRenderer, "Link");
    }

    public void SetVisualEmpty()
    {
        meshRenderer.material = emptySheetMaterial;
    }

    public void SetVisualFull()
    {
        meshRenderer.material = fullSheetMaterial;
    }
    
    public string FrontText
    {
        get => frontText.text;
        set => frontText.text = value;
    }

    public string BackText
    {
        get => backText.text;
        set => backText.text = value;
    }

    public void DisplayText(string s)
    {
        Debug.Log("VisualSheetCard: " + s);
        if (IsFront)
        {
            if(frontText!=null)
            {
                FrontText = s;
            }
            
        }else
        {
            if (backText != null)
                BackText = s;
        }
    }



    public void UpdateSheetInfo()
    {
        if(sheetCard.IsEmptySheet)
        {
            SetVisualEmpty();
        }
        else
        {
            SetVisualFull();
        }
    }



    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(linkRenderer!=null)
        {
            if(sheetCard.instrumentLink==null)
            {
                linkRenderer.EnableRender = false;
            }
            else
            {
                linkRenderer.EnableRender = true;
                linkRenderer.Pos2 = transform.position;
                linkRenderer.Pos1 = sheetCard.instrumentLink.visualCard.transform.position;
            }
        }
    }
}
