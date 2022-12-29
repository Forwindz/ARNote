
using UnityEngine;

public abstract class INoteRender : MonoBehaviour
{
    public SheetRender sheetRender;
    public virtual IBaseNote NoteData
    {
        get;set;
    }

    public bool Hidden
    {
        get; set;
    }

    public abstract void InitNote();

    public abstract void OnJudge(SheetScoreCalc.NoteJudge judge);

}
