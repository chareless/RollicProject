using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject obj;
    public GameObject[] objects;
    public Vector3[] pos;
    public bool isRandom;
    void Start()
    {
        if(!isRandom)
        {
            for(int i=0;i<pos.Length;i++)
            {
                Instantiate(obj,transform.position + pos[i], transform.rotation);
            }
        }
        else
        {
            int random = Random.Range(0, objects.Length);
            for (int i = 0; i < pos.Length; i++)
            {
                Instantiate(objects[random], transform.position + pos[i], transform.rotation);
            }
        }   
    }
}
