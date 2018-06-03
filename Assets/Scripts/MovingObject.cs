using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

    public LayerMask layerMaskPortal, layerMaskCollider, layerMaskTarget, layerMaskObject;
    public float speed; 

    private GameObject bait;
    protected Animator animator;
    // Use this for initialization
    protected virtual void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    protected bool Move(float xDir, float yDir)
    {
        animator = GetComponent<Animator>();

        Vector3 start = new Vector3(transform.position.x, ((int) transform.position.y / 5) + 1, transform.position.z);
        Vector3 end = new Vector3(xDir, transform.position.y, yDir);

        RaycastHit hit;
        bool orangePortal, bluePortal, target;
        orangePortal = false;
        bluePortal = false;
        target = false;

        var heading = new Vector3(xDir, start.y, yDir) - start;
        var distance = heading.magnitude;
        var direction = (heading / distance)*5;

        if (Physics.Raycast(start, direction, out hit, 5.0f, layerMaskObject))
        {
            if (hit.collider != null)
                return false;
        }

        if (Physics.Raycast(start, direction, out hit, 5.0f, layerMaskTarget))
        {
            if (hit.collider != null)
            {
                target = true;

                if (hit.collider.CompareTag("Player"))
                    hit.collider.gameObject.GetComponent<Player>().Die(false);
                else if (hit.collider.CompareTag("Turret"))
                    hit.collider.gameObject.GetComponent<Turret>().Die();
                else if (hit.collider.CompareTag("Frankenturret"))
                    hit.collider.gameObject.GetComponent<Frankenturret>().Die();
            }
        }
        if (Physics.Raycast(start, direction, out hit, 5.0f, layerMaskPortal))
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
                bait = Instantiate(gameObject, transform.position, transform.rotation);
                Invoke("DestroyBait", 2);
                StartCoroutine(bait.GetComponent<MovingObject>().SmoothMovement(end, target));
                // animacion hacia abajo

                Vector3 newPos = GameManager.instance.GetOrangePortalEnd();
                newPos += new Vector3(0, transform.position.y - newPos.y, 0);
                transform.position = newPos;
                newPos = GameManager.instance.GetOrangePortalBegin();
                newPos += new Vector3(0, transform.position.y - newPos.y, 0);
                StartCoroutine(SmoothMovement(newPos, target));
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
                StartCoroutine(bait.GetComponent<MovingObject>().SmoothMovement(end, target));
                // animacion hacia abajo

                Vector3 newPos = GameManager.instance.GetBluePortalEnd();
                newPos += new Vector3(0, transform.position.y - newPos.y, 0);
                transform.position = newPos;
                newPos = GameManager.instance.GetBluePortalBegin();
                newPos += new Vector3(0, transform.position.y - newPos.y, 0);
                StartCoroutine(SmoothMovement(newPos, target));
                return true;

            }
        }
        else if (Physics.Raycast(start, direction, out hit, 5.0f, layerMaskCollider))
        {
            if (hit.collider != null)
            {
                StartCoroutine(SmoothMovement(end, target));
                return true;
            }
        }

        return false;
    }

    protected IEnumerator SmoothMovement(Vector3 end, bool attack)
    {
        // Rotation
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.LookRotation(end - transform.position, Vector3.up);
        animator = gameObject.GetComponent<Animator>();
        if (animator != null)
        {
            if (startRot.eulerAngles.y == 270 && endRot.eulerAngles.y == 0)
                animator.SetBool("ToRight", true);
            else if (startRot.eulerAngles.y == 0 && endRot.eulerAngles.y == 270)
                animator.SetBool("ToLeft", true);
            else if (startRot.eulerAngles.y < endRot.eulerAngles.y)
                animator.SetBool("ToRight", true);
            else if (startRot.eulerAngles.y > endRot.eulerAngles.y)
                animator.SetBool("ToLeft", true);
        }

        for (float t = 0f; t < 0.5f; t += Time.deltaTime)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, t / 0.5f);
            yield return null;
        }
        transform.rotation = endRot;
        if (animator != null)
        {
            animator.SetBool("ToRight", false);
            animator.SetBool("ToLeft", false);
        }

        // Movement
        float step = speed * Time.deltaTime;
        float sqrRemainingDistance;
        if (attack)
        {

            Vector3 halfEnd = end;
            if (transform.position.x < end.x)
                halfEnd.x -= 1.5f;
            else if (transform.position.x > end.x)
                halfEnd.x += 1.5f;
            else if (transform.position.z < end.z)
                halfEnd.z -= 1.5f;
            else if (transform.position.z > end.z)
                halfEnd.z += 1.5f;

            sqrRemainingDistance = (transform.position - halfEnd).sqrMagnitude;
            animator.SetBool("Walk", true);
            while (sqrRemainingDistance > float.Epsilon)
            {
                //float step = speed * Time.deltaTime * (Mathf.Sin((sqrRemainingDistance/25.0f) * (Mathf.PI/180f) ));
                transform.position = Vector3.MoveTowards(transform.position, halfEnd, step);
                sqrRemainingDistance = (transform.position - halfEnd).sqrMagnitude;
                yield return null;
            }
            animator.SetBool("Walk", false);
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(2);
        }
       
        animator.SetBool("Walk", true);
            
        sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            //float step = speed * Time.deltaTime * (Mathf.Sin((sqrRemainingDistance/25.0f) * (Mathf.PI/180f) ));
            transform.position = Vector3.MoveTowards(transform.position, end, step);

            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }
        animator.SetBool("Walk", false);
        

    }
    

    private void DestroyBait()
    {
        Destroy(bait);
    }

}
