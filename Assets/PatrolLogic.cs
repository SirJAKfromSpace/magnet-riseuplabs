using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLogic : MonoBehaviour
{
    Vector3 startposition, endposition;
    public float movespeed = 3;
    bool isGoingBack = false;

    // Start is called before the first frame update
    void Start() {
        startposition = transform.position;
        endposition = transform.GetChild(0).transform.position;
    }

    // Update is called once per frame
    void Update() {
        //transform.position = Vector3.Lerp(endposition, startposition, Mathf.PingPong(Time.time, movespeed));
        if(!isGoingBack) transform.position = Vector3.MoveTowards(transform.position, endposition, movespeed * Time.deltaTime);
        else transform.position = Vector3.MoveTowards(transform.position, startposition, movespeed * Time.deltaTime);


        if (transform.position == endposition) isGoingBack = true;
        else if (transform.position == startposition) isGoingBack = false;
    }
}
