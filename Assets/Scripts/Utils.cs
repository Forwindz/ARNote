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
                if (comp == null)
                {
                    comp = obj.transform.GetComponentInParent<T>();
                }
            }
        }
    }

    public static void FindComp<T>(GameObject obj, ref T comp, string objName) where T : Component
    {
        if (comp == null)
        {
            comp = obj.GetComponent<T>();
            if (comp == null)
            {
                //search child
                T[] candidates = obj.transform.GetComponentsInChildren<T>();
                if (candidates != null)
                {
                    foreach (T c in candidates)
                    {
                        if (c.gameObject.name == objName)
                        {
                            comp = c;
                            return;
                        }
                    }
                }
                //search father
                T[] candidates2 = obj.transform.GetComponentsInParent<T>();
                if (candidates2 != null)
                {
                    foreach (T c in candidates2)
                    {
                        if (c.gameObject.name == objName)
                        {
                            comp = c;
                            return;
                        }
                    }
                }

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
