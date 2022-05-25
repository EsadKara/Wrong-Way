using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    playerControl playerCs;
    gameManager gM;
    [SerializeField] AudioClip rocketSound;
    void Start()
    {
        playerCs = GameObject.Find("Player").GetComponent<playerControl>();
        gM = GameObject.Find("GameManager").GetComponent<gameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerCs.hasRocket = true;
            gM.CollectRocket();
            Destroy(gameObject);
        }
    }
}
