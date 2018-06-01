using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frankenturret : Enemy {

    public int tilesToActivate = 5;

	// Use this for initialization
	protected override void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!attackMode)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward*5.0f), Color.red);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, tilesToActivate * 5.0f, layerMaskTarget))
            {
                if (hit.collider != null)
                {
                    Animator animator = GetComponent<Animator>();
                    animator.SetTrigger("Activate");
                    attackMode = true;
                }
            }
        }
    }

}
