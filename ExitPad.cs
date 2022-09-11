using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPad : MonoBehaviour
{
    public Transform teleportPlayer;
    public GameObject player;
    public bool tutorialCompleted;


    public void OnTriggerEnter(Collider other)
    {
        player.transform.position = teleportPlayer.transform.position; //teleports player to another location
        tutorialCompleted = true;

    }

    public bool tutorialComplete()
    {
        return tutorialCompleted = true;
    }

}
