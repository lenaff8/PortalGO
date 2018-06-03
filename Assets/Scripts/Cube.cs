using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{

    protected bool pickable;
    protected bool picked;
    protected Outline outline;
    protected Vector3 playerDirection;
    public LayerMask playerMask, objectLayer;
    // Use this for initialization
    virtual protected void Start()
    {
        outline = gameObject.GetComponent<Outline>();
        pickable = false;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        RaycastHit hit;
        if (!GameManager.instance.playersTurn)
        {
            outline.enabled = false;
            return;
        }
        //see if it is pickable
        else if (!picked)
        {
            //Search for player nearby
            pickable = false;

            Vector3[] dirs = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };
            foreach (Vector3 dir in dirs)
            {
                if (Physics.Raycast(gameObject.transform.position, dir, out hit, 5f, playerMask))
                {
                    pickable = true;
                    playerDirection = dir;
                    break;
                }
            }
            //Outline if hovered
        }
        //see if it's outline should be enabled
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, objectLayer))
        {
            if (pickable && gameObject == hit.transform.gameObject)
                outline.enabled = true;
            else
                outline.enabled = false;
        }
        if (outline.enabled && Input.GetMouseButtonDown(0))
        {
            if (!picked)
            {
                picked = true;
                PickUp();
            }
            else
            {
                PutDown();

            }
        }
    }

    public bool IsPickable()
    {
        return pickable;
    }

    virtual protected void PickUp()
    {

    }

    virtual protected void PutDown()
    {

    }
}
