using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerS : MonoBehaviour
{
    GameObject weapon;
    SpriteRenderer wCol;

    private Rigidbody2D rb2d;
    //private ConfigurableJoint cj;

    bool dir = true;


    //float speed = 20.0f;
    float height = 10.0f;
    bool jump = true;
    bool rightPressed = false;
    bool leftPressed = false;
    bool spacePressed = false;
    bool switchPress = false;
    public int wType = 0;

    Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        weapon = GameObject.FindGameObjectWithTag("Wpn");
        wCol = weapon.GetComponent<SpriteRenderer>();
        wCol.color = Color.green;
    }

    // Update is called once per frame
    Vector2 movement;

    void FixedUpdate()
    {
        rb2d.freezeRotation = true;

        rightPressed = Input.GetKey(KeyCode.RightArrow);
        leftPressed = Input.GetKey(KeyCode.LeftArrow);
        spacePressed = Input.GetKey(KeyCode.Space);
        if(!switchPress){
            switchPress = Input.GetKeyDown(KeyCode.R);
        }
        
        Transform wpnT = weapon.GetComponent<Transform>();
        Transform plyR = this.GetComponent<Transform>();

        if(switchPress){
            Debug.Log("Hit Col" +  wType);
            wType = (wType+1) % 3;
            if(wType == 0) wCol.color = Color.green;
            else if(wType == 1) wCol.color = Color.blue;
            else if(wType == 2) wCol.color = Color.red;
            else wCol.color = Color.black;
            switchPress = false;
        }

        if(rightPressed){
            movement.x = 0.2f;
            if(!dir){
                dir = true;
                wpnT.transform.RotateAround(this.transform.position, new Vector3(0,0,1), 180f);
            }
        }
        else if(leftPressed){
            movement.x = -0.2f;
            if(dir){
                dir = false;
                wpnT.transform.RotateAround(this.transform.position, new Vector3(0,0,1), -180f);
            }
        }
        else{
            movement.x = 0f;
        }

        
        /* 
        if (rightPressed) {
            rb2d.AddForce(transform.right * speed);
            //rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x , 0, 5), 0);
        }
        else if (leftPressed) {
            rb2d.AddForce(-transform.right * speed);
            //rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x , -5, 0), 0);
        }
        */
        this.transform.Translate(movement);

        if (spacePressed) {
            if (jump) {
                rb2d.AddForce(transform.up * height, ForceMode2D.Impulse);
                    jump = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        jump = true;
    }
}
