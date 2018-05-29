using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress_Script : MonoBehaviour {
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Active");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("Deactive");
        }
    }
}
