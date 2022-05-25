using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelRotate : MonoBehaviour
{
    [SerializeField] float turnSpeed;
   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(turnSpeed * Time.deltaTime, 0, 0);
    }
}
