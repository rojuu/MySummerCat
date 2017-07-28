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

        RaycastHit hitInfo;
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(Mathf.Clamp(moveX * speed,-speed,speed), rb.velocity.y);
        /* ACCELRATION STUFF IF WANTED
        if (moveX == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * accelration * Time.deltaTime, rb.velocity.y);
        }
        */
        /*
        if (Physics.Raycast(transform.position, Vector2.down, out hitInfo))
        {
            
            if (hitInfo.distance > 1f)
            {
                velocity.y += -gravity * Time.deltaTime;
                rb.MovePosition(new Vector2(transform.position.x, transform.position.y + velocity.y));
                Debug.Log(hitInfo.distance);
            }   
            */
        }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.contacts[0].normal);
        if (col.contacts[0].normal == Vector2.up)
        {
            Debug.Log("Can Jump");
            canJump = true;
        }
            
    }
}