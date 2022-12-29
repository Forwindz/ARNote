using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetScoreCalc : MonoBehaviour
{
    public enum NoteJudge
    {
        IGNORE,
        MISS,
        GOOD,
        GREAT,
        PERFECT
    }
    public float missBeat = 4.0f;
    public float goodBeat = 2.0f;
    public float greatBeat = 1.0f;
    public float perfectBeat = 0.5f;

    public float goodScore = 0.7f;
    public float greatScore = 0.9f;
    public float perfectScore = 1.0f;

    public int wrongNoteScore = 10;

    public NoteJudge Judge(float deltaBeat)
    {
        deltaBeat = Mathf.Abs(deltaBeat);
        if(deltaBeat<perfectBeat)
        {
            return NoteJudge.PERFECT;
        }else if (deltaBeat < greatBeat)
        {
            return NoteJudge.GREAT;
        }
        else if (deltaBeat < goodBeat)
        {
            return NoteJudge.GOOD;
        }else
        {
            return NoteJudge.MISS;
        }
    }

    public float GetScoreRatio(NoteJudge n)
    {
        return n switch
        {
            NoteJudge.GOOD => goodScore,
            NoteJudge.GREAT => greatScore,
            NoteJudge.PERFECT => perfectScore,
            _ => 0.0f,
        };
    }

    public int GetScore(int baseScore, NoteJudge judge)
    {
        return (int)(baseScore * GetScoreRatio(judge));
    }



}
