using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringSheet : MonoBehaviour
{
    public SheetCard card;
    public string sheet;
    public string sheetName = "sheet_9";
    public float bpm = 120;
    // Start is called before the first frame update
    void Start()
    {
        if(card==null)
        {
            card = gameObject.GetComponent<SheetCard>();
        }
        if(card==null)
        {
            return;
        }
        SheetBuildHelper helper = new SheetBuildHelper(sheetName);
        helper.bpm = bpm;
        helper.AddNotes(sheet);
        card.sheetData = helper.sheet;
    }
}
