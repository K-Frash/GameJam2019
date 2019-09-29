﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponS : MonoBehaviour
{
    GameObject Player;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = 20;
        Player = GameObject.Find("Player");
        offset = new Vector3(1.0f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position = Player.transform.position;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position = Player.transform.position + offset;
        }
        Debug.Log(Player.transform.position);
    }
}
