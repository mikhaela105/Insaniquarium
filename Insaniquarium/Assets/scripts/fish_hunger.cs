using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_hunger : MonoBehaviour {

    float hungerTimer;
    public Color hungryColor;
    public guppy_sound guppy_Sound;
    public bool dead = false, hungry=true;
    public float minY;

    public GameObject console;


	// Use this for initialization
	void Start () {
        hungerTimer = 10;
        guppy_Sound = this.GetComponent<guppy_sound>();
        console = GameObject.FindGameObjectWithTag("console");
	}
	
	// Update is called once per frame
	void Update () {
        hungerTimer -= Time.deltaTime;

        if (hungry)
        {
            GameObject[] food = GameObject.FindGameObjectsWithTag("food");
            if (food.Length > 0)
            {
                GameObject closest = food[0];
                
            }
        }

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
            hungry = true;
            // Hungry and look for food
            Color color = this.GetComponent<SpriteRenderer>().color;


        } else if (hungerTimer <= -10)
        { 
            // Die
            if (!dead)
            {
                guppy_Sound.playDieSound();
                this.GetComponent<Animator>().SetBool("isDead", true);
                this.GetComponent<fish_movement2>().enabled = false;
            }
            dead = true;
        }
        else
        {
            hungry = false;
        }
    }

    public void eat()
    {
        hungerTimer = 10;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "food")
        {
            console.GetComponent<food_spawn>().currentFoodCount--;
            Destroy(col.gameObject);
            eat();

            fish_movement2 m = this.GetComponent<fish_movement2>();
            int direction = Random.Range(0, 2);
            if (direction == 0)
            {
                m.goLeft();
            }
            else if (direction == 1)
            {
                m.goRight();
            }
        }
    }
}
