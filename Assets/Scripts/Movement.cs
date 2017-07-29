﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    [Range(0, 15)]
    public float speed;
    [Range(0, 45)]
    public float jumpStrength;
    [Range(0, 8)]
    public float slideSpeed;

    float accelration;
    float gravity = 9.81f;
    bool canJump;
    bool canWallJump;
    RaycastHit2D hitInfo;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            hitInfo = Physics2D.Raycast(transform.position + new Vector3(0, -.5f), Vector3.down);
            if (hitInfo.distance < 0.15f)
            if (canJump)
            {
                hitInfo = Physics2D.Raycast(transform.position + new Vector3(0, -.5f), Vector3.down);
                if (hitInfo.distance < 0.15f)
                {
                    Debug.Log("canjump");
                    rb.velocity = new Vector2(0, jumpStrength);
                    canJump = false;
                }
            }
            else if (canWallJump)
            {
                hitInfo = Physics2D.Raycast(transform.position + new Vector3(-0.5f, 0), Vector3.left);
                if (hitInfo.distance < 0.6f)
                {
                    rb.velocity = new Vector2(1.0f, jumpStrength);
                    canWallJump = false;
                }
                else
                {
                    hitInfo = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0), Vector3.left);
                    if (hitInfo.distance < 0.6f)
                    {
                        rb.velocity = new Vector2(-1.0f, jumpStrength);
                        canWallJump = false;
                    }
                }
            }

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
                Debug.Log(rb.velocity.y);
                rb.velocity = new Vector2(moveX, 0.981f - slideSpeed);
            }
        }
        rb.velocity = new Vector2(moveX * Time.fixedDeltaTime * 100 * speed, rb.velocity.y);

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
        }
    }
}