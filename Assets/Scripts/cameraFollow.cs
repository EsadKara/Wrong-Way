using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
   [SerializeField] GameObject player;
    Vector3 playerPos;


    private void Update()
    {
        playerPos = new Vector3(player.transform.position.x - 0.6f, player.transform.position.y + 0.9f, player.transform.position.z);
    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerPos, 15f * Time.deltaTime);
    }
}
