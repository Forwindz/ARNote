using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SheetPerformGame : MonoBehaviour
{
    public SheetCard sheetCard;
    public InstrumentCard instrumentCard;
    public SheetRender sheetRender;
    public SheetScoreCalc sheetScore;
    public TextMeshPro scoreMesh;

    private float prepareTime = 3.0f;


    [Header("Runtime")]
    private float score_ = 0;
    public int DisplayScore
    {
        get => (int)(score / sheetCard.sheetData.totalBaseScore * 100);
    }
    public float score
    {
        get => score_;
        set
        {
            score_ = value;
            if(isPlaying)
            {
                scoreMesh.text = "Score " + DisplayScore;
            }
        }
    }
    public bool isPlaying = false;
    public float time = 0.0f;

    void Start()
    {
        Init();
    }
    void Init()
    {
        Utils.FindComp(gameObject, ref sheetCard);
        Utils.FindComp(gameObject, ref sheetRender);
        Utils.FindComp(gameObject, ref sheetScore);
        instrumentCard = sheetCard.instrumentLink;
    }
    public void BeginPlay()
    {
        Init();
        isPlaying = true;
        prepareTime = sheetRender.prepareTime;
        time = -prepareTime;
        instrumentCard.onKeyClickEvent.AddListener(OnKeyClick);
        scoreMesh.gameObject.transform.position =
            instrumentCard.transform.position +
            Vector3.up;
        sheetCard.sheetVisualCard.DisplayText("Start!");
        score = 0;
    }

    public void EndPlay()
    {
        isPlaying = false;
        instrumentCard.onKeyClickEvent.RemoveListener(OnKeyClick);
        scoreMesh.text = "Final Score: " + DisplayScore;
        if(DisplayScore>90)
        {
            sheetCard.sheetVisualCard.DisplayText("Perfect Performance!");
        }
        else if (DisplayScore > 50)
        {
            sheetCard.sheetVisualCard.DisplayText("Great Performance!");
        }else
        {
            sheetCard.sheetVisualCard.DisplayText("Good Performance!");
        }



    }


    // Update is called once per frame
    void Update()
    {
        if(!isPlaying)
        {
            return;
        }
        time += Time.deltaTime;
        Vector3 offset = Vector3.down* sheetRender.basicBeatGap* 
            sheetCard.sheetData.TimeToBeat(Time.deltaTime);
        float disappearTime = time - sheetScore.missBeat;
        foreach (INoteRender nr in sheetRender.noteRenders)
        {
            nr.transform.position += offset;
            IBaseNote nd = nr.NoteData;
            if(nd.BeginTime<= disappearTime)
            {
                if(!nr.Hidden)
                {
                    nr.OnJudge(SheetScoreCalc.NoteJudge.MISS);
                }
                nr.gameObject.SetActive(false);
            }
        }
        if(time>=sheetCard.sheetData.totalTime+sheetScore.missBeat)
        {
            EndPlay();
        }
    }

    public void OnKeyClick(KeyBehavior kb)
    {
        int ind = kb.index;
        INoteRender nearestNote = null;
        float distance = 100000000.0f;
        bool found = false;
        foreach (INoteRender nr in sheetRender.noteRenders)
        {
            if(!nr.Hidden)
            {
                IBaseNote nd = nr.NoteData;
                if(nd.AudioIndex!=ind)
                {
                    continue;
                }
                float d = Mathf.Abs(nd.BeginTime - time);
                if(d<distance)
                {
                    distance = d;
                    nearestNote = nr;
                    found = true;
                }
                
            }
        }
        if(!found)
        {
            Debug.Log("Wrong Note!");
            score -= sheetScore.wrongNoteScore;
            return;
        }
        distance = sheetCard.sheetData.TimeToBeat(distance);
        SheetScoreCalc.NoteJudge nj = sheetScore.Judge(distance);
        score += sheetScore.GetScore(nearestNote.NoteData.BasicScore, nj);
        if (nj == SheetScoreCalc.NoteJudge.IGNORE)
        {
            return;
        }
        nearestNote.OnJudge(nj);
        nearestNote.Hidden = true;
        Debug.Log("Score: " + score + " | " + nj);
    }


}
