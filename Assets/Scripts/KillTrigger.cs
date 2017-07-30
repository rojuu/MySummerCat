using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class KillTrigger : MonoBehaviour
{

    public Vector3 spawnPoint;
    //public ParticleSystem killParticle;
    public ParticleSystem killParticle;
    Vector3 playerPosition;

    private void Start()
    {
        spawnPoint = spawnPoint + transform.position;
        //GameObject.Find("Player").transform.position = playerPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Kill(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Kill(collider.gameObject);
        }
    }

    private void Kill(GameObject go)
    {
        go.GetComponent<Movement>().KillPlayer(spawnPoint, 0.3f);
        //go.transform.position = playerPosition;
        playerPosition = GameObject.FindWithTag("Player").transform.position;
        Instantiate(killParticle, playerPosition, transform.rotation);
        //go.transform.position = spawnPoint;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
            return;

        Handles.DrawSolidRectangleWithOutline(
            new Rect(
                transform.position.x + (spawnPoint.x - 0.5f),
                transform.position.y + (spawnPoint.y - 0.5f),
                1, 1
            ),
            Color.green, Color.yellow
        );

        Handles.DrawSolidRectangleWithOutline(
            new Rect(
                transform.position.x - transform.localScale.x / 2,
                transform.position.y - transform.localScale.y / 2,
                transform.localScale.x,
                transform.localScale.y
            ),
            new Color(0, 0, 0, 0), Color.green
        );


        Handles.color = Color.green;
        Handles.DrawLine(transform.position, transform.position + spawnPoint);
    }
#endif
}
