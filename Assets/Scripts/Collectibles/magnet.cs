using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnet : MonoBehaviour
{
    gameManager gM;
    void Start()
    {
        gM = GameObject.Find("GameManager").GetComponent<gameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gM.CollectMagnet();
            Destroy(gameObject);
        }
    }
}
