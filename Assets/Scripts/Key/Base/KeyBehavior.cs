using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehavior : MonoBehaviour
{
    public int index;
    public GameObject hitEffect;
    public Vector3 effectBias;

    protected MeshRenderer meshRenderer;
    protected AudioSource audioSource;
    [Header("Click State")]
    public bool isPressed = false;//not used
    
    // Start is called before the first frame update
    void Start()
    {
        Utils.EnsureComp(gameObject, ref meshRenderer);
        Utils.EnsureComp(gameObject, ref audioSource);
    }

    public void SetupKey(KeyResources resources)
    {
        Utils.EnsureComp(gameObject, ref meshRenderer);
        Utils.EnsureComp(gameObject, ref audioSource);
        meshRenderer.material = resources.GetMaterial(index);
        audioSource.clip = resources.GetSound(index);
    }

    public virtual void HitKey()
    {
        audioSource.Play();
        if(hitEffect!=null)
        {
            GameObject go = Instantiate(hitEffect);
            go.transform.position = transform.position+effectBias;
            go.transform.rotation *= transform.rotation;
        }
    }
}
