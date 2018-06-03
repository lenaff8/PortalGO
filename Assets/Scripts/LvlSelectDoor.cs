using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LvlSelectDoor : Door {


    // Use this for initialization
    override protected void Start()
    {
        base.Start();
    }
        // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        SetState("Activate");
    }

    void OnTriggerExit(Collider other)
    {
        SetState("Deactivate");
        
    }

    override protected void OnSetState(string state) {
        if(state == "Activate")
        {
            compColliderBehind.enabled = true;
        }
        else
        {
            compColliderBehind.enabled = false;
        }
    }

    
}
