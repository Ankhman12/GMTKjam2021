using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private PlayerMovement player;
    //public LayerMask playerLayer;
    

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player")) {
            player = other.GetComponent<PlayerMovement>();
            player.canTrick = true;
            player.ResetTrickTimer();
            player.currentObstacle = this.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerMovement>();
            player.canTrick = false;
            player.currentObstacle = null;
        }
    }

}
