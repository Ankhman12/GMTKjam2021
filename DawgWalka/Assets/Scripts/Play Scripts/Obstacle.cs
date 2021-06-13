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
            if(!player.canTrick) { //enabling one trigger from multiple colliders
                player.canTrick = true;
                player.ResetTrickTimer();
                player.currentObstacle = this.gameObject;
                Debug.Log(other);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Rail"))
        {
            player = other.GetComponent<PlayerMovement>();
            player.canTrick = false;
            player.currentObstacle = null;
        }
    }

}
