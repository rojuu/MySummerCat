using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    [Range(0, 15)]
    public float speed;
    [Range(0, 5)]
    public float runningSpeed;
    [Range(0, 45)]
    public float jumpStrength;
    [Range(0, 8)]
    public float slideSpeed;
    [Range(0, 8)]
    public float movPushTime;
    [Range(0, 5)]
    public float movPushX;

    float accelration;
    float movementPush;
    Vector2 platformPush;
    float gravity = 9.81f;
    float maxSpeed = 0;
    bool canJump;
    bool canWallJump;
    bool canMove = true;
    bool wallClinging;
    bool isDead;

    RaycastHit2D hitInfo;
    TrailRenderer trail;
    Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        trail = GetComponentInChildren<TrailRenderer>();
    }

    void Update()
    {
        if (!canMove) return;

        //Jump Start
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            hitInfo = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0, Vector2.down);//rb.Cast(Vector2.down, jumpHit); //Physics2D.Raycast(transform.position + new Vector3(0, -.5f), Vector3.down);

            if (hitInfo.distance < 0.6f)
            {
                rb.velocity = new Vector2(0, jumpStrength);
                canJump = false;
                animator.SetTrigger("Jump");
            }
            else
            {
                hitInfo = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0, Vector2.left);
                if (hitInfo.distance < 0.6f && hitInfo.collider.tag != "Lava")
                {
                    rb.velocity = new Vector2(0, jumpStrength * 1.3f * 0.71f);
                    movementPush = movPushX;
                    canWallJump = false;
                    animator.SetTrigger("Jump");
                }
                else
                {
                    hitInfo = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0, Vector2.right);
                    if (hitInfo.distance < 0.6f && hitInfo.collider.tag != "Lava")
                    {
                        rb.velocity = new Vector2(0, jumpStrength * 1.3f * 0.71f);
                        movementPush = -movPushX;
                        canWallJump = false;
                        animator.SetTrigger("Jump");
                    }
                }
            }
        }

        //Jump End
        if (movementPush > 0.1f) movementPush -= movPushTime * 5 * Time.deltaTime;
        else if (movementPush < -0.1f) movementPush += movPushTime * 5 * Time.deltaTime;
        else if (movementPush < 0.1f || movementPush > -0.1f) movementPush = 0f;

        animator.SetFloat("VelocityY", rb.velocity.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetBool("WallCling", false);
        if (!canMove) return;
        float moveX = Input.GetAxisRaw("Horizontal");

        if (moveX != 0)
        {
            if (moveX < 0)
            {
                hitInfo = Physics2D.BoxCast(transform.position, Vector2.one * 0.52f, 0, Vector2.left);
                //Debug.DrawRay(transform.position + new Vector3(-0.5f, 0), Vector3.left, Color.red);
            }
            else if (moveX > 0)
            {
                hitInfo = Physics2D.BoxCast(transform.position, Vector2.one * 1f, 0, Vector2.right);
                //Debug.DrawRay(transform.position + new Vector3(0.5f, 0), Vector3.right, Color.red);
            }

            else { hitInfo = new RaycastHit2D(); hitInfo.distance = float.PositiveInfinity; }

            if (hitInfo.distance < 0.4f && hitInfo.collider.tag != "Lava" && (movementPush < movPushX * 0.7f && movementPush > -movPushX * 0.7f))
            {
                moveX = 0;
                wallClinging = true;
                //if (rb.velocity.x < 0) trail.transform.position = transform.position - new Vector3(0.3f, 0, 0);
                //else if (rb.velocity.x > 0) trail.transform.position = transform.position + new Vector3(0.3f, 0, 0);

                rb.velocity = new Vector2(moveX, 0.981f - slideSpeed);
                animator.SetBool("WallCling", true);
            }
        }
        //if (movementPush > 0.2f || movementPush < -0.2f) moveX = 0;

        if (moveX > 0) moveX = (moveX - Mathf.Clamp01(movementPush));
        if (moveX < 0) moveX = (moveX + Mathf.Clamp01(movementPush));

        moveX += movementPush;

        maxSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveX *= runningSpeed;
            maxSpeed = speed * runningSpeed;
        }
        float finalMovement = moveX * Time.fixedDeltaTime * 100 * speed;
        if (platformPush != Vector2.zero) finalMovement += platformPush.x * 1.09f;
        rb.velocity = new Vector2(Mathf.Clamp(finalMovement, -maxSpeed, maxSpeed), rb.velocity.y);
        animator.SetFloat("VelocityX", Mathf.Abs(moveX));
        platformPush = Vector2.zero;
        // Sprite rotation
        if (rb.velocity.x < 0) rb.transform.localScale = new Vector3(-1, 1, 1);
        else if (rb.velocity.x > 0) rb.transform.localScale = new Vector3(1, 1, 1);

    }

    /* ACCELRATION STUFF IF WANTED
    if (moveX == 0)
    {
        rb.velocity = new Vector2(rb.velocity.x * accelration * Time.deltaTime, rb.velocity.y);
    }
    */

    void OnCollisionEnter2D(Collision2D col)
    {
        if (Util.VecAlmostEqual(Vector2.up, col.contacts[0].normal, 0.001f))
        {
            canJump = true;
            canWallJump = true;
            animator.SetTrigger("HitGround");
        }

        if (Util.VecAlmostEqual(Vector2.left, col.contacts[0].normal, 0.001f))
        {
            movementPush = 0f;
        }
        /*
        if (col.gameObject.tag == "RotPlatform")
        {
            hitInfo = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0, Vector2.down);
            if (hitInfo.distance < .6f)
            {
                //rb.MovePosition(new Vector2(0, -hitInfo.distance));
                //rb.velocity = new Vector2(rb.velocity.x, 5f / (hitInfo.distance * 4));
                Debug.Log(hitInfo.distance);
            }
        }
        */
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "MovPlatform")
        {
            platformPush = col.gameObject.GetComponent<MovingPlatform>().velocity;
        }
        if (col.gameObject.tag == "RotPlatform")
        {
            platformPush = col.gameObject.GetComponent<RotatingPlatform>().velocity;
        }
    }
    /*
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "RotPlatform")
        {
            hitInfo = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0, Vector2.down);
            if (hitInfo.distance < .6f)
            {
                //rb.MovePosition(new Vector2(0, -hitInfo.distance));
                rb.velocity = new Vector2(rb.velocity.x, 60 );
                Debug.Log(hitInfo.distance);
            }
        }
    }
    */
    public void KillPlayer(Vector3 pos, float delay)
    {
        canMove = false;
        rb.velocity = Vector2.zero;
        GetComponentInChildren<TrailRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        StartCoroutine(IsDead(true, pos, delay));
    }

    IEnumerator IsDead(bool val, Vector3 pos, float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponentInChildren<TrailRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        transform.position = pos;
        canMove = val;
    }
}