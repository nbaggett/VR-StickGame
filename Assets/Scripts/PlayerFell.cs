﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFell : MonoBehaviour
{
    public Transform playerSpawn = null;
    public Transform stickSpawn = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            other.transform.position = playerSpawn.position;
        }
        else if (other.gameObject.tag == "Interactable")
        {
            other.transform.position = stickSpawn.position;
        }

    }
}
