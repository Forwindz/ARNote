using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitNoteRender : INoteRender
{
    public MeshRenderer meshRenderer;

    public override void InitNote()
    {
        Utils.FindComp(gameObject, ref meshRenderer);
        meshRenderer.material = 
            sheetRender.instrumentCard.resources.GetMaterial(NoteData.AudioIndex);
    }

    public override void OnJudge(SheetScoreCalc.NoteJudge judge)
    {
        if(SheetScoreCalc.NoteJudge.IGNORE==judge)
        {
            return;
        }
        ShrinkEffect e = gameObject.AddComponent<ShrinkEffect>();
        switch (judge)
        {
            case SheetScoreCalc.NoteJudge.MISS:
                e.speed = 1.0f;
                transform.localScale *= 0.2f;
                meshRenderer.material.color = Color.red;
                break;
            case SheetScoreCalc.NoteJudge.GOOD:
                e.speed = 4.0f;
                transform.localScale *= 0.5f;
                meshRenderer.material.color = Color.black;
                break;
            case SheetScoreCalc.NoteJudge.GREAT:
                e.speed = 16.0f;
                transform.localScale *= 0.75f;
                meshRenderer.material.color = Color.grey;
                break;
            case SheetScoreCalc.NoteJudge.PERFECT:
                e.speed = 50.0f;
                transform.localScale *= 0.9f;
                meshRenderer.material.color = Color.white;
                break;
        }
    }
}
