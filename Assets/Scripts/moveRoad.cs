using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRoad : MonoBehaviour
{
    [SerializeField] GameObject coinsPref, street, obstaclePref, obstacle2Pref, obstacle3Pref, obstacles;
    GameObject obs1, obs2, obs3;

    playerControl playerCs;
    Animator playerAnim;

    private void Awake()
    {
        for(int i = 0; i < 1; i++)
        {
            obs1 = Instantiate(obstaclePref);
            obs1.transform.SetParent(obstacles.transform);
            obs1.SetActive(false);
            obs2 = Instantiate(obstacle2Pref);
            obs2.transform.SetParent(obstacles.transform);
            obs2.SetActive(false);
            obs3 = Instantiate(obstacle3Pref);
            obs3.transform.SetParent(obstacles.transform);
            obs3.SetActive(false);
        }
    }

    private void Start()
    {
        playerCs = GameObject.Find("Player").GetComponent<playerControl>();
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            street.transform.position = new Vector3(street.transform.position.x + 50, street.transform.position.y, street.transform.position.z);
            int randomPos = Random.Range(0, 2);
            int random = Random.Range(0, 2);
            int random2 = Random.Range(0, 2);
            int randomPos2 = Random.Range(0, 2);
            Vector3 coinPos, obstacle1Pos, obstacle2Pos;

            // Obstacle1 & coin pos
            if (randomPos == 0)
            {
                coinPos = new Vector3(street.transform.position.x + 6f, 0.4f, -0.625f);
                obstacle1Pos = new Vector3(street.transform.position.x + 6f, 0, 0.625f);
                obstacle2Pos = new Vector3(street.transform.position.x + 6f, 0.05f, -0.625f);
            }
            else
            {
                coinPos = new Vector3(street.transform.position.x + 6f, 0.4f, 0.625f);
                obstacle1Pos = new Vector3(street.transform.position.x + 6f, 0, -.625f);
                obstacle2Pos = new Vector3(street.transform.position.x + 6f, 0.05f, 0.625f);
            }

            //Coin Spawn
            Instantiate(coinsPref, coinPos, coinsPref.transform.rotation);

            //Obstacle1 & obstacle3 Spawn
            if (random == 0)
            {
                obs1.SetActive(true);
                obs3.SetActive(false);
                obs1.transform.position = obstacle1Pos;
            }
            else
            {
                obs3.SetActive(true);
                obs1.SetActive(false);
                obs3.transform.position = obstacle1Pos;
            }

            //Obstacle2 Spawn
            if (random2 == 0)
            {
                obs2.SetActive(true);
                obs2.transform.position = obstacle2Pos;
            }
            else
            {
                obs2.SetActive(false);
            }

           
            

            playerCs.moveSpeed += 0.125f;
            playerAnim.speed += 0.02f;
            if (playerAnim.speed >= 1.5f)
            {
                playerAnim.speed = 1.5f;
            }
        }
    }

}
