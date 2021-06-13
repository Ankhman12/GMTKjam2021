using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{

    private Vector2 currentPos;
    public Vector2 roadDir;
    public static float moveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = this.transform.position;
        if(GameManager.Instance.gameState == GameState.Game) currentPos += roadDir * moveSpeed * Time.deltaTime;
        this.transform.position = currentPos;
    }
}
