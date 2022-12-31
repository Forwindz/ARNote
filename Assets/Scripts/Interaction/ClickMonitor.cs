using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickMonitor : MonoBehaviour
{
    public UnityEvent<RaycastHit> clickEvent;
    private OnClickInvoke lastFocus = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                clickEvent.Invoke(hit);
                OnClickInvoke oc = hit.transform.GetComponent<OnClickInvoke>();
                if(oc!=null)
                {
                    if(lastFocus!=oc)
                    {
                        if(lastFocus!=null)
                        {
                            lastFocus.OnLoseFocus(hit);
                        }
                        lastFocus = oc;
                        oc.OnFocus(hit);
                    }
                    oc.ProcessMouseInfo(hit);
                }
            }
            else
            {
                if (lastFocus != null)
                {
                    lastFocus.OnLoseFocus(hit);
                    lastFocus = null;
                }
            }
        }
    }
}
