using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector2 movement;
    bool dir = true; //True == Right, False = Left
    float timer = 0.5f;

    //Public Variables for testing
    public float dX = 0.1f;
    public float dY = 0f;
    // Start is called before the first frame update
    void Start()
    {
        movement = new Vector2(0.0f, 0.0f);
        movement.x = dX;
        movement.y = dY;
    }

    void MoveEnemy(){
        // Testing Purposes
        movement.x = dX;
        movement.y = dY;

        if(dir == true){
            movement.x += dX;
        }
        else{
            movement.x -= dX;

        }

        this.transform.Translate(movement);
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Hit Collision");
        dir = !dir;
        movement.x = 0f;
        if(timer>0)
        {
            timer -= Time.deltaTime;
            return;
        }

        timer = 0.5f;
    }
}
