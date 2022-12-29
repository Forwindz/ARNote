using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

interface IGetResources
{
    Material GetMaterial(int index);
    string GetDisplayText(int index);
    AudioClip GetSound(int index);
    
}
