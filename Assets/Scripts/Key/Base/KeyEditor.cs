using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KeyResources))]
public class KeyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate objects"))
        {
            GameObject target = Selection.activeGameObject;
            KeyResources res = target.GetComponent<KeyResources>();
            if(res==null)
            {
                Debug.LogError("Target gameobject does not have KeyResources Component");
                return;
            }
            if(res.template==null)
            {
                Debug.LogError("Target gameobject's KeyResources does not have a template");
                return;
            }
            GameObject parent = new GameObject("InstrumentCard_" + res.resouceName);
            InstrumentCard card = parent.AddComponent<InstrumentCard>();

            card.keyCenterOffset = res.centerOffset;
            card.resources = res;
            
            for(int i=0;i<res.audioClips.Count;i++)
            {
                GameObject newObj = Instantiate(res.template);
                newObj.transform.parent = parent.transform;
                newObj.transform.position += res.offset * i;

                KeyBehavior kb=null;
                Utils.EnsureComp(newObj, ref kb);
                kb.index = i;
                kb.SetupKey(res);
                kb.name = res.template.name + "_" + i;

                KeyClick kc = null;
                Utils.EnsureComp(newObj, ref kc);
                kc.keyBehavior = kb;
                kc.card = card;

                card.keys.Add(kb);
            }
        }
    }
}
