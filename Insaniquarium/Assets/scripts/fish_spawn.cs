using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_spawn : MonoBehaviour {

    public GameObject smallFish;
    public float minX, maxX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void spawnSmallFish()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), 5.5f);
        GameObject fish = Instantiate(smallFish, spawnPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        fish.GetComponent<fish_movement2>().fall();
        fish.GetComponent<guppy_sound>().playSplashSound();


    }


}
