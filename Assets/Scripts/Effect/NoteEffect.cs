using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEffect : MonoBehaviour
{
    public float time = 1.0f;
    public float speed = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        transform.position += Vector3.up * speed;
        speed *= 0.96f;
        if(time<=0)
        {
            Destroy(gameObject);
        }
    }

}
