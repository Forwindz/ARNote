using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Click2PlayKey : MonoBehaviour
{
    public UnityEvent<RaycastHit> clickEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                clickEvent.Invoke(hit);
                OnClickInvoke oc = hit.transform.GetComponent<OnClickInvoke>();
                if(oc!=null)
                {
                    oc.OnClick(hit);
                }
            }
        }
    }
}
