using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetRender : MonoBehaviour
{
    [Header("Card")]
    public SheetCard sheetCard;
    public InstrumentCard instrumentCard;
    [Header("Scan line")]
    public GameObject sheetScanlineTemplate = null;
    [Header("Note")]
    public INoteRender noteTemplate = null;
    public Vector3 noteBias;
    public float basicBeatGap = 0.2f;
    public List<INoteRender> noteRenders;
    public float prepareTime = 3.0f;
    [Header("Runtime attribute")]
    public GameObject renderObj;
    public bool inited = false;

    

    // Start is called before the first frame update
    void Start()
    {
        Utils.FindComp(gameObject, ref sheetCard);
    }


    public void InitSheet(InstrumentCard instrumentCard)
    {
        Utils.FindComp(gameObject, ref sheetCard);
        this.instrumentCard = instrumentCard;
        if(renderObj!= null)
        {
            Destroy(renderObj);
        }

        renderObj = new GameObject("SheetRenderObj");
        renderObj.transform.parent = transform;
        noteRenders = new List<INoteRender>();
        SheetData data = sheetCard.sheetData;

        for(int i=0;i<data.NoteCount;i++)
        {
            IBaseNote note = data[i];
            GameObject obj = Instantiate(noteTemplate.gameObject, transform);
            INoteRender r = null;
            Utils.FindComp(obj, ref r);

            // basic position
            KeyBehavior kb = instrumentCard.keys[note.AudioIndex];
            obj.transform.position = kb.transform.position + instrumentCard.keyCenterOffset+noteBias;
            obj.transform.rotation *= instrumentCard.transform.rotation;

            // time offset -> position offset
            float beat = data.TimeToBeat(note.BeginTime+prepareTime);
            obj.transform.position+=Vector3.up*beat*basicBeatGap;
            obj.name = "note_" + note.AudioIndex + "_" + (int)(beat*1000);
            obj.transform.parent = renderObj.transform;

            r.NoteData = note;
            r.sheetRender = this;
            r.InitNote();

            noteRenders.Add(r);
        }

        inited = true;
    }

}
