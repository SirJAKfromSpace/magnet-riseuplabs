using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour
{
    public float moveSpeed = 5;
    public float jumpSpeed = 5;
    public float jumpSwipeLength = 2;
    public float swipeTime = 2;
    
    public float dashDiff = 3;
    public float boostTime = 0.5f;
    
    public float magnetFlipTime = 3;
    bool isRed = false;

    MeshRenderer playerMesh;
    MeshRenderer headMesh;
    public Material redmat;
    public Material bluemat;
    Material headmat;
    public Material dashmat;

    Rigidbody rigid;
    Joystick joystick;

    Vector3 moveVect;
    public bool isGrounded;
    int collMaskGrnd;
    float dist2Ground = 1;

    Vector3 startPos;

    void Start(){
        rigid = GetComponent<Rigidbody>();
        playerMesh = GetComponent<MeshRenderer>();
        headMesh = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        headmat = headMesh.material;
        InvokeRepeating("MagnetFlip", magnetFlipTime, magnetFlipTime);
        joystick = GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
        collMaskGrnd = LayerMask.GetMask("Ground");
        startPos = transform.position;
    }

    void Update(){
        moveVect = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;
        isGrounded = Physics.Raycast(transform.position - transform.up * 0.1f, -transform.up, dist2Ground, collMaskGrnd);
        //Debug.DrawRay(transform.position - transform.up * 0.1f, -transform.up * dist2Ground, Color.red);

        foreach (Touch t in Input.touches) {
            //Debug.Log("Taps:" + t.tapCount);
            if(t.tapCount == 2) {
                Debug.Log("< < < DASH");
                moveSpeed = moveSpeed + dashDiff;
                Invoke("DashReset", boostTime);
                headMesh.material = dashmat;
            }
            else if(t.phase==TouchPhase.Ended && t.deltaPosition.y>jumpSwipeLength && t.deltaTime<swipeTime) {
                if (isGrounded) {
                    Debug.Log("( ( JUMP ) )");
                    rigid.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
                }
            }
        }

        if (moveVect.magnitude >= 0.1f) {
            float targetangle = Mathf.Atan2(moveVect.x, moveVect.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetangle, 0);
        }
        
        rigid.MovePosition(transform.position + moveVect * moveSpeed * Time.deltaTime);

        if (transform.position.y < -10) transform.position = startPos;
    }

    void MagnetFlip() {
        if (isRed) {
            playerMesh.material = bluemat;
            isRed = false;
            gameObject.tag = "MagnetBlue";
        }
        else {
            playerMesh.material = redmat;
            isRed = true;
            gameObject.tag = "MagnetRed";
        }
    }

    void DashReset() {
        moveSpeed = moveSpeed - dashDiff;
        headMesh.material = headmat;
    }
}
