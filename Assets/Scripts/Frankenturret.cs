using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frankenturret : Enemy {

    public int tilesToActivate = 5;

	// Use this for initialization
	protected override void Start () {
        base.Start();

    }

    // Update is called once per frame
    void Update () {

        if (!attackMode)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward*5.0f*tilesToActivate), Color.red);
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y+1.0f, transform.position.z);
            if (Physics.Raycast(newPos, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMaskTarget))
            {
                if (hit.collider != null)
                {
                    //Animator animator = GetComponent<Animator>();
                    //animator.SetTrigger("Activate");
                    attackMode = true;
                }
            }
        }
    }

    public void Die()
    {
        // Animacion
        
    }

}
