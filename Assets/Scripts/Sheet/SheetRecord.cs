using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetRecord : MonoBehaviour
{
    private bool isRecording =false;

    public SheetCard sheetCard;
    [SerializeField]
    protected SheetData tempSheet;
    [SerializeField]
    protected float time;

    public bool IsRecording
    {
        get => isRecording;
    }

    public void BeginRecord()
    {
        Utils.FindComp(gameObject, ref sheetCard);
        isRecording = true;
        sheetCard.instrumentLink.onKeyClickEvent.AddListener(OnKeyClick);
        tempSheet = new SheetData("customSheet_"+Random.Range(0,10000));
        tempSheet.bpm = 60;
        time = 0.0f;
        sheetCard.sheetVisualCard.DisplayText("Recording");
    }

    public void EndRecord()
    {
        isRecording = false;
        sheetCard.sheetData = tempSheet;
        sheetCard.sheetVisualCard.DisplayText("You've made a new sheet!");
    }

    // Update is called once per frame
    void Update()
    {
        if(isRecording)
        {
            time += Time.deltaTime;
        }
    }

    public void OnKeyClick(KeyBehavior kb)
    {
        if(!isRecording)
        {
            return;
        }
        tempSheet.AddNote(new HitNote(time, kb.index));
        sheetCard.sheetVisualCard.DisplayText("Recording "+tempSheet.notes+" Notes");
    }
}
