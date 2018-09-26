using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_hunger : MonoBehaviour {

    float hungerTimer;
    public Color hungryColor;
    public guppy_sound guppy_Sound;
    public fish_stats stats;
    public bool dead = false, hungry = true, becameHungry = false,testEat=false;
    public float minY;
    public int foodEaten = 0;

    public GameObject console;


	// Use this for initialization
	void Start () {
        hungerTimer = 10;
        guppy_Sound = this.GetComponent<guppy_sound>();
        console = GameObject.FindGameObjectWithTag("console");
        stats = this.GetComponent<fish_stats>();
	}
	
	// Update is called once per frame
	void Update () {
        hungerTimer -= Time.deltaTime;

        if (testEat)
        {
            eat();
            testEat = false;
        }

        if (hungry && !dead)
        {
            this.GetComponent<SpriteRenderer>().color = Color.green;
            if (!becameHungry)
            {
                this.GetComponent<fish_movement2>().accelerateToFoodSpeed = this.GetComponent<fish_movement2>().speedX;
                becameHungry = true;
            }
        }
        else
        {
            becameHungry = false;
            if (!dead)
            {
                this.GetComponent<SpriteRenderer>().color = Color.white;
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
        //TODO GET FOODS VALUES
        foodEaten++;

        if (foodEaten % 2 == 0)
        {
            stats.grow();
        }

        hungerTimer = 10;
        this.GetComponent<fish_movement2>().findClosestFood();
    }

    public void finishEating()
    {
        this.GetComponent<Animator>().SetBool("eat", false);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "food" && hungry)
        {
            this.GetComponent<guppy_sound>().playSlurpSound();
            this.GetComponent<Animator>().SetBool("eat", true);
            Invoke("finishEating", 0.3f);

            console.GetComponent<food_spawn>().currentFoodCount--;
            Destroy(col.gameObject);
            eat();
        }
    }
}
