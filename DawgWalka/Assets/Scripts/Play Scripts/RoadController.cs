using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{

    private Vector2 currentPos;
    public Vector2 roadDir;
    public const float maxMoveSpeed = 8;
    public static float moveSpeed = maxMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement.OnMissTrick += SlowDown;
    }

    void OnDestroy() {
        PlayerMovement.OnMissTrick -= SlowDown;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = this.transform.position;
        if(GameManager.Instance.gameState == GameState.Game) currentPos += roadDir * moveSpeed * Time.deltaTime;
        this.transform.position = currentPos;
        Debug.Log(moveSpeed);
    }

    public void SlowDown(ref GameObject obj) {
        StartCoroutine("SlowDownRoad");
    }

    IEnumerator SlowDownRoad() {
        moveSpeed = (maxMoveSpeed)/2f;
        yield return null;
        while(moveSpeed < maxMoveSpeed) {
            moveSpeed += 0.4f * Time.deltaTime;
            yield return null;
        }
        moveSpeed = maxMoveSpeed;
    }
}
