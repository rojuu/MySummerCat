using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody2D rb;
    [Range(0, 15)]
    public float speed;
    [Range(0, 45)]
    public float jumpStrength;

    float accelration;
    float gravity = 9.81f;
    bool canJump;
    bool canDoubleJump;
    Vector2 velocity;
    Vector2 lastPos;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && canJump == true)
        {
            rb.velocity = new Vector2(0, jumpStrength);
            canJump = false;
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = Vector2.zero;
        RaycastHit2D hitInfo;


        float moveX = Input.GetAxisRaw("Horizontal");
        if (moveX != 0)
        {
            if (moveX < 0)
            {
                hitInfo = Physics2D.Raycast(transform.position, Vector3.left);
                Debug.DrawRay(transform.position + new Vector3(-0.5f, 0), Vector3.left, Color.red);
            }
            else if (moveX > 0)
            {
                hitInfo = Physics2D.Raycast(transform.position, Vector3.right);
                Debug.DrawRay(transform.position + new Vector3(0.5f, 0), Vector3.right, Color.red);
            }

            else { hitInfo = new RaycastHit2D(); hitInfo.distance = float.PositiveInfinity; }

            if (hitInfo.distance < 0.52f && hitInfo.distance != 0)
            {
                moveX = 0;
            }
        }
        rb.velocity = new Vector2(Mathf.Clamp(moveX * speed, -speed, speed), rb.velocity.y);

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
        }
    }
}