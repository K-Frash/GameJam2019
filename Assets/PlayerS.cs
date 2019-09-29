using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerS : MonoBehaviour
{
    GameObject Weapon;
    private Rigidbody2D rb2d;
    //private ConfigurableJoint cj;


    float speed = 20.0f;
    float height = 10.0f;
    bool jump = true;
    bool rightPressed = false;
    bool leftPressed = false;
    bool spacePressed = false;

    Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        Weapon = GameObject.Find("Weapon");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.freezeRotation = true;

        rightPressed = Input.GetKey(KeyCode.RightArrow);
        leftPressed = Input.GetKey(KeyCode.LeftArrow);
        spacePressed = Input.GetKey(KeyCode.Space);
        

        if (rightPressed) {
            rb2d.AddForce(transform.right * speed);
            //rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x , 0, 5), 0);
        }
        else if (leftPressed) {
            rb2d.AddForce(-transform.right * speed);
            //rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x , -5, 0), 0);
        }

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
