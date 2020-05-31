using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldCharacter : MonoBehaviour
{
    private MovingPlatform mp = null;

    private void Start()
    {
        mp = GetComponent<MovingPlatform>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Interactable")
        {
            Debug.Log("STICK IN ZONE");
            other.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Interactable")
        {
            Debug.Log("STICK IN ZONE");
            other.transform.parent = null;
        }
    }
}
