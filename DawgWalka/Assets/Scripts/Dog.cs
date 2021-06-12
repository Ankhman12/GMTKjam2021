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
            //Debug.Log("%%%");
            Vector2 castPos = new Vector2(this.transform.position.x, this.transform.position.y + 3f);
            RaycastHit2D hit = Physics2D.BoxCast(castPos, scanBoxDimensions, 0f, this.transform.up, 5f, obstacleLayer);
            target = hit.collider.gameObject.transform;
            Vector2 targetPoint = hit.point;
            targetPoint.y = targetPoint.y + 2f;
            targetDir = targetPoint - ((Vector2)this.transform.position);
            readyForNext = false;
        }
        //Debug.Log(targetDir.normalized * moveSpeed);
        //Debug.Log("$$$$$$$$$$");
        targetDir = ((Vector2)target.position) - ((Vector2)this.transform.position);
        rb.MovePosition(rb.position + targetDir.normalized * moveSpeed * Time.fixedDeltaTime);
        
    }


    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    Debug.Log("&&&");
    //    if (other.gameObject.layer == obstacleLayer && readyForNext)
    //    {
    //        target = other.gameObject.transform;
    //        readyForNext = false;
    //        Debug.Log("&&&");
    //    }
    //}
}
