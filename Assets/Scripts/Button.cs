using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    Animator anim;
    public  Animator otherObject;
    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        anim.SetTrigger("Active");
        otherObject.SetTrigger("Activate");
    }
    void OnTriggerExit(Collider other)
    {
        anim.SetTrigger("Deactive");
        otherObject.SetTrigger("Deactivate");
    }

}
