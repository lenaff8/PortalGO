using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

    public LayerMask layerMaskPortal, layerMaskTile, layerMaskTarget;
    public float speed;

    private GameObject bait;

    // Use this for initialization
    protected virtual void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    protected bool Move(int xDir, int yDir)
    {
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(xDir, 0, yDir);

        RaycastHit hit;
        bool orangePortal, bluePortal, target;
        orangePortal = false;
        bluePortal = false;
        target = false;
        if (Physics.Raycast(start, transform.TransformDirection(new Vector3(xDir, 0, yDir)), out hit, 5.0f, layerMaskTarget))
        {
            if (hit.collider != null)
                target = true;
        }
        if (Physics.Raycast(start, transform.TransformDirection(new Vector3(xDir, 0, yDir)), out hit, 5.0f, layerMaskPortal))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "OrangePortal")
                {
                    orangePortal = true;
                }
                else if (hit.collider.gameObject.tag == "BluePortal")
                {
                    bluePortal = true;
                }
            }
        }

            
        if (bluePortal)
        {
            Vector3 nullVector = new Vector3(-1, -1, -1);
            if (GameManager.instance.GetOrangePortalEnd() != nullVector)
            {
                bait = Instantiate(gameObject, transform.position, Quaternion.identity);
                Invoke("DestroyBait", 1);
                StartCoroutine(bait.GetComponent<MovingObject>().SmoothMovement(end));
                // animacion hacia abajo

                Vector3 newPos = GameManager.instance.GetOrangePortalEnd();
                newPos += new Vector3(0, transform.position.y - newPos.y, 0);
                transform.position = newPos;
                newPos = GameManager.instance.GetOrangePortalBegin();
                newPos += new Vector3(0, transform.position.y - newPos.y, 0);
                StartCoroutine(SmoothMovement(newPos));
                return true;

            }

        }
        else if (orangePortal)
        {
            Vector3 nullVector = new Vector3(-1, -1, -1);
            if (GameManager.instance.GetBluePortalEnd() != nullVector)
            {
                bait = Instantiate(gameObject, transform.position, Quaternion.identity);
                Invoke("DestroyBait", 1);
                StartCoroutine(bait.GetComponent<MovingObject>().SmoothMovement(end));
                // animacion hacia abajo

                Vector3 newPos = GameManager.instance.GetBluePortalEnd();
                newPos += new Vector3(0, transform.position.y - newPos.y, 0);
                transform.position = newPos;
                newPos = GameManager.instance.GetBluePortalBegin();
                newPos += new Vector3(0, transform.position.y - newPos.y, 0);
                StartCoroutine(SmoothMovement(newPos));
                return true;

            }
        }
        else if (Physics.Raycast(start, transform.TransformDirection(new Vector3(xDir, 0, yDir)), out hit, 5.0f, layerMaskTile))
        {
            if (hit.collider != null)
            {
                StartCoroutine(SmoothMovement(end));
                if (target)
                {
                    // animacion atacando
                }
                else
                {
                    // animacion caminando
                }
                return true;

            }
        }

        return false;
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float step = speed * Time.deltaTime;
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            //float step = speed * Time.deltaTime * (Mathf.Sin((sqrRemainingDistance/25.0f) * (Mathf.PI/180f) ));
            transform.position = Vector3.MoveTowards(transform.position, end, step);

            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }
    }

    private void DestroyBait()
    {
        Destroy(bait);
    }
}
