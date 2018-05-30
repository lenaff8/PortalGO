using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

    private bool pt;

	// Use this for initialization
	protected override void Start () {
        
    }
	
	// Update is called once per frame
	private void Update () {

        if (!GameManager.instance.playersTurn) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (horizontal != 0)
            vertical = 0;

        if (horizontal != 0 || vertical != 0)
        {
            if (Move(vertical * 5, horizontal * -5))
            {
                GameManager.instance.playersTurn = false;
            }
        }
        
    }

    

}
