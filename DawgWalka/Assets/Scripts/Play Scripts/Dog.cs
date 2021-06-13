using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    //public Vector2 movement;
    public float moveSpeed;
    public LayerMask obstacleLayer;
    public Vector2 scanBoxDimensions;

    private Rigidbody2D rb;
    [SerializeField]
    private Vector2 targetDir;
    private Transform target;
    private bool readyForNext;
    //private int count;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetDir = transform.up;
        target = this.transform;
        readyForNext = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(target.position.y);
        if (target.position.y < this.transform.position.y) {
            target = this.transform;
            readyForNext = true;
            //Debug.Log("***");
        }
    }

    private void FixedUpdate()
    {
        if (readyForNext)
        {
            Vector2 castPos = new Vector2(this.transform.position.x, this.transform.position.y + 3f); //Set position ahead of dog to cast box
            RaycastHit2D hit = Physics2D.BoxCast(castPos, scanBoxDimensions, 0f, this.transform.up, 5f, obstacleLayer); //cast box to check for obstacles
            //Debug.Log(hit.collider);
            //if (hit.collider != null) { 

                target = hit.collider.transform; //target is set to transform of first collider hit by boxcast
            //}
            Vector2 targetPoint = hit.point; //target point is set to point of contact
            targetPoint.y = targetPoint.y + 5f; //target point is set to +2 y units ahead of point of contact
            targetDir = targetPoint - ((Vector2)this.transform.position); //targetDir set to point from current position to target point
            readyForNext = false;
        }
        targetDir = ((Vector2)target.position) - ((Vector2)this.transform.position);
        rb.MovePosition(rb.position + targetDir.normalized * moveSpeed * Time.fixedDeltaTime);
        
    }
}
