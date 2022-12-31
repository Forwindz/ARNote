using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetCard : Card
{
    [SerializeField]
    private SheetData sheetData_;
    public SheetVisualCard sheetVisualCard
    {
        get => (SheetVisualCard)visualCard;
    }

    public SheetData sheetData
    {
        get => sheetData_;
        set {
            if(sheetData_!=value)
            {
                sheetData_ = value;
                if (sheetVisualCard != null)
                {
                    sheetVisualCard.UpdateSheetInfo();
                }
            }
            
        }
    }

    public bool IsEmptySheet
    {
        get => sheetData == null || sheetData.NoteCount == 0;
    }

    public SheetRender sheetRender;
    public SheetPerformGame sheetPerformGame;
    public SheetRecord sheetRecord;
    public SheetPlay sheetPlay;
    [Header("Card Link")]
    public InstrumentCard instrumentLink;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Utils.FindComp(gameObject, ref sheetRender);
        Utils.FindComp(gameObject, ref sheetPerformGame);
        Utils.FindComp(gameObject, ref sheetRecord);
        Utils.FindComp(gameObject, ref sheetPlay);
        UpdateDisplayState();
    }

    // Update is called once per frame
    void Update()
    {
        if(instrumentLink!=null)
        {
            float curDis = Vector3.Distance(visualCard.transform.position, instrumentLink.visualCard.transform.position);
            if(curDis>approachThreshold)
            {
                OnCardApproach(null, curDis);
            }
        }
    }


    

    public void InitSheet()
    {
        sheetRender.InitSheet();
    }

    public void BeginGame()
    {
        Utils.EnsureComp(gameObject, ref sheetPerformGame);
        sheetPerformGame.BeginPlay();
    }

    public void EndGame()
    {
        if(sheetPerformGame==null)
        {
            return;
        }
        sheetPerformGame.EndPlay();
    }

    public void BeginRecord()
    {
        Utils.EnsureComp(gameObject, ref sheetRecord);
        sheetRecord.BeginRecord();
    }

    public void EndRecord()
    {
        if (sheetRecord == null)
        {
            return;
        }
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

    public override void OnCardApproach(Card other, float distance)
    {
        if (other is InstrumentCard)
        {
            if(other != instrumentLink)
            {
                instrumentLink = (InstrumentCard)other;
                UpdateDisplayState();
            }else
            {
                instrumentLink = (InstrumentCard)other;
            }
            
        }
        if(other == null)
        {
            if (other != instrumentLink)
            {
                instrumentLink = null;
                UpdateDisplayState();
            }
        }
    }

    public override void OnClick(RaycastHit hit)
    {
        StateChange();
    }

    public override void OnFlip()
    {
        UpdateDisplayState();
    }

    public void UpdateDisplayState()
    {
        if(instrumentLink==null)
        {
            sheetVisualCard.DisplayText("Link to an instrument Card to use this card :)");
            return;
        }
        if(sheetPerformGame!=null && sheetPerformGame.isPlaying)
        {
            EndGame();
        }

        if (sheetRecord != null && sheetRecord.IsRecording)
        {
            EndRecord();
        }

        if (sheetPlay != null && sheetPlay.isPlaying)
        {
            StopPlay();
        }
        
        
        if (IsFront)
        {
            if (IsEmptySheet)
            {
                if (sheetRecord == null)
                {
                    sheetVisualCard.DisplayText("Touch to Create your own music!");
                }
                else
                {
                    if (sheetRecord.IsRecording)
                    {
                    }
                    else
                    {
                        sheetVisualCard.DisplayText("Touch to Create your own music!");
                    }
                }
            }
            else
            {
                if (sheetPlay == null)
                {
                    sheetVisualCard.DisplayText("Touch to Play the music!");
                }
                else
                {
                    if (sheetPlay.isPlaying)
                    {
                    }
                    else
                    {
                        sheetVisualCard.DisplayText("Touch to Play the music!");
                    }
                }
            }


        }
        else
        {
            if (!IsEmptySheet)
            {
                if (sheetPerformGame == null)
                {
                    sheetVisualCard.DisplayText("Touch to Perform sheet by yourself!");
                }
                else
                {
                    if (sheetPerformGame.isPlaying)
                    {
                    }
                    else
                    {
                        sheetVisualCard.DisplayText("Touch to Perform sheet by yourself!");
                    }
                }
            }
            else
            {
                sheetVisualCard.DisplayText("Empty Sheet, flip card, touch it, and create your own sheet!");
            }

        }
    }

    public void StateChange()
    {
        if (instrumentLink == null)
        {
            return;
        }

        if (IsFront)
        {
            if (IsEmptySheet)
            {
                if (sheetRecord == null)
                {
                    BeginRecord();
                }
                else
                {
                    if (sheetRecord.IsRecording)
                    {
                        EndRecord();
                    }
                    else
                    {
                        BeginRecord();
                    }
                }
            }
            else
            {
                if (sheetPlay == null)
                {
                    BeginPlay();
                }
                else
                {
                    if (sheetPlay.isPlaying)
                    {
                        StopPlay();
                    }
                    else
                    {
                        BeginPlay();
                    }
                }
            }
            

        }
        else
        {
            if (!IsEmptySheet)
            {
                if (sheetPerformGame == null)
                {
                    BeginGame();
                }
                else
                {
                    if (sheetPerformGame.isPlaying)
                    {
                        EndGame();
                    }
                    else
                    {
                        BeginGame();
                    }
                }
            }else
            {
                sheetVisualCard.DisplayText("Empty Sheet, flip card, touch it, and create your own sheet!");
            }

        }
    }
}
