using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTest : MonoBehaviour
{
    public Transform exit;
    public Transform entrance;

    bool disablePortal = false;
    public Transform player;

    // Start is called before the first frame update
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && disablePortal == false)
        {
            player = other.gameObject.transform;
            player.position = exit.position;
        }       
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(1f);
        disablePortal = false;
    }
}
