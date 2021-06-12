using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 movement;
    public float moveSpeed;
    public float drag;
    Rigidbody2D rb;

    //these variables calculate the real speed of the player, for SFX
    [System.NonSerialized]
    public static float currentSpeed; 
    Vector3 PrevPos; 
    Vector3 NewPos; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PrevPos = transform.position;
        NewPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = drag;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

        //updating speed for SFX
        NewPos = transform.position;  // each frame track the new position
        currentSpeed = ((NewPos - PrevPos) / Time.fixedDeltaTime).magnitude;  // velocity = dist/time
        PrevPos = NewPos;  // update position for next frame calculation
    }
}
