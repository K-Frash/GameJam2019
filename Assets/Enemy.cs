using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector2 movement;
    SpriteRenderer col;

    
    bool dir = true; //True == Right, False = Left
    float timer = 1f;
    public float dX;
    public float dY;
    

    //Public Variables for testing
    
    public int type = 0; //0 = slime, 1 = fish, 2 = bird

    // Start is called before the first frame update
    public virtual void Awake()
    {
        Debug.Log("Basic Enemy Created");
        col = gameObject.GetComponent<SpriteRenderer>();
        col.color = Color.black;
    }

    public virtual StatStruct SaveState(){
        StatStruct s = new StatStruct();
        s.dir = dir;
        return s;
    }

    public virtual void LoadState(StatStruct s){
        dir = s.dir;
    }

    public virtual void MoveEnemy(){
        // Testing Purposes
        movement.x = dX;
        movement.y = dY;

        if(dir == true){
            movement.x = dX;
        }
        else{
            movement.x = -dX;
        }

        this.transform.Translate(movement);
    }

    public virtual void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Wall"){
            dir = !dir;
           StartCoroutine(WaitTime());
        }

        IEnumerator WaitTime() {
            float tmp = dX;
            dX = 0f;
            yield return new WaitForSeconds(timer);
            dX = tmp;
        }
    }
}


public class Slime : Enemy
{
    Vector2 movement;
    SpriteRenderer col;
    Rigidbody2D rb;
    bool dir = true; //True == Right, False = Left
    float timer = 1f;

    GameObject plyr;
    Transform plyrTrns;
    Rigidbody2D plyrRb;

    public float knockup = 7f;
    public float knockback = 10f;

     public override void Awake(){
        Debug.Log("Slime Created");
        col = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        plyr =  GameObject.FindGameObjectWithTag("Plyr");
        plyrTrns = plyr.GetComponent<Transform>();
        plyrRb = plyr.GetComponent<Rigidbody2D>();
        col.color = Color.green;
        rb.gravityScale = 2;
    }

    public override void MoveEnemy(){
        // Testing Purposes
        movement.x = dX;
        movement.y = dY;
        //Debug.Log("Plyr "+plyrTrns.transform.position);

        if(dir == true){
            movement.x = dX;
        }
        else{
            movement.x = -dX;
        }

        this.transform.Translate(movement);
    }

    public override StatStruct SaveState(){
        StatStruct s = new StatStruct();
        s.dir = dir;
        return s;
    }

    public override void LoadState(StatStruct s){
        dir = s.dir;
    }

    public override void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.tag + " Slime");
        if(other.gameObject.tag == "Wall"){
           dir = !dir;
           StartCoroutine(WaitTime());
        }
        else if(other.gameObject.tag == "Plyr"){
            Debug.Log("Hit Player");
            plyrRb.AddForce(Vector2.up*knockup, ForceMode2D.Impulse);
            if(dir){
                plyrRb.AddForce(Vector2.right*knockback, ForceMode2D.Impulse);
            }
            else{
                plyrRb.AddForce(Vector2.left*knockback, ForceMode2D.Impulse);
            }
        }

        IEnumerator WaitTime() {
            float tmp = dX;
            dX = 0f;
            yield return new WaitForSeconds(timer);
            dX = tmp;
        }
    }
}

public class Fish : Enemy
{
    Vector2 movement;
    SpriteRenderer col;
    Rigidbody2D rb;

    bool dir = true; //True == Right, False = Left
    bool strJ = true;
    bool plyHit = false;
    float timer = 0.5f;
    float jumpSpeed;
    float jumpStrength = 20f;
    float orig_dX;

     public override void Awake(){
        Debug.Log("Fish Created");
        orig_dX = 0.2f;
        jumpSpeed =  0.1f;
        col = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        col.color = Color.cyan;
        rb.gravityScale = 4;
    }

    void Jump(string mode)
    {
        if(mode == "strong"){
            rb.AddForce(Vector2.up*jumpStrength, ForceMode2D.Impulse);
        }
        else if(mode == "weak"){
            rb.AddForce(Vector2.up*jumpStrength*0.5f, ForceMode2D.Impulse);
        }
    }

    public override void MoveEnemy(){
        // Testing Purposes
        movement.x = dX;
        movement.y = dY;

        //Debug.Log(rb.velocity.y + " " + dX + " " + orig_dX);

        if(strJ && plyHit){ //In Air by palyer hit
            dX = 0.05f;
        }
        else if(strJ){ //In air by obstacle/got hit
            dX = jumpSpeed;
        }
        else{ //Ground
            dX = orig_dX;
        }

        if(dir == true){
            movement.x = dX;
        }
        else{
            movement.x = -dX;
        }

        this.transform.Translate(movement);
    }

    public override StatStruct SaveState(){
        StatStruct s = new StatStruct();
        s.dir = dir;
        return s;
    }

    public override void LoadState(StatStruct s){
        dir = s.dir;
    }

    public override void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.tag + " Fish");
        if(other.gameObject.tag == "Wall"){
           dir = !dir;
           StartCoroutine(WaitTime());
           strJ = true;
           plyHit = false;
           Jump("weak");
        }

        else if(other.gameObject.tag == "Plyr"){
            //dir = !dir;
            strJ = true;
            plyHit = true;
            Jump("strong");
        }
        else{
            strJ = false;
            plyHit = false;
        }

        IEnumerator WaitTime() {
            float tmp = dX;
            dX = 0f;
            yield return new WaitForSeconds(timer);
            dX = tmp;
        }
    }
}

public class Bird : Enemy
{
    Vector2 movement;
    SpriteRenderer col;

    Rigidbody2D rb;
    bool dir = true; //True == Right, False = Left
    float timer = 0.5f;

    GameObject plyr;
    Transform plyrTrns;
    Rigidbody2D plyrRb;

     public override void Awake(){
        Debug.Log("Bird Created");
        col = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        col.color = Color.red;
        rb.gravityScale = 0;
        plyr =  GameObject.FindGameObjectWithTag("Plyr");
        plyrTrns = plyr.GetComponent<Transform>();
        plyrRb = plyr.GetComponent<Rigidbody2D>();

    }

    bool swooping = false;
    void Swoop(){
        swooping = true;
        rb.AddForce(Vector2.down*15f, ForceMode2D.Impulse);
    }

    public override void MoveEnemy(){
        // Testing Purposes
        movement.x = dX;
        movement.y = dY;
        Debug.Log(Mathf.Abs(plyrTrns.position.x - this.transform.position.x));

        if(Mathf.Abs(plyrTrns.position.x - this.transform.position.x) < 5){
            if(!swooping){
                Swoop();
            }
        }
        else{
            swooping = false;
        }
        if(transform.position.y <= GameObject.FindWithTag("Ground").transform.position.y + 5){
            movement.y = 0.1f;
        }

        if(dir == true){
            movement.x = dX;
        }
        else{
            movement.x = -dX;
        }

        this.transform.Translate(movement);
    }

    public override StatStruct SaveState(){
        StatStruct s = new StatStruct();
        s.dir = dir;
        return s;
    }

    public override void LoadState(StatStruct s){
        dir = s.dir;

        //transform.position = GameObject.FindWithTag("Ground").transform.position + new Vector3(0,5);
        //transform.position = transform.position + new Vector3(0,5);
    }

    public override void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.tag + " Bird");
        if(other.gameObject.tag == "Wall"){
            dir = !dir;
           StartCoroutine(WaitTime());
        }
        else if(other.gameObject.tag == "Plyr"){
            if(dir){
                plyrRb.AddForce(Vector2.right*5f, ForceMode2D.Impulse);
            }
            else{
                plyrRb.AddForce(Vector2.left*5f, ForceMode2D.Impulse);
            }
        }

        IEnumerator WaitTime() {
            float tmp = dX;
            dX = 0f;
            yield return new WaitForSeconds(timer);
            dX = tmp;
        }
    }
}