using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StatStruct{
    public bool dir;
    public float dX;
    public float dY;
}

public class EnemyMgr : MonoBehaviour
{
    Enemy minion;
    GameObject playerobj;
    PlayerS plyrInit;
    int prevW;

    StatStruct stats;
    public int mType = 0; //0=slime,1=fish,2=bird
    int curType = -1;

    //Init Stats
    public float dX = 0.1f;
    public float dY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        stats = new StatStruct();
        stats.dir = true;
        stats.dX = dX;
        stats.dY = dY;
        createEnemy();
        playerobj = GameObject.FindGameObjectWithTag("Plyr");
        plyrInit = playerobj.GetComponent<PlayerS>();
        prevW = plyrInit.wType;
    }

    // Update is called once per frame
    void Update()
    {
        if(prevW != plyrInit.wType){
            mType = (mType+1)%3;
            prevW = plyrInit.wType;
        }
        createEnemy();
        minion.MoveEnemy();
    }

    void createEnemy(){
        if(curType == mType) return;

        Debug.Log("Changing Enemy!");

        if(minion != null) stats = minion.SaveState();
        Destroy(minion);
        if(mType == 0){
            minion = gameObject.AddComponent<Slime>();
            minion.dX = dX;
            minion.dY = dY;
            //minion.GetComponent<Rigidbody2D>().gravityScale = 1;

        }
        else if(mType == 1){
            minion = gameObject.AddComponent<Fish>();
            minion.dX = dX*2;
            minion.dY = dY*2;
            //minion.GetComponent<Rigidbody2D>().gravityScale = 4;

        }
        else if(mType == 2){
            minion = gameObject.AddComponent<Bird>();
            minion.dX = dX*1.5f;
            minion.dY = dY*1.5f;
        }
        else{
            minion = gameObject.AddComponent<Enemy>();
            minion.dX = 0;
            minion.dY = 0;
            minion.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        curType = mType;
        minion.LoadState(stats);
    }
}
