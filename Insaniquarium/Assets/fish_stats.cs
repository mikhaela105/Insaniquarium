using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_stats : MonoBehaviour {

    public bool kingFish, bigFish, medFish, smallFish;
    public RuntimeAnimatorController[] levels;
    public Animator anim;
    public int level = 0;

	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
        smallFish = true;
        anim.runtimeAnimatorController = levels[level];
	}
	
	// Update is called once per frame
	void Update () {
		


	}

    public void grow()
    {
        level++;
        if (level < levels.Length)
        {
            anim.runtimeAnimatorController = levels[level];
        }
    }

    public void makeMedFish()
    {
        smallFish = false;
        medFish = true;
    }

}
