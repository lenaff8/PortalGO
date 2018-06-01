using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReciever : MonoBehaviour {
    public Mechanism[] links;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Activate()
    {
        //anim.SetTrigger(message);
        for (int i = 0; i < links.Length; i++)
        {
            links[i].SetState("Activate");
        }
    }

    public void Deactivate()
    {
        //anim.SetTrigger(message);
        for (int i = 0; i < links.Length; i++)
        {
            links[i].SetState("Deactivate");
        }
    }


}
