using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour {

    //public ParticleSystem orangePortal, bluePortal;
    public GameObject bluePortal, orangePortal, bluePortalBegin, orangePortalBegin;
    public LayerMask layerMask, layerMaskNotPortal, layerMaskPortal;

    private GameObject portal, beginPortal;
    private Outline outline;
    private bool orangePortalB = false;
    private bool bluePortalB = false;
    private bool mouseDownR = false;
    private bool mouseDownL = false;

    // Use this for initialization
    void Start () {
        outline = gameObject.GetComponent<Outline>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.instance.playersTurn || GameManager.instance.setup || !GameManager.instance.playing) 
        {
            outline.enabled = false;
            return;
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 2.0f, layerMaskNotPortal))
        {
            if (hit.collider != null)
                return;
        }

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 2.0f, layerMaskPortal))
        {
            if (GameManager.instance.GetBluePortalPos() == transform.position)
            {
                GameManager.instance.SetBluePortalPos(new Vector3(-1, -1, -1));
                GameManager.instance.SetBluePortal(new Vector3(-1, -1, -1));
            }
            if (GameManager.instance.GetOrangePortalPos() == transform.position)
            {
                GameManager.instance.SetOrangePortalPos(new Vector3(-1, -1, -1));
                GameManager.instance.SetOrangePortal(new Vector3(-1, -1, -1));
            }
        }

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask))
        {
            if (gameObject.tag == "Tile" && gameObject == hit.transform.gameObject)
                outline.enabled = true;
            else 
                outline.enabled = false;

            if (Input.GetMouseButtonDown(0))        // Blue Portal
            {
                if (!mouseDownL)
                {
                    mouseDownL = true;
                    if (gameObject == hit.transform.gameObject)
                    {
                        if (!bluePortalB && !orangePortalB)
                        {
                            bluePortalB = true;
                            beginPortal = Instantiate(bluePortalBegin, gameObject.transform.position, gameObject.transform.localRotation);
                            portal = Instantiate(bluePortal, gameObject.transform.position, gameObject.transform.localRotation);
                            GameManager.instance.SetBluePortal(portal.transform.position);
                            GameManager.instance.SetBluePortalPos(transform.position);

                            if (gameObject.transform.rotation.eulerAngles.z == 90)
                            {
                                beginPortal.transform.localScale += new Vector3(0.0f, 0.0f, 0.5f);
                                portal.transform.localScale += new Vector3(0.0f, 0.0f, 0.5f);
                            }
                            else
                            {
                                beginPortal.transform.localScale += new Vector3(0.0f, 0.5f, 0.5f);
                                portal.transform.localScale += new Vector3(0.0f, 0.5f, 0.5f);
                            }

                        }                           // [OPCIONAL] Sonido de no poder poner portal
                    }
                    else if (bluePortalB)
                    {
                        Destroy(beginPortal);
                        Destroy(portal);
                        bluePortalB = false;
                    }
                }

            }
            else
                if (mouseDownL)
                mouseDownL = false;

            if (Input.GetMouseButtonDown(1))        // Orange Portal
            {
                if (!mouseDownR)
                {
                    mouseDownR = true;
                    if (gameObject == hit.transform.gameObject)
                    {
                        if (!bluePortalB && !orangePortalB)
                        {
                            orangePortalB = true;
                            beginPortal = Instantiate(orangePortalBegin, gameObject.transform.position, gameObject.transform.localRotation);
                            portal = Instantiate(orangePortal, gameObject.transform.position, gameObject.transform.localRotation);
                            GameManager.instance.SetOrangePortal(portal.transform.position);
                            GameManager.instance.SetOrangePortalPos(transform.position);

                            if (gameObject.transform.rotation.eulerAngles.z == 90)
                            {
                                beginPortal.transform.localScale += new Vector3(0.0f, 0.0f, 0.5f);
                                portal.transform.localScale += new Vector3(0.0f, 0.0f, 0.5f);
                            }
                            else
                            {
                                beginPortal.transform.localScale += new Vector3(0.0f, 0.5f, 0.5f);
                                portal.transform.localScale += new Vector3(0.0f, 0.5f, 0.5f);
                            }
                        }                           // [OPCIONAL] Sonido de no poder poner portal
                    }
                    else if (orangePortalB)
                    {
                        Destroy(beginPortal);
                        Destroy(portal);
                        orangePortalB = false;
                    }
                }

            }
            else
                if (mouseDownR)
                mouseDownR = false;
        }  
    }

}
