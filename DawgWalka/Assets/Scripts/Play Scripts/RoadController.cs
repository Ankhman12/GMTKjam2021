using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{

    private Vector2 currentPos;
    public Vector2 roadDir;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = this.transform.position;
        currentPos += roadDir * moveSpeed * Time.deltaTime;
        this.transform.position = currentPos;
    }
}
