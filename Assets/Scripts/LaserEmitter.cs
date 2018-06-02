using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{


    LineRenderer laserLineRenderer, secondLineRenderer;
    public LayerMask layerMaskObjects;
    public Vector3 LaserDirection;
    float laserWidth = 0.1f;
    float laserMaxLength = 50f;
    bool hittinReciever = false;
    GameObject reciever = null;
    Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };


    // Use this for initialization
    void Start()
    {

        laserLineRenderer = gameObject.GetComponent<LineRenderer>();
        laserLineRenderer.SetPositions(initLaserPositions);
        laserLineRenderer.startWidth = laserWidth;
        laserLineRenderer.endWidth = laserWidth;
        laserLineRenderer.enabled = true;

        secondLineRenderer = gameObject.transform.GetChild(0).GetComponent<LineRenderer>();
        secondLineRenderer.SetPositions(initLaserPositions);
        secondLineRenderer.startWidth = laserWidth;
        secondLineRenderer.endWidth = laserWidth;
        secondLineRenderer.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        laserLineRenderer.SetPositions(initLaserPositions);
        //secondLineRenderer.SetPositions(initLaserPositions);

        ShootLaserFromTargetPosition(transform.position, LaserDirection);
    }


    void ShootLaserFromTargetPosition(Vector3 position, Vector3 direction)
    {
        Ray ray = new Ray(position+direction*2, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = position + (laserMaxLength * direction);

        if (Physics.Raycast(ray, out raycastHit, laserMaxLength, layerMaskObjects))
        {

            if (raycastHit.collider.gameObject.tag == "OrangePortal")
            {
                Debug.Log("Hitting orange");

                Vector3 nullVector = new Vector3(-1, -1, -1);
                Debug.Log("Blue at " + GameManager.instance.GetBluePortalEnd());
                Vector3 beginPos = GameManager.instance.GetBluePortalEnd();
                Vector3 towardPos = GameManager.instance.GetBluePortalBegin();
                Vector3 dir = towardPos - beginPos;
                beginPos = beginPos + (dir / 2) - new Vector3(0, 1, 0); ; //dir is of the form (0,5,0) (i think)
                ShootSecondaryLaserFromTargetPosition(beginPos, dir);
                Vector3 endPos = GameManager.instance.GetOrangePortalEnd();
                Vector3 fromPos = GameManager.instance.GetOrangePortalBegin();
                endPosition = raycastHit.point + (endPos - fromPos);
            }
            else if (raycastHit.collider.gameObject.tag == "BluePortal")
            {
                Debug.Log("Hitting blue");
                Vector3 nullVector = new Vector3(-1, -1, -1);
                Debug.Log("Orange at " + GameManager.instance.GetOrangePortalEnd());
                Vector3 beginPos = GameManager.instance.GetOrangePortalEnd();
                Vector3 towardPos = GameManager.instance.GetOrangePortalBegin();
                Vector3 dir = towardPos - beginPos;
                beginPos = beginPos + (dir / 2) - new Vector3(0, 1, 0);
                ShootSecondaryLaserFromTargetPosition(beginPos, dir);
                Vector3 endPos = GameManager.instance.GetBluePortalEnd();
                Vector3 fromPos = GameManager.instance.GetBluePortalBegin();
                endPosition = raycastHit.point + (endPos - fromPos);
            }
            else
            {
                checkForObjectCollision(raycastHit);
                endPosition = raycastHit.point;
            }

        }

        laserLineRenderer.SetPosition(0, transform.position);
        laserLineRenderer.SetPosition(1, endPosition);
    }


    void ShootSecondaryLaserFromTargetPosition(Vector3 position, Vector3 direction)
    {
        Debug.Log("Shoot second ray from " + position);
        secondLineRenderer.SetPosition(0, position);
        Ray ray = new Ray(position + direction * 2, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = position + (laserMaxLength * direction);
        //bool orangePortal = false;
        //bool bluePortal = false;
        if (Physics.Raycast(ray, out raycastHit, laserMaxLength, layerMaskObjects))
        {
            endPosition = raycastHit.point;
            checkForObjectCollision(raycastHit);
        }
        secondLineRenderer.SetPosition(1, endPosition);
    }

    void checkForObjectCollision(RaycastHit raycastHit)
    {
        if (raycastHit.collider.gameObject.tag == "LaserReciever")
        {
            if (!hittinReciever)
            {
                Debug.Log("u win");
                hittinReciever = true;
                reciever = raycastHit.collider.gameObject;
                reciever.SendMessage("Activate");
            }
        }
        else
        {
            if (hittinReciever)
            {
                Debug.Log("no win");
                reciever.SendMessage("Deactivate");
                hittinReciever = false;
            }
            if (raycastHit.collider.gameObject.tag == "Player")
            {
                //player dies
                Debug.Log("u ded");
            }
        }
    }
}
