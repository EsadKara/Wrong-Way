using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    [SerializeField] float turnSpeed;
    bool move;
    GameObject player;
    gameManager gM;
    playerControl playerCs;
    void Start()
    {
        gM = GameObject.Find("GameManager").GetComponent<gameManager>();
        player = GameObject.FindGameObjectWithTag("Collect");
        playerCs = GameObject.Find("Player").GetComponent<playerControl>();
        move = false;
    }

   
    void Update()
    {
        transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
        if (gM.hasMagnet)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < 3f)
            {
                this.gameObject.transform.SetParent(null);
                move = true;
            }
        }
        if (move)
        {
            MoveToPlayer();
        }
    }

    void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (playerCs.moveSpeed * 2) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gM.CollectCoin();
            Destroy(gameObject);
        }
    }
}
