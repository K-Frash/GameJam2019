using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
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


