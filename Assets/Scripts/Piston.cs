using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : Mechanism {
    public GameObject tile;
    Collider tileCollider;
    public LayerMask layerMaskCollider, layerMaskTarget;


    // Use this for initialization
    override protected void Start () {
        base.Start();
        tileCollider = tile.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update () {

    }

    protected bool Move()
    {
        Vector3 start = transform.position;
        Vector3 dir = new Vector3(0, 5, 0);
        Vector3 end = start + dir;

        RaycastHit hit;
        if (Physics.Raycast(start, transform.TransformDirection(dir), out hit, 5.0f, layerMaskTarget))
        {
            if (hit.collider != null)
            {
                GameObject target = hit.transform.gameObject;
                Vector3 position = hit.transform.position;
                target.SendMessage("SmoothMovement", position + new Vector3(0,5,0));
            }
        }

        return false;
    }

    override protected void OnSetState(string state)
    {
        m_Animator.SetTrigger(state);
        tileCollider.enabled = !tileCollider.enabled;
        Move();
    }
}
