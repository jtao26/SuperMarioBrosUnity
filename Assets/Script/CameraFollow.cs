using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour
{
    private Transform PlayerTransform;
    private float CameraPosX, CameraPosY;
    private float PlayerPosX, PlayerPosY;
    private bool CameraLock = false;
    public float LeftLim = 0, RightLim = 30;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CameraLock == false){
                transform.position = new Vector3(PlayerTransform.position.x, transform.position.y, transform.position.z); 
        }
        else if(CameraLock == true){
            if(transform.position.x == LeftLim){
                if(PlayerTransform.position.x > transform.position.x){
                    transform.position = new Vector3(PlayerTransform.position.x, transform.position.y, transform.position.z);
                    CameraLock = false;
                }
            }
            else if(transform.position.x == RightLim){
                if(PlayerTransform.position.x < transform.position.x){
                    transform.position = new Vector3(PlayerTransform.position.x, transform.position.y, transform.position.z);
                    CameraLock = false;
                }
            }
        }

        // Update by frame will lead to x < 0, showing a negative number like -0.013
        // Therefore when detecting a negative number, reset the camera position and 
        // the Lock Mark
        if(transform.position.x <= LeftLim){
            transform.position = new Vector3(LeftLim, transform.position.y, transform.position.z);
            CameraLock = true;
        }
        if(transform.position.x >= RightLim){
            transform.position = new Vector3(RightLim, transform.position.y, transform.position.z);
            CameraLock = true;
        }

        
        
        



        


        
    }
}
