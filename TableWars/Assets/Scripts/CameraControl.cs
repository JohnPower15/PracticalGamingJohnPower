using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float pan_speed=20f;
    Vector3 boarders;
    //Quaternion cameraRotation;
    public int scroll_speed = 20;
    public int rotationSpeed = 20;


    void Update()
    {   
        
        boarders = new Vector3(10,10,10);
        boarders = new Vector3(10,10,10);
        Vector3 position = transform.position;
        

       if (Input.GetKey("w"))
        {
            
            Vector3 camer_move = new Vector3(transform.forward.x, 0, transform.forward.z);
            transform.position += camer_move.normalized*pan_speed*Time.deltaTime;

        }
        if (Input.GetKey("s"))
        {

            Vector3 camer_move = new Vector3(transform.forward.x, 0, transform.forward.z);
            transform.position -= camer_move.normalized * pan_speed * Time.deltaTime;

        }
        if (Input.GetKey("a"))
        {

            Vector3 camer_move = new Vector3(transform.forward.z, 0, transform.forward.x);
            transform.position -= camer_move.normalized * pan_speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {

            Vector3 camer_move = new Vector3(transform.forward.z, 0, transform.forward.x);
            transform.position += camer_move.normalized * pan_speed * Time.deltaTime;
        }
    }
}
