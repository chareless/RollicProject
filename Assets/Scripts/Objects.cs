using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    Rigidbody rb;
    public float force;
    private bool forced;
    private bool collided;
    private int place;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y > 0.6f)
        {
            transform.position = new Vector3 (transform.position.x, 0.6f, transform.position.z);
        }
        if (LevelManager.stop == true && !forced)
        {
            if(collided)
            {
                forced = true;
                if (place == LevelManager.miniLevel)
                {
                    rb.AddForce(force, 0, 0, ForceMode.Impulse);
                }
            }
        }

        if (LevelManager.stop == false)
        {
            forced = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (LevelManager.miniLevel == 1)
        {
            if (collision.gameObject.tag == "MainPlane1")
            {
                Destroy(gameObject);
            }
        }

        if (LevelManager.miniLevel == 2)
        {
            if (collision.gameObject.tag == "MainPlane2")
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MainPlane1" || collision.gameObject.tag == "MainPlane2")
        {
            transform.parent = collision.transform;
        }
        if (collision.gameObject.tag == "Plane")
        {
            Destroy(gameObject);
            LevelManager.destroyedObject++;
        }
        if (collision.gameObject.tag == "MainPlane1" )
        {
            place = 0;
        }
        if (collision.gameObject.tag == "MainPlane2")
        {
            place = 1;
        }
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Obj")
        {
            if(!LevelManager.stop)
            {
                collided = true;
            }
        }
    }
}
