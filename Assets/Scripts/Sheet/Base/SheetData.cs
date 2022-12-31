using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class SheetData
{
    public string name;
    public float totalTime;
    public float bpm;
    public int totalBaseScore;
    public List<IBaseNote> notes = new List<IBaseNote>();

    public float NoteCount
    {
        get => notes == null ? 0 : notes.Count;
    }

    public float BeatTime
    {
        get => 60.0f / bpm;
    }

    public IBaseNote this[int index]
    {
        get => notes[index];
        set => notes[index] = value;
    }

    public SheetData(string name)
    {
        this.name = name;
        notes = new List<IBaseNote>();
    }

    public void AddNote(IBaseNote note)
    {
        totalTime = Math.Max(note.EndTime, totalTime);
        notes.Add(note);
        totalBaseScore += note.BasicScore;
    }

    public void SortNote()
    {
        notes.Sort(new Comparison<IBaseNote>(Compare));
    }

    public float TimeToBeat(float time)
    {
        return time / BeatTime;
    }

    public float BeatToTime(float beat)
    {
        return beat * BeatTime;
    }

    int Compare(IBaseNote a,IBaseNote b)
    {
        return (int)(a.BeginTime - b.BeginTime);
    }

    
}

