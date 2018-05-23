using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 0.04f;

    private bool playerTurn;

	// Use this for initialization
	void Start () {
        playerTurn = true;

    }
	
	// Update is called once per frame
	void Update () {
		if (playerTurn)
        {
            if (Input.GetAxis("Vertical") > 0)
                transform.position += new Vector3(5.0f, 0.0f, 0.0f); // TODO: Movimiento sinusoidal
            if (Input.GetAxis("Vertical") < 0)
                transform.position += new Vector3(-5.0f, 0.0f, 0.0f); // TODO: Movimiento sinusoidal
            if (Input.GetAxis("Horizontal") > 0)
                transform.position += new Vector3(0.0f, 0.0f, -5.0f); // TODO: Movimiento sinusoidal
            if (Input.GetAxis("Horizontal") < 0)
                transform.position += new Vector3(0.0f, 0.0f, 5.0f); // TODO: Movimiento sinusoidal

            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                playerTurn = false;
                Invoke("Provisional", 2);
            }
        }
	}

    void Provisional()
    {
        playerTurn = true;
    }
}
