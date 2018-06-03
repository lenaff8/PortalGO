using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    protected bool pickable;
    protected Outline outline;
    public LayerMask playerMask, objectLayer;
    // Use this for initialization
    virtual protected void Start () {
        outline = gameObject.GetComponent<Outline>();
        pickable = false;
	}
	
	// Update is called once per frame
	virtual protected void Update () {
        if (!GameManager.instance.playersTurn)
        {
            outline.enabled = false;
            return;
        }
        else
        {
            //Search for player nearby
            pickable = false;
            RaycastHit hit;
            
            Vector3[] dirs = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left}; 
            foreach (Vector3 dir in dirs)
            {
                if (Physics.Raycast(gameObject.transform.position, dir, out hit, 5f, playerMask))
                {
                    pickable = true;
                    break;
                }
            }
            //Outline if hovered

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, objectLayer))
            {
                if (pickable && gameObject == hit.transform.gameObject)
                    outline.enabled = true;
                else
                    outline.enabled = false;
            }
            if (pickable && outline.enabled && Input.GetMouseButtonDown(0)) PickUp();
        }
    }

    public bool IsPickable()
    {
        return pickable;
    }

    virtual protected void PickUp()
    {

    }
}
