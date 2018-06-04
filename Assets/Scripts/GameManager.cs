using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public bool playersTurn, enemiesMoving, portalUsed, playing, setup;
    public GameObject cake1, cake2, cake3;

    private float turnDelay = 0.5f;
    private Vector3 orangePortalBegin, orangePortalEnd, orangePortalPos;
    private Vector3 bluePortalBegin, bluePortalEnd, bluePortalPos;
    private List<Enemy> enemies;
    private Vector3 nullVector = new Vector3(-1, -1, -1);
    private int levelsUnlocked;
    private int cake;
    private int numLvls = 3;
    private int currentLvl;
    private GameObject cakeObj;

    private List<Vector2> collectables;         //x: Cube   y: Cake

    // Use this for initialization
    void Awake () {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        collectables = new List<Vector2>();
        for (int i = 0; i < numLvls; i++)
            collectables.Add(Vector2.zero);
        enemies = new List<Enemy>();
        levelsUnlocked = 0;
        currentLvl = 0;
        playing = false;
        playersTurn = false;
        enemiesMoving = false;
        setup = false;
        cake = 0;
        //InitGame(); // Eliminar
    }

    public void InitGame()
    {
        orangePortalBegin = nullVector;
        orangePortalEnd = nullVector;
        bluePortalBegin = nullVector;
        bluePortalEnd = nullVector;

        enemies.Clear();

        playersTurn = false;
        portalUsed = false;
        enemiesMoving = false;
        playing = true;
        setup = true;
        Invoke("NotSetup", 1);
        Invoke("PlayerTurn", 1);
    }

	// Update is called once per frame
	void Update () {
        if (playersTurn || enemiesMoving || !playing || setup)
            return;
        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;

        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(turnDelay);
            if (portalUsed)
            {
                orangePortalBegin = nullVector;
                orangePortalEnd = nullVector;
                bluePortalBegin = nullVector;
                bluePortalEnd = nullVector;

                Destroy(GameObject.Find("BluePortal"));
                Destroy(GameObject.Find("BluePortalBegin"));
                Destroy(GameObject.Find("OrangePortal"));
                Destroy(GameObject.Find("OrangePortalBegin"));
            }
                
        }
        Invoke("PlayerTurn",0.4f);
    }

    private void PlayerTurn()
    {
        if (playing)
        {
            playersTurn = true;
            enemiesMoving = false;
        }
    }

    private void NotSetup()
    {
        setup = false;
    }

    public void SetCurrentLvl(int lvl)
    {
        currentLvl = lvl;
    }

    public int GetCurrentLvl()
    {
        return currentLvl;
    }
    

    public void SetCubeCollected()
    {
        collectables[currentLvl-1] = new Vector2(1, collectables[currentLvl-1].y);
    }

    public void SetCakeCollected()
    {
        if (collectables[currentLvl].y == 0)
        {
            Debug.Log("CAKE");
            collectables[currentLvl-1] = new Vector2(collectables[currentLvl-1].x, 1);
            ++cake;
            if (cake == 1)
                cakeObj = Instantiate(cake1);
            if (cake == 2)
                cakeObj = Instantiate(cake2);
            if (cake == 3)
                cakeObj = Instantiate(cake3);
            Invoke("InactiveCake", 1.5f);
        }
    }

    private void InactiveCake()
    {
        Destroy(cakeObj);

    }


    public bool CubeCollected()
    {
        if (collectables[currentLvl-1].x == 1)
            return true;
        return false;
    }

    public bool CakeCollected()
    {
        if (collectables[currentLvl-1].y == 1)
            return true;
        return false;
    }

    public Vector3 GetOrangePortalPos()
    {
        return orangePortalPos;
    }

    public Vector3 GetOrangePortalBegin()
    {
        return orangePortalBegin;
    }

    public Vector3 GetOrangePortalEnd()
    {
        return orangePortalEnd;
    }

    public Vector3 GetBluePortalPos()
    {
        return bluePortalPos;
    }

    public Vector3 GetBluePortalBegin()
    {
        return bluePortalBegin;
    }

    public Vector3 GetBluePortalEnd()
    {
        return bluePortalEnd;
    }

    public void SetOrangePortalPos(Vector3 portalPos)
    {
        orangePortalPos = portalPos;
    }

        public void SetOrangePortal(Vector3 portal)
    {
        if (portal == nullVector)
        {
            orangePortalBegin = portal;
            orangePortalEnd = portal;
            return;
        }
        if (portal.z % 5 != 0)              // Portal vertical
        {
            orangePortalBegin = new Vector3(portal.x,portal.y,portal.z - 2.5f);
            orangePortalEnd = new Vector3(portal.x,portal.y,portal.z + 2.5f);
        }
        else if (portal.x % 5 != 0)              // Portal vertical
        {
            orangePortalBegin = new Vector3(portal.x - 2.5f, portal.y, portal.z);
            orangePortalEnd = new Vector3(portal.x + 2.5f, portal.y, portal.z);
        }
        else                                // Portal horizontal
        {
            orangePortalBegin = portal;
            orangePortalEnd = portal;
        }
    }


    public void SetBluePortalPos(Vector3 portalPos)
    {
        bluePortalPos = portalPos;
    }

    public void SetBluePortal(Vector3 portal)
    {
        if (portal == nullVector)
        {
            bluePortalBegin = portal;
            bluePortalEnd = portal;
            return;
        }
        if (portal.z % 5 != 0)              // Portal vertical
        {
            bluePortalBegin = new Vector3(portal.x, portal.y, portal.z - 2.5f);
            bluePortalEnd = new Vector3(portal.x, portal.y, portal.z + 2.5f);
        }
        else if (portal.x % 5 != 0)              // Portal vertical
        {
            bluePortalBegin = new Vector3(portal.x - 2.5f, portal.y, portal.z);
            bluePortalEnd = new Vector3(portal.x + 2.5f, portal.y, portal.z);
        }
        else                                // Portal horizontal
        {
            bluePortalBegin = portal;
            bluePortalEnd = portal;
        }
    }

    public void ChangeScene(int scene)
    {

        //fade.GetComponent<Animator>().SetTrigger("FadeOut");

        InitGame();
        currentLvl = scene;
        SceneManager.LoadScene(scene);

    }

    public void ReloadScene()
    {

        //fade.GetComponent<Animator>().SetTrigger("FadeOut");
        InitGame();

        SceneManager.LoadScene(currentLvl);

    }

    public void OnMenu(bool menuActive)
    {
        if (menuActive)
            setup = true;
        else
            setup = false;
        Debug.Log(setup);
    }
}
