using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public bool playersTurn, enemiesMoving;
    private float turnDelay = 0.5f;

    private Vector3 orangePortalBegin, orangePortalEnd;
    private Vector3 bluePortalBegin, bluePortalEnd;
    private List<Enemy> enemies;
    private Vector3 nullVector = new Vector3(-1, -1, -1);
    // Use this for initialization
    void Awake () {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        orangePortalBegin = nullVector;
        orangePortalEnd = nullVector;
        bluePortalBegin = nullVector;
        bluePortalEnd = nullVector;

        
        playersTurn = false;
        enemiesMoving = true;
        Invoke("PlayerTurn", 2);
        enemies = new List<Enemy>();
    }
	
	// Update is called once per frame
	void Update () {
        if (playersTurn || enemiesMoving)
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
        }
        Invoke("PlayerTurn",0.1f);
    }

    private void PlayerTurn()
    {
        playersTurn = true;
        enemiesMoving = false;
    }

    private void SetPlayersTurn()
    {
        playersTurn = true;
    }

    public Vector3 GetOrangePortalBegin()
    {
        return orangePortalBegin;
    }

    public Vector3 GetOrangePortalEnd()
    {
        return orangePortalEnd;
    }

    public Vector3 GetBluePortalBegin()
    {
        return bluePortalBegin;
    }

    public Vector3 GetBluePortalEnd()
    {
        return bluePortalEnd;
    }

    public void SetOrangePortal(Vector3 portal)
    {
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

    public void SetBluePortal(Vector3 portal)
    {
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
}
