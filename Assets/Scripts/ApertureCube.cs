using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApertureCube : Cube {

    bool state = true;
	// Use this for initialization
	override protected void Start () {
        base.Start();
    }
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();

    }

    // this object was clicked and the player is near - do something
    override protected void PickUp()
    {
        Debug.Log("Clicked Aperture Cube");
    }
}
