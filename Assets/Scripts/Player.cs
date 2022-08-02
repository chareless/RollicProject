using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector3 velocity;
    Vector3 firstPos;
    Vector3 endPos;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.gameStarted)
        {
            //Klavye Kontrolü
            /*
            velocity = new Vector3(0, 0, Input.GetAxis("Horizontal"));
            transform.position += velocity * 2 * Time.fixedDeltaTime;*/
            
            //Mouse Kontrolü
            if (Input.GetMouseButtonDown(0))
            {
                firstPos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                endPos = Input.mousePosition;

                float fark = endPos.x - firstPos.x;

                transform.position += new Vector3(0, 0, fark * Time.fixedDeltaTime * speed);
            }

            if (Input.GetMouseButtonUp(0))
            {
                firstPos = Vector3.zero;
                endPos = Vector3.zero;
            }

            //Telefon Kontrolü

            if (Input.touchCount > 0)
            {
                Touch parmak = Input.GetTouch(0);
                transform.position += new Vector3(0,0,parmak.deltaPosition.x)*Time.fixedDeltaTime*speed;
            }
        }
    }
}
