using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoScene : MonoBehaviour {

    public Sprite[] nums;
    public Sprite cubeSprite, cakeSprite;
    public Image numLvl, cube, cake;

	// Use this for initialization
	void Start () {
        numLvl.sprite = nums[GameManager.instance.GetCurrentLvl()];
        if (GameManager.instance.CubeCollected())
            cube.sprite = cubeSprite;
        if (GameManager.instance.CakeCollected())
            cake.sprite = cakeSprite;

    }

    private void Update()
    {
        if (GameManager.instance.CubeCollected())
            cube.sprite = cubeSprite;
        if (GameManager.instance.CakeCollected())
            cake.sprite = cakeSprite;
    }

    private void CubeCollected()
    {
        cube.sprite = cubeSprite;
    }

    private void CakeCollected()
    {
        cake.sprite = cakeSprite;
    }

}
