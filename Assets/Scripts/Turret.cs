using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public LayerMask layerMask;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0,2,0), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            //player ded
        }
 
    }
}
