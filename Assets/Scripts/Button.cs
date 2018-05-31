using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    Animator anim;
    public Mechanism[] links;
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
        for (int i = 0; i < links.Length; i++)
        {
            links[i].SetState("Activate");
        }
    }
    void OnTriggerExit(Collider other)
    { 
        anim.SetTrigger("Deactive");
        for (int i = 0; i < links.Length; i++)
        {
            links[i].SetState("Deactivate");
        }
    }

}
