using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    public float moveTime = 0.1f;
    public GameObject target;

    // Use this for initialization
    protected override void Start () {
        GameManager.instance.AddEnemyToList(this);
        base.Start();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveEnemy()
    {
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
            if (Move(xDir, 0))
                return;

        Move(0, yDir);

       
    }
}
