using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGroundCheck : MonoBehaviour
{
    playerControl playerCs;
    void Start()
    {
        playerCs = GameObject.Find("Player").GetComponent<playerControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            playerCs.isGround = true;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            playerCs.isGround = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            playerCs.isGround = false;
        }
    }

}
