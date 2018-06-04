using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

    private int nextlvl;
    private bool droppingCube;
    //private Animator animator;
    // Use this for initialization
    protected override void Start () {
        //animator = GetComponent<Animator>();
        droppingCube = false;
    }
	
	// Update is called once per frame
	private void Update () {

        if (!GameManager.instance.playersTurn || GameManager.instance.setup || !GameManager.instance.playing) return;

        if (!droppingCube)
        {
            int horizontal = 0;
            int vertical = 0;

            horizontal = (int)(Input.GetAxisRaw("Horizontal"));
            vertical = (int)(Input.GetAxisRaw("Vertical"));

            if (horizontal != 0)
                vertical = 0;

            if (horizontal != 0 || vertical != 0) { 
                if (Move(transform.position.x + vertical * 5, transform.position.z + horizontal * -5))
                {
                    GameManager.instance.playersTurn = false;
                }
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("To0"))
        {
            GameManager.instance.playersTurn = false;
            nextlvl = 0;
            Invoke("ChangeScene", 0.5f);
        }
        if (other.CompareTag("To1"))
        {
            GameManager.instance.playersTurn = false;
            nextlvl = 1;
            Invoke("ChangeScene", 0.5f);
        }
        if (other.CompareTag("To2"))
        {
            GameManager.instance.playersTurn = false;
            nextlvl = 2;
            Invoke("ChangeScene", 0.5f);
        }
        if (other.CompareTag("To3"))
        {
            GameManager.instance.playersTurn = false;
            nextlvl = 3;
            Invoke("ChangeScene", 0.5f);
        }
        if (other.CompareTag("To4"))
        {
            GameManager.instance.playersTurn = false;
            nextlvl = 4;
            Invoke("ChangeScene", 0.5f);
        }
    }

    public void Die(bool turret)            // True si muere por torreta, false si muere por frankenturret
    {
        /*if (turret)
            animator.SetTrigger("DieFromTurret");
        else
            animator.SetTrigger("DieFromFrankenturret");*/
        GameManager.instance.playing = false;
        Invoke("PlayerIsDead", 4);
    }

    public void NotWalking()
    {
    }

    private void PlayerIsDead()
    {
        GameManager.instance.ReloadScene();
    }

    public void SetDroppingCube(bool value)
    {
        droppingCube = value;
    }

    private void ChangeScene()
    {
        GameManager.instance.ChangeScene(nextlvl);
    }
}
