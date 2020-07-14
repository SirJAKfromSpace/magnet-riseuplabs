using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour
{
    Transform playerTf;
    Vector3 offsetVector;

    // Start is called before the first frame update
    void Start(){
        playerTf = GameObject.Find("Player").transform;
        offsetVector = transform.position - playerTf.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerTf.position + offsetVector;
    }
}
