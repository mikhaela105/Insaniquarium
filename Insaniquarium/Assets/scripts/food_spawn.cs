using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food_spawn : MonoBehaviour {

    public GameObject biscuitObject;

    public int currentFoodCount = 0;
    public int maxFoodCount = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetMouseButtonDown(0) && Camera.main.ScreenToWorldPoint(Input.mousePosition).y <3.3f)
        {
            if  (currentFoodCount < maxFoodCount)
            {
                currentFoodCount++;
                Instantiate(biscuitObject,Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.Euler(0,0,0));

                GameObject[] fish = GameObject.FindGameObjectsWithTag("guppy");
                for (int i = 0; i < fish.Length; i++)
                {
                    fish[i].GetComponent<fish_movement2>().findClosestFood();
                }
            }
        }

	}
}
