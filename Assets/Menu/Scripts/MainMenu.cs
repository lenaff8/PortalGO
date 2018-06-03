using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    private Camera cam;
    public Canvas totalMenu;
    public GameObject mainMenu, controlsMenu, creditsMenu, exitMenu;
    private bool inMenu;

    private void Start()
    {
        cam = Camera.main;
        inMenu = true;
    }

    private void Update()
    {
        if (!inMenu)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                cam.GetComponent<Animator>().Play("toMenu");
                totalMenu.gameObject.SetActive(true);
                totalMenu.GetComponent<Animator>().Play("toMenuCanvas");
                mainMenu.GetComponent<CanvasGroup>().interactable = true;
                GameManager.instance.playing = false;
                inMenu = true;
            }
        }
    }

    public void ToScene()
    {
        Invoke("SetInMenuFalse",1);
        GameManager.instance.playing = true;
        GameManager.instance.playersTurn = true;
    }

    public void ExitMainMenu()
    {
        mainMenu.GetComponent<Animator>().Play("exitMainMenu");
        Invoke("InactiveMainMenu",0.5f);

    }

    public void ExitControlsMenu()
    {
        controlsMenu.GetComponent<Animator>().Play("exitControlsMenu");
        Invoke("InactiveControlsMenu", 1.0f);

    }

    public void ExitCreditsMenu()
    {
        creditsMenu.GetComponent<Animator>().Play("exitCreditsMenu");
        Invoke("InactiveCreditsMenu", 1.0f);

    }

    public void ExitExitMenu()
    {
        exitMenu.GetComponent<Animator>().Play("exitExitMenu");
        Invoke("InactiveExitMenu", 1.0f);

    }

    public void GoMainMenu()
    {
        Invoke("ActiveMainMenu", 0.5f);

    }

    private void InactiveMainMenu()
    {
        mainMenu.gameObject.SetActive(false);
    }

    private void InactiveControlsMenu()
    {
        controlsMenu.gameObject.SetActive(false);
    }

    private void InactiveCreditsMenu()
    {
        creditsMenu.gameObject.SetActive(false);
    }

    private void InactiveExitMenu()
    {
        exitMenu.gameObject.SetActive(false);
    }
    
    private void ActiveMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
        mainMenu.GetComponent<CanvasGroup>().interactable = true;
        mainMenu.GetComponent<Animator>().Play("toMainMenu");
    }

    private void SetInMenuFalse()
    {
        totalMenu.gameObject.SetActive(false);
        inMenu = false;
    }


}
