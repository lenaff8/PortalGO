using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    public float moveTime = 0.1f;
    public GameObject target;

    protected bool attackMode;

    // Use this for initialization
    protected override void Start () {
        GameManager.instance.AddEnemyToList(this);
        base.Start();
        attackMode = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveEnemy()
    {
        if (!attackMode)
            return;

        int xDir = 0;
        int yDir = 0;

        if (target.transform.position.x > transform.position.x)
            xDir = 5;
        else if(target.transform.position.x < transform.position.x)
            xDir = -5;

        if (target.transform.position.z > transform.position.z)
            yDir = 5;
        else if (target.transform.position.z < transform.position.z)
            yDir = -5;


        if (xDir != 0)
            if (Move(transform.position.x + xDir, transform.position.z))
                return;

        Move(transform.position.x, transform.position.z + yDir);

    }
}
