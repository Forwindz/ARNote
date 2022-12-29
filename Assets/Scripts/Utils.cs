using System;
using System.Reflection;
using UnityEngine;
class Utils
{
    public static void EnsureComp<T>(GameObject obj, ref T comp) where T : Component
    {
        comp = obj.GetComponent<T>();
        if(comp==null)
        {
            comp = obj.AddComponent<T>();
        }
    }

    public static void FindComp<T>(GameObject obj, ref T comp) where T : Component
    {
        if (comp == null)
        {
            comp = obj.GetComponent<T>();
            if(comp==null)
            {
                comp = obj.transform.GetComponentInChildren<T>();
            }
        }
    }

    public static void InvokeMethod<T>(T obj, string methodName)
    {
        Type tp = typeof(T);
        MethodInfo method = tp.GetMethod(methodName);
        method?.Invoke(obj, null);
    }

}
