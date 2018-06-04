using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : Enemy {

    private int tilesToActivate = 2;

	// Use this for initialization
	protected override void Start () {
        base.Start();

    }

    // Update is called once per frame
    void Update () {

        if (!attackMode) {
            if ((int)(transform.position.y - target.transform.position.y) / 5 == 0)
            {
                int tilesX = (int)(transform.position.x - target.transform.position.x) / 5;
                int tilesZ = (int)(transform.position.z - target.transform.position.z) / 5;
                if ((Mathf.Abs(tilesX) + Mathf.Abs(tilesZ)) < tilesToActivate)
                {
                    Animator animator = GetComponent<Animator>();
                    animator.SetTrigger("showMimic");
                    attackMode = true;
                }
            }
        }
    }

}
