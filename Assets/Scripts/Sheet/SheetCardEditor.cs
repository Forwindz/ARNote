
using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SheetCard))]
class SheetCardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SheetCard c = Selection.activeGameObject.GetComponent<SheetCard>();
        if (c!=null)
        {
            if(c.instrumentLink!=null)
            {
                CheckFunction(c.BeginGame);
                CheckFunction(c.EndGame);
                CheckFunction(c.BeginRecord);
                CheckFunction(c.EndRecord);
                CheckFunction(c.BeginPlay);
                CheckFunction(c.StopPlay);
            }
        }
    }

    public void CheckFunction(Action action)
    {
        if (GUILayout.Button("Invoke "+ action.Method.Name))
        {
            action.Invoke();
        }
    }
}
