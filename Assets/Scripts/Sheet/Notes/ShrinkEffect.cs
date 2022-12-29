using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkEffect : MonoBehaviour
{
    public float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= Vector3.one*speed * Time.deltaTime;
        if(
            transform.localScale.x<=0 ||
            transform.localScale.y<=0 ||
            transform.localScale.z<=0
            )
        {
            gameObject.SetActive(false);
            transform.localScale = Vector3.zero;
            Destroy(this);
        }
    }
}
