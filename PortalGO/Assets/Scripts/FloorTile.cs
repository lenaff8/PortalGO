using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour {

    //public ParticleSystem orangePortal, bluePortal;
    public GameObject bluePortal, orangePortal, bluePortalBegin, orangePortalBegin;

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

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (gameObject == hit.transform.gameObject)
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
                    else
                        if (bluePortalB)
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
                    else
                        if (orangePortalB)
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
