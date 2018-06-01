using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : Enemy {

    public int tilesToActivate;

	// Use this for initialization
	protected override void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!attackMode) {
            int tilesX = (int)(transform.position.x - target.transform.position.x) / 5;
            int tilesZ = (int)(transform.position.z - target.transform.position.z) / 5;
            if ((tilesX + tilesZ) > tilesToActivate)
            {
                Animator animator = GetComponent<Animator>();
                animator.SetTrigger("Activate");
                attackMode = true;
            }
        }
    }

}
