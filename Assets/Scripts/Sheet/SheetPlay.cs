using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetPlay : MonoBehaviour
{
    public SheetCard sheetCard;
    public SheetRender sheetRender;

    public float time = 0.0f;
    public int playIndex = 0;
    public bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        Utils.FindComp(gameObject, ref sheetCard);
        Utils.FindComp(gameObject, ref sheetRender);
    }

    public void Play()
    {
        isPlaying = true;
        sheetCard.sheetVisualCard.DisplayText("Playing");
        sheetRender.InitSheet();
    }

    public void Pause()
    {
        isPlaying = false;
    }

    public void ResetPlayProgress()
    {
        time = 0.0f;
        playIndex = 0;
        isPlaying = false;
        sheetCard.sheetVisualCard.DisplayText("End");
        sheetRender.ClearRender();
    }


    // Update is called once per frame
    void Update()
    {
        if(isPlaying)
        {
            time += Time.deltaTime;
            SheetData sheet = sheetCard.sheetData;
            while (playIndex < sheet.NoteCount && time >= sheet[playIndex].BeginTime)
            {
                sheetCard.instrumentLink.Play(sheet[playIndex].AudioIndex);
                playIndex++;
            }
            sheetCard.sheetVisualCard.DisplayText("Playing " + (int)(sheet.totalTime - time) + " s");

            foreach (INoteRender nr in sheetRender.noteRenders)
            {
                if(!nr.enabled)
                {
                    continue;
                }
                IBaseNote nd = nr.NoteData;
                sheetRender.SetNotePosition(nr, sheetCard.sheetData.TimeToBeat(nd.BeginTime - time));
                if (nd.BeginTime <= time)
                {
                    nr.OnJudge(SheetScoreCalc.NoteJudge.PERFECT);
                }
                if (nd.BeginTime <= time-1.0f)
                {
                    nr.enabled = false;
                }
            }

            if (time>sheet.totalTime)
            {
                ResetPlayProgress();
            }
        }
        
    }
}
