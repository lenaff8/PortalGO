using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : Mechanism {
    public GameObject tileBase, tileTop;
    public LayerMask layerMaskObjects;
    Collider tileBaseCollider, tileTopCollider;
    GameObject target = null;
    public Transform targetParent = null;

    // Use this for initialization
    override protected void Start () {
        tileBaseCollider = tileBase.GetComponent<Collider>();
        tileTopCollider = tileTop.GetComponent<Collider>();
        base.Start();
    }

    // Update is called once per frame
    void Update () {

    }

    override protected void OnSetState(string state)
    {
        m_Animator.SetTrigger(state);
        tileBaseCollider.enabled = !tileBaseCollider.enabled;
        tileTopCollider.enabled = !tileTopCollider.enabled;

        //Bands position

        Vector3 start = gameObject.transform.GetChild(2).transform.position;
        Vector3 dir = new Vector3(0, 3, 0);
        Vector3 end = start + dir;
        Debug.DrawRay(start, dir, Color.cyan);
        RaycastHit hit;

        if (Physics.Raycast(start - new Vector3(0, 2f, 0), transform.TransformDirection(dir), out hit, 10f, layerMaskObjects))
        {
            if (hit.collider != null)
            {
                target = hit.transform.gameObject;
                Vector3 position = hit.transform.position;
                targetParent = target.transform.parent;
                target.transform.parent = gameObject.transform.GetChild(2).transform;

                Invoke("DestroyParent", 1f);
            }
        }
    }

    private void DestroyParent()
    {
        target.transform.parent = targetParent;
        targetParent = null;
    }
}
