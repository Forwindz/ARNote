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
    // Start is called before the first frame update
    void Start()
    {
        Utils.FindComp(gameObject, ref meshRenderer);
        Utils.FindComp(gameObject, ref frontText, "FrontInfo");
        Utils.FindComp(gameObject, ref backText, "BackInfo");
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
            FrontText = s;
        }else
        {
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
    //void Update()
    //{

    //}
}
