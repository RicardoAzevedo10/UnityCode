using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{ 
//This script is intended for the player to grab the ball, and for cases of VR games
    public GameObject ball;
    public GameObject myHand;
    public float handPower;

    bool inHands = false;  
    Collider ballCol;
    Rigidbody ballRb;
    Camera cam;
    


    // Start is called before the first frame update
    void Start()
    {
       
        ballCol = ball.GetComponent<SphereCollider>();
        ballRb = ball.GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))// In this case this button is the right button in vr googlecardboard
        {
            if (!inHands) //If the ball is not in the hands of the player, the same grab the ball
            {
                //Grab the ball
                ballCol.isTrigger = true;
                ball.transform.SetParent(myHand.transform);
                ball.transform.localPosition = new Vector3(0f, -0.336f, 0.544f);
                ballRb.velocity = Vector3.zero;
                ballRb.useGravity = false;
                inHands = true;
            }
            else if(inHands)
            {
                //Realease the ball
                ballCol.isTrigger = false;
                ballRb.useGravity = true;
                this.GetComponent<PlayerGrab>().enabled = false;
                ball.transform.SetParent(null);
                ballRb.velocity = cam.transform.rotation * Vector3.forward * handPower;
                inHands = false;
               

            }
          

        }
    }
}
