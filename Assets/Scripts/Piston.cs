using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : Mechanism {
    public GameObject tile;
    Collider tileCollider;

    // Use this for initialization
    override protected void Start () {
        base.Start();
        tileCollider = tile.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update () {

    }

    override protected void OnSetState(string state)
    {
        m_Animator.SetTrigger(state);
        if (state == "Activated")
        {
            tileCollider.enabled = false;
        }
        else if (state == "Deactivated")
        {
            tileCollider.enabled = true;
        }
    }
}
