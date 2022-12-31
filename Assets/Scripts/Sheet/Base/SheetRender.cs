using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetRender : MonoBehaviour
{
    [Header("Card")]
    public SheetCard sheetCard;
    public InstrumentCard instrumentCard
    {
        get => sheetCard.instrumentLink;
    }
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

    public void ClearRender()
    {
        if (renderObj != null)
        {
            Destroy(renderObj);
        }
    }

    public void InitSheet()
    {
        Utils.FindComp(gameObject, ref sheetCard);
        ClearRender();

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
            obj.transform.position = HitPos(note.AudioIndex);
            KeyBehavior kb = instrumentCard.keys[note.AudioIndex];
            obj.transform.rotation *= instrumentCard.transform.rotation;

            // time offset -> position offset
            float beat = data.TimeToBeat(note.BeginTime+prepareTime);
            obj.transform.position+=OffsetPos(beat * basicBeatGap);
            obj.name = "note_" + note.AudioIndex + "_" + (int)(beat*1000);
            obj.transform.parent = renderObj.transform;

            r.NoteData = note;
            r.sheetRender = this;
            r.InitNote();

            noteRenders.Add(r);
        }

        inited = true;
    }

    public Vector3 OffsetPos(float distance)
    {
        if(distance<0.6f)
        {
            return Vector3.up * distance*1.5f;
        }
        else
        {
            return Vector3.up*0.6f * 1.5f + (distance- 0.6f) *(transform.forward+transform.up*0.2f).normalized;
        }
    }

    public Vector3 HitPos(int audioIndex)
    {
        KeyBehavior kb = instrumentCard.keys[audioIndex];
        return kb.transform.position + instrumentCard.keyCenterOffset + noteBias;
    }

    public void SetNotePosition(INoteRender r,float offset)
    {
        r.transform.position = HitPos(r.NoteData.AudioIndex) + OffsetPos(offset);
    }

    public void DisplayScanline(bool b)
    {
        if(sheetScanlineTemplate==null)
        {
            return;
        }
        sheetScanlineTemplate.SetActive(b);
        if(b)
        {
            Collider c = sheetScanlineTemplate.GetComponent<Collider>();
            Vector3 bias = Vector3.zero;
            if(c!=null)
            {
                bias = c.bounds.size * 0.5f;
            }
            KeyBehavior kb = instrumentCard.keys[0];
            sheetScanlineTemplate.transform.position = kb.transform.position + Vector3.up * 0.9f + bias - noteBias;
        }
    }

}
