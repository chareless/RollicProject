using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int speed;
    public float sayac;
    public float sayacLimit;
    public static int miniLevel = 0;
    public bool onGame;
    public static bool stop;
    public int[] planeCount;
    public static int destroyedObject;
    public bool check;
    public GameObject[] planes;
    public GameObject mainPlane;
    public GameObject[] blok;
    public Text[] planeText;

    void PlaneMove()
    {
        if (planes[miniLevel-1].transform.position.y < -1)
        {
            planes[miniLevel-1].transform.position += new Vector3(0, 1.5f, 0) * Time.fixedDeltaTime;
        }
        else
        {
            planes[miniLevel-1].transform.position = new Vector3(planes[miniLevel-1].transform.position.x, mainPlane.transform.position.y - 0.1f, planes[miniLevel-1].transform.position.z);
        }
    }

    void PlaneCountControl()
    {
        if (check == false)
        {
            if (destroyedObject >= planeCount[miniLevel])
            {
                check = true;
                blok[miniLevel].SetActive(false);
                miniLevel++;
                destroyedObject = 0;
            }
            else
            {
                destroyedObject = 0;
                GameManager.winOrLose = -1;
            }
        }
    }
    
    void TextController()
    {
        for(int i= 0; i < planeCount.Length; i++)
        {
            if(i>=miniLevel)
            {
                planeText[i].text = destroyedObject + "/" + planeCount[i];
            }
        }
    }

    void FixedUpdate()
    {
        if (GameManager.gameStarted && GameManager.winOrLose != -1)
        {
            TextController();
            sayac += Time.fixedDeltaTime;
            if (sayac < sayacLimit)
            {
                destroyedObject = 0;
                check = false;
                stop = false;
                if(GameManager.level<=10)
                {
                    transform.position += new Vector3(speed, 0, 0) * Time.fixedDeltaTime;
                }
                else
                {
                    transform.position += new Vector3(speed+((GameManager.level-10)/1000), 0, 0) * Time.fixedDeltaTime;
                }
            }
            else
            {
                stop = true;
            }

            if (!onGame)
            {
                if (sayac > sayacLimit + 3)
                {
                    PlaneCountControl();

                    if (sayac > sayacLimit + 5)
                    {
                        PlaneMove();
                        
                        if (sayac > sayacLimit + 7)
                        {
                            sayac = -2.75f;
                        }
                    }
                }

                if (miniLevel >= 2)
                {
                    destroyedObject = 0;
                    onGame = true;
                    sayac = 0;
                    miniLevel = 0;
                    GameManager.winOrLose = 1;
                }
            }
        }
    }
}
