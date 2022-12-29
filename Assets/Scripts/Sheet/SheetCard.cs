using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetCard : MonoBehaviour
{
    [SerializeField]
    private SheetData sheetData_;

    public SheetData sheetData
    {
        get => sheetData_;
        set {
            sheetData_ = value;
            /*if(sheetRender!=null && sheetRender.inited && instrumentLink!=null)
            {
                InitSheet();
            }*/
        }
    }

    public SheetRender sheetRender;
    public SheetPerformGame sheetPerformGame;
    public SheetRecord sheetRecord;
    public SheetPlay sheetPlay;
    [Header("Card Link")]
    public InstrumentCard instrumentLink;
    // Start is called before the first frame update
    void Start()
    {
        Utils.FindComp(gameObject, ref sheetRender);
        Utils.FindComp(gameObject, ref sheetPerformGame);
        Utils.FindComp(gameObject, ref sheetRecord);
        Utils.FindComp(gameObject, ref sheetPlay);
        //InitGame(instrumentLink);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitSheet()
    {
        sheetRender.InitSheet(instrumentLink);
    }

    public void BeginGame()
    {
        InitSheet();
        Utils.EnsureComp(gameObject, ref sheetPerformGame);
        sheetPerformGame.BeginPlay();
    }

    public void EndGame()
    {
        sheetPerformGame.EndPlay();
    }

    public void BeginRecord()
    {
        Utils.EnsureComp(gameObject, ref sheetRecord);
        sheetRecord.BeginRecord();
    }

    public void EndRecord()
    {
        sheetRecord.EndRecord();
    }

    public void BeginPlay()
    {
        Utils.EnsureComp(gameObject, ref sheetPlay);
        sheetPlay.Play();
    }

    public void PausePlay()
    {
        if(sheetPlay==null)
        {
            return;
        }
        sheetPlay.Pause();
    }

    public void StopPlay()
    {
        if (sheetPlay == null)
        {
            return;
        }
        sheetPlay.ResetPlayProgress();
    }
}
