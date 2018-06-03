using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApertureCube : Cube {

    protected bool dropping;
    private int speed = 12;
    private GameObject player;
    private Transform parent;
    private Vector3 finalPosition;
    public LayerMask layerMaskCollider;
    // Use this for initialization
    override protected void Start () {
        base.Start();
        player = GameObject.Find("player");
        parent = gameObject.transform.parent;
        dropping = false;
    }
	
	// Update is called once per frame
	override protected void Update () {
        
        if (dropping)
        {
            float horizontal = 0;
            float vertical = 0;
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            if (horizontal != 0) vertical = 0;
            Vector3 dir = new Vector3(vertical, 0f, -horizontal);
            if (horizontal != 0 || vertical != 0)
            {
                dropping = false;
                player.SendMessage("SetDroppingCube", false);
                RaycastHit hit;
                if (Physics.Raycast(gameObject.transform.position, dir, out hit, 5.0f, layerMaskCollider))
                {
                    if (hit.collider != null)
                    {
                        //can drop cube there
                        picked = false;
                        finalPosition = gameObject.transform.position + 5 * dir + new Vector3(0f, -2.4f, 0f);
                        Debug.Log("Starting drop");
                        StartCoroutine(MoveToFinalPosition(parent));
                        GameManager.instance.playersTurn = false;
                    }
                }
            }
        }
        else base.Update();
    }

    // this object was clicked and it's pickable - do something
    override protected void PickUp()
    {
        Debug.Log("Picking Up Cube");
        //We do not want sudden movements
        finalPosition = gameObject.transform.position + playerDirection * 5f + new Vector3(0f, 2.4f, 0f);
        StartCoroutine(MoveToFinalPosition(player.transform));
        GameManager.instance.playersTurn = false;
    }

    protected override void PutDown()
    {
        Debug.Log("Selected Cube for drop");
        player.SendMessage("SetDroppingCube", true);
        dropping = true;
    }

    protected IEnumerator MoveToFinalPosition(Transform targetParent)
    {
        // Movement
        float step = speed * Time.deltaTime;
        float sqrRemainingDistance;
        sqrRemainingDistance = (transform.position - finalPosition).sqrMagnitude;
        while (sqrRemainingDistance > (0.000001f))
        {
            //float step = speed * Time.deltaTime * (Mathf.Sin((sqrRemainingDistance/25.0f) * (Mathf.PI/180f) ));
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, finalPosition, step);

            sqrRemainingDistance = (gameObject.transform.position - finalPosition).sqrMagnitude;
            Debug.Log("Remaining " + sqrRemainingDistance);
            yield return null;
        }
        Debug.Log("Trespassing cube");
        gameObject.transform.parent = targetParent;

    }
}
