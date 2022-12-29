using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyResources : MonoBehaviour, IGetResources
{
    public List<string> displayText = new();
    public List<Material> materials = new();
    public List<AudioClip> audioClips = new();

    [Header("Generation setup")]

    public string resouceName = "keyResource";
    public GameObject template;
    public Vector3 offset = Vector3.zero;
    public Vector3 centerOffset = Vector3.zero;

    public string GetDisplayText(int index)
    {
        return displayText[index];
    }

    public Material GetMaterial(int index)
    {
        return materials[index];
    }

    public AudioClip GetSound(int index)
    {
        return audioClips[index];
    }


}
