using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SceneManage : MonoBehaviour
{
    public GameObject Player;
    
    private static SceneManage instance;

    private GameObject[] scale1Doors;
    private GameObject[] scale2Doors;
    private GameObject[] scale3Doors;
    private GameObject[][] scaleDoors = new GameObject[3][];
    public float scale1Min = 0.03f;
    public float scale2Min = 0.08f;
    public float scale3Min = 0.14f;
    

    public static SceneManage Instance
    {
        get
        {
            if (instance == null)
            {
               // instance = FindObjectOfType<SceneManage>();
            }
            return instance;
        }
    }

    public float GetPlayerScale()
    {
       // print("Player Scale: " + Player.transform.localScale.x);
        return Player.transform.localScale.x;
    }

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        scale1Doors = GameObject.FindGameObjectsWithTag("Scale1");
        
        scale2Doors = GameObject.FindGameObjectsWithTag("Scale2");
        
        scale3Doors = GameObject.FindGameObjectsWithTag("Scale3");
        
        scaleDoors[0] = scale1Doors;
        
        scaleDoors[1] = scale2Doors;
        
        scaleDoors[2] = scale3Doors;

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void SetScaleXDoors(int scale, bool state)
    {
        if (scaleDoors[scale - 1].Length > 0 && scaleDoors[scale - 1][0].activeSelf != state)
        {
            foreach (var door in scaleDoors[scale - 1])
            {
                //print(door);
                door.SetActive(state);
            }
        }     
    }


    // Update is called once per frame
    void Update()
    {
        if(GetPlayerScale() < scale1Min)
        {
            DieLogic();
        }
        else if (GetPlayerScale() < scale2Min)
        {
            print("scale 1");
            SetScaleXDoors(1,false);
            SetScaleXDoors(2,true);
            SetScaleXDoors(3,true);
        }
        else if (GetPlayerScale() < scale3Min)
        {
            print("scale 2");
            SetScaleXDoors(1,true);
            SetScaleXDoors(2,false);
            SetScaleXDoors(3,true);
        }
        else
        {
            print("scale 3");
            SetScaleXDoors(1,true);
            SetScaleXDoors(2,true);
            SetScaleXDoors(3,false);
        }
    }

    void DieLogic()
    {
        print("Die");
    }
}
