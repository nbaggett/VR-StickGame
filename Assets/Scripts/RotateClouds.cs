using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateClouds : MonoBehaviour
{
    public float degrees = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, degrees * Time.deltaTime, 0);
    }
}
