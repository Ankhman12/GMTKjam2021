using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 movement;
    public float moveSpeed;
    public float drag;
    //public Dog ziggi;

    Rigidbody2D rb;

    //these variables calculate the real speed of the player, for SFX
    [System.NonSerialized]
    public static float currentSpeed; 
    Vector3 PrevPos; 
    Vector3 NewPos;

    //Trick fields
    private bool onRail;
    //private bool isOllie;
    public bool canTrick;
    private float trickTimer;
    public GameObject currentObstacle;
    [SerializeField]
    private float obstacleForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        PrevPos = transform.position;
        NewPos = transform.position;

        canTrick = false;
        currentObstacle = null;
    }

    // Update is called once per frame
    void Update()
    {
        //Player input
        movement.x = Input.GetAxis("Horizontal");
        movement.y = drag;
        if (Input.GetKeyDown(KeyCode.Space) && canTrick) {
            DoTrick();
        }
        trickTimer -= Time.deltaTime;
        
    }

    private void FixedUpdate()
    {
        if (onRail)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                LeaveRail();
            }
            else
            {
                movement = Vector2.zero;
                //Debug.Log("sssKRRRRRRRRt");
            }
        }
        if (canTrick && trickTimer < 0)
        {
            if (currentObstacle.CompareTag("Cone")) {
                currentObstacle.GetComponent<Rigidbody2D>().AddForce((transform.forward + transform.right) * obstacleForce);
            }
        }

        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

        //updating speed for SFX
        NewPos = transform.position;  // each frame track the new position
        currentSpeed = ((NewPos - PrevPos) / Time.fixedDeltaTime).magnitude;  // velocity = dist/time
        PrevPos = NewPos;  // update position for next frame calculation
    }

    

    private void DoTrick() {
        if (currentObstacle.CompareTag("Rail"))
        {
            //Play Trick animation(s)
            //....

            //Snap player to rail
            Vector2 railPos = new Vector2(currentObstacle.transform.position.x, this.transform.position.y);
            this.transform.position = railPos;
            onRail = true;
        }
        else if (currentObstacle.CompareTag("Cone"))
        {
            //Play Trick animation(s)
            //....
        }

    }

    private void LeaveRail() {
        rb.AddForce(-transform.right * 10f, ForceMode2D.Impulse); 
        onRail = false;     
    }

    public void ResetTrickTimer() {
        trickTimer = 1f;
    }

}
