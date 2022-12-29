using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SheetBuildHelper
{
    public SheetData sheet;
    public float curPointer = 0.0f;
    public float bpm
    {
        get => sheet.bpm;
        set => sheet.bpm = value;
    }

    public float beatLen
    {
        get => 60.0f / bpm;
    }
    public SheetBuildHelper(string name)
    {
        sheet = new SheetData(name);
    }

    public void AddSingleNote(float len, int index)
    {
        float newLen = beatLen* len;
        sheet.AddNote(new HitNote(curPointer, index));
        curPointer += newLen;
    }

    public void AddBlank(float len)
    {
        float newLen = beatLen * len;
        curPointer += newLen;
    }

    // Music:
    // _ represent 1/2
    // __ represent 1/4
    // - represent 2
    // -- represent 4
    // number: represent do re mi fa so la xi
    // 0: no note here
    // |: no effect

    // example:
    // 1 2_ 2 3_ 4 | 5-- | 5- 2__ 3__ 2_ 0 |
    public void AddNotes(string content)
    {
        content = content.Replace('|', ' ');
        string[] tokens = content.Split(" ");
        foreach(string token in tokens)
        {
            string t = token.Trim();
            if(t.Length==0)
            {
                continue;
            }
            int halfCount = 0;
            int plusCount = 0;
            float finalLen = 1.0f;
            foreach(char c in t)
            {
                switch(c)
                {
                    case '-':
                        plusCount++;
                        finalLen *= 2.0f;
                        break;
                    case '_':
                        halfCount++;
                        finalLen *= 0.5f;
                        break;
                }
            }
            int index = t[0] - '0';
            if(index==0)
            {
                AddBlank(finalLen);
            }
            else
            {
                AddSingleNote(finalLen, index - 1);
            }
        }
    }
}
