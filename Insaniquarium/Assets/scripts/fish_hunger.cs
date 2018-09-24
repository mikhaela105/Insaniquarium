using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_hunger : MonoBehaviour {

    float hungerTimer;
    public Color hungryColor;
    public guppy_sound guppy_Sound;
    public bool dead = false;
    public float minY;

	// Use this for initialization
	void Start () {
        hungerTimer = 10;
        guppy_Sound = this.GetComponent<guppy_sound>();
	}
	
	// Update is called once per frame
	void Update () {
        hungerTimer -= Time.deltaTime;

        if (dead)
        {
            // we must fall
            if (this.transform.position.y >= minY)
            {
                this.transform.Translate(Vector2.down * 2 * Time.deltaTime);

            } else
            {
                this.transform.rotation = Quaternion.Euler(50, 0, 0);
                Color color = this.GetComponent<SpriteRenderer>().color;
                color.a -= 0.01f;
                this.GetComponent<SpriteRenderer>().color = color;

                if (color.a <= 0)
                {
                    Destroy(this.gameObject);
                }
            }

        }

        if (hungerTimer <=0 && hungerTimer > -10)
        {
            // Hungry and look for food
            Debug.Log("FISH IS HUNGRY");
            Color color = this.GetComponent<SpriteRenderer>().color;

        } else if (hungerTimer <= -10)
        {
            // Die
            if (!dead)
            {
                Debug.Log("FISH IS DEAD");
                guppy_Sound.playDieSound();
                this.GetComponent<Animator>().SetBool("isDead", true);
                this.GetComponent<fish_movement2>().enabled = false;
            }
            dead = true;


        }
    }
}
