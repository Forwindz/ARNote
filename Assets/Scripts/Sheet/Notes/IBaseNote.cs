using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IBaseNote
{
    public float BeginTime
    {
        get;
    }

    // when the note end
    public float EndTime
    {
        get;
    }

    public int AudioIndex
    {
        get;
    }

    // basic score of the note
    public int BasicScore
    {
        get;
    }

}
