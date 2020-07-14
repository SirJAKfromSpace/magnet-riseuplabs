using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagnetRing : MonoBehaviour
{
    Rigidbody rigid;
    Transform playertf;
    public float forceMultiplier = 4;
    // Start is called before the first frame update
    void Start(){
        rigid = transform.GetComponentInParent<Rigidbody>();
        playertf = rigid.transform;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Finish") {
            Debug.Log("FINISH");
            Animator a = GameObject.Find("ScreenFader").transform.GetComponent<Animator>();
            a.SetBool("fadenow", true);
            Invoke("ReloadScene", 0.8f);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "MagnetRed") {
            Debug.Log("RED");
            Vector3 point = other.ClosestPoint(playertf.position);
            if (playertf.tag == other.tag) {
                Debug.Log("PUSH");
                rigid.AddForce((playertf.position - point).normalized * forceMultiplier, ForceMode.Impulse);
            }
            else {
                Debug.Log("PULL");
                rigid.AddForce((point - playertf.position).normalized * forceMultiplier, ForceMode.Impulse);
            }
        }
        else if (other.tag == "MagnetBlue") {
            Debug.Log("BLUE");
            Vector3 point = other.ClosestPoint(playertf.position);
            if (playertf.tag == other.tag) {
                Debug.Log("PUSH");
                rigid.AddForce((playertf.position - point).normalized * forceMultiplier, ForceMode.Impulse);
            }
            else {
                Debug.Log("PULL");
                rigid.AddForce((point - playertf.position).normalized * forceMultiplier, ForceMode.Impulse);
            }
        }
    }

    void ReloadScene() {
        SceneManager.LoadScene("MainScene");
    }

}
