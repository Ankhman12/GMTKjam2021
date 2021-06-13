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
    [SerializeField] private bool onRail;
    //private bool isOllie;
    public bool canTrick;
    private float trickTimer;
    public GameObject currentObstacle;
    public static event ActionRef<GameObject> OnMissTrick;
    public static event Action OnTrick;

    //Animations and VFX
    public ParticleSystem GrindFX;
    public Animator playerAnim;
    public Animator dogAnim;

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
        trickTimer -= Time.deltaTime;
        
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("AAAA");
            Debug.Log(GameManager.Instance.gameState);
            if(GameManager.Instance.gameState == GameState.Game) {
                Debug.Log("Pausing");
                GameManager.Instance.SetGameState(GameState.Paused);
            } else if (GameManager.Instance.gameState == GameState.Paused) {
                Debug.Log("Unpausing");
                GameManager.Instance.SetGameState(GameState.Game);
            }    
        } 
        
    }

    private void FixedUpdate()
    { 
        if (Input.GetKey(KeyCode.Space) && canTrick && !onRail)
        {
            DoTrick();
        }
        if (onRail)
        {
            if (Input.GetKeyUp(KeyCode.Space) || currentObstacle == null)
            {
                LeaveRail();
            }
            else
            {
                movement = Vector2.zero;
            }

            Vector2 railPos = new Vector2(currentObstacle.transform.position.x, this.transform.position.y);
            if (this.transform.position.x != railPos.x)
            {
                rb.MovePosition(railPos * moveSpeed * Time.deltaTime);
            }
        }
        if (canTrick && trickTimer < 0)
        {
            //Trick failure
            Debug.Log("miss");
            OnMissTrick?.Invoke(ref currentObstacle);
            dogAnim.SetTrigger("Slowed");
            canTrick = false;
            if (!currentObstacle.CompareTag("Rail"))
            {
                currentObstacle.GetComponent<Obstacle>().Impact();
            }
            currentObstacle = null;
        }

        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

        //updating speed for SFX
        NewPos = transform.position;  // each frame track the new position
        currentSpeed = ((NewPos - PrevPos) / Time.fixedDeltaTime).magnitude;  // velocity = dist/time
        PrevPos = NewPos;  // update position for next frame calculation
    }

    

    private void DoTrick() {
        if(currentObstacle == null) {
            //Jump?
        }
        else 
        {
            if (currentObstacle.CompareTag("Rail"))
            {
                //Play Trick animation(s)
                playerAnim.SetBool("isGrinding", true);
                GrindFX.Play();
                rb.freezeRotation = true;
                onRail = true;
                Debug.Log("Yaet");
            }
            else if (currentObstacle.CompareTag("Cone"))
            {
                //Play Trick animation(s)
                playerAnim.SetTrigger("Kickflip");
                Debug.Log("Trick'd");
            }
            else if (currentObstacle.CompareTag("Barrier")) {
                playerAnim.SetTrigger("Duck");
            }
            OnTrick?.Invoke();
            canTrick = false;
        }

    }

    private void LeaveRail() {
        GrindFX.Stop();
        playerAnim.SetBool("isGrinding", false);
        onRail = false;
    }

    public void ResetTrickTimer() {
        trickTimer = 0.3f;
    }


}
