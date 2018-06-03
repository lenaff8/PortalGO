﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

    private bool pt;
    private bool droppingCube;
    //private Animator animator;
    // Use this for initialization
    protected override void Start () {
        //animator = GetComponent<Animator>();
        droppingCube = false;
    }
	
	// Update is called once per frame
	private void Update () {

        if (!GameManager.instance.playersTurn) return;

        if (!droppingCube)
        {
            int horizontal = 0;
            int vertical = 0;

            horizontal = (int)(Input.GetAxisRaw("Horizontal"));
            vertical = (int)(Input.GetAxisRaw("Vertical"));

            if (horizontal != 0)
                vertical = 0;

            if (horizontal != 0 || vertical != 0) { 
                if (Move(transform.position.x + vertical * 5, transform.position.z + horizontal * -5))
                {
                    GameManager.instance.playersTurn = false;
                }
            }
        }
        
    }

    public void SetDroppingCube(bool value)
    {
        droppingCube = value;
    }
 
}
