using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform box;
    [SerializeField] Rigidbody2D boxRB;

    bool pickedup = false;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GrabObject();
    }

    private void GrabObject()
    {
        if(box != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickedup = !pickedup;
                if (pickedup)
                {
                    box.parent = player;
                    boxRB = box.GetComponent<Rigidbody2D>();
                    boxRB.isKinematic = true;
                    boxRB.simulated = false;
                }
                else if(pickedup == false)
                {
                    box.parent = null;
                    boxRB.isKinematic = false;
                    boxRB.simulated = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            box = other.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(pickedup == false)
        {
            if (other.gameObject.CompareTag("Box"))
            {
                box = null;
            }
        }
    }
}
