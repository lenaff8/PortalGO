using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter : MonoBehaviour{


    LineRenderer laserLineRenderer;
    public LayerMask layerMaskObjects;
    public Vector3 LaserDirection;
    float laserWidth = 0.1f;
    float laserMaxLength = 50f;
    bool hittinReciever = false;
    GameObject reciever = null;

    // Use this for initialization
    void Start()
    {
        laserLineRenderer = gameObject.GetComponent<LineRenderer>();
        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);
        laserLineRenderer.startWidth = laserWidth;
        laserLineRenderer.endWidth = laserWidth;
        laserLineRenderer.enabled = true;
    }
    // Update is called once per frame
    void Update () {
        ShootLaserFromTargetPosition(transform.position, LaserDirection);
    }


    void ShootLaserFromTargetPosition(Vector3 position, Vector3 direction)
    {
        Ray ray = new Ray(position, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = position + (laserMaxLength * direction);
        //bool orangePortal = false;
        //bool bluePortal = false;
        if (Physics.Raycast(ray, out raycastHit, laserMaxLength, layerMaskObjects))
        {
            endPosition = raycastHit.point;
            if (raycastHit.collider.gameObject.tag == "LaserReciever")
            {
                //player dies
                if (!hittinReciever)
                {
                    Debug.Log("u win");
                    hittinReciever = true;
                    reciever = raycastHit.collider.gameObject;
                    reciever.SendMessage("Activate");
                }
            }
            else{
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

            /*
            if (raycastHit.collider.gameObject.tag == "OrangePortal")
            {
                orangePortal = true;
            }
            else if (raycastHit.collider.gameObject.tag == "BluePortal")
            {
                bluePortal = true;
            }


            if (bluePortal)
            {
                Vector3 nullVector = new Vector3(-1, -1, -1);
                if (GameManager.instance.GetOrangePortalEnd() != nullVector)
                {
                    Vector3 beginPos = GameManager.instance.GetOrangePortalEnd();
                    beginPos += new Vector3(0, transform.position.y - beginPos.y, 0);
                    Vector3 towardPos = GameManager.instance.GetOrangePortalBegin();
                    towardPos += new Vector3(0, transform.position.y - towardPos.y, 0);
                    ShootLaserFromTargetPosition(beginPos, towardPos-beginPos);
                }
            }
            else if (orangePortal)
            {
                Vector3 nullVector = new Vector3(-1, -1, -1);
                if (GameManager.instance.GetOrangePortalEnd() != nullVector)
                {
                    Vector3 beginPos = GameManager.instance.GetBluePortalEnd();
                    beginPos += new Vector3(0, transform.position.y - beginPos.y, 0);
                    Vector3 towardPos = GameManager.instance.GetBluePortalBegin();
                    towardPos += new Vector3(0, transform.position.y - towardPos.y, 0);
                    ShootLaserFromTargetPosition(beginPos, towardPos - beginPos);
                }
            }*/
        }

        laserLineRenderer.SetPosition(0, transform.position);
        laserLineRenderer.SetPosition(1, endPosition);
    }

}
