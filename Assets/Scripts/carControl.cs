using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carControl : MonoBehaviour
{
    [SerializeField] float carSpeed;
    void Update()
    {
        transform.Translate(0, 0, carSpeed * Time.deltaTime);
    }
}
