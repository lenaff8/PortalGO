using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour {
    protected Animator m_Animator;
    // Use this for initialization
    protected virtual void Start() {
        //Get them_Animator, which you attach to the GameObject you intend to animate.
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetState(string state)
    {
        m_Animator.SetTrigger(state);
        OnSetState(state);
    }

    protected virtual void OnSetState(string state){}
}
