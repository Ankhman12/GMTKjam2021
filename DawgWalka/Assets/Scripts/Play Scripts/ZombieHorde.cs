using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHorde : MonoBehaviour
{
    //maybe should move to GameManager
    //Ranges from 0 to 1
    //Game over when distance = 0;
    [SerializeField]
    public static float distance = 1;
    public float decay = 0.1f;
    [SerializeField]
    private float distanceToPlayer = 3.9f; //physical distance between player and zombies at furthest point
    private float startPosY;

    float hitPenalty = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        distance = 1;
        startPosY = transform.position.y;
        PlayerMovement.OnMissTrick += HitObstacle;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameState == GameState.Game) distance += decay * Time.deltaTime;
        if(distance > 1) {
            distance = 1;
        } else if(distance <= 0 && GameManager.Instance.gameState == GameState.Game) {
            GameManager.Instance.SetGameState(GameState.GameOver);
        }

        if(Input.GetKeyDown(KeyCode.N)) {
            HitObstacle();
        }

        //update position
        transform.position = 
            new Vector3(transform.position.x, startPosY + ((1 - distance) * distanceToPlayer), transform.position.z);
    }

    void OnDestroy() {
        PlayerMovement.OnMissTrick -= HitObstacle;
    }

    //should be called when player hits obstacle
    public void HitObstacle() {
        StartCoroutine(DecreaseDistance(hitPenalty));
    }

    public void HitObstacle(ref GameObject collision) {
        HitObstacle();
    }

    //if you have a certain # of penalty
    public void HitObstacle(float penalty) {
        StartCoroutine(DecreaseDistance(penalty));
    }

    IEnumerator DecreaseDistance(float penalty) {
        float increasePerSecond = 0.8f;
        float totalAdded = 0;
        while(totalAdded < penalty) {
            distance -= increasePerSecond * Time.deltaTime;
            totalAdded += increasePerSecond * Time.deltaTime;
            yield return null;
        }
    }

}
