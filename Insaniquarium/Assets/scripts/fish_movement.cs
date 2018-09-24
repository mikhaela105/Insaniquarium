using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_movement : MonoBehaviour {

	public float speed, minX, maxX, minY, maxY;
    public bool turning;
    public bool falling = false;

	public bool left, right;
	public Animator anim = new Animator();

    public Vector2 gotoPosition;
    public float speedY;
    public float gotoSpeed;

    float fallSpeed = 0;

	// Use this for initialization
	void Start () {
		anim=this.GetComponent<Animator>();
        //gotoPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        gotoPosition.y = Random.Range(minY, maxY);
        speedY = Random.Range(0.2f, 5);
        StartCoroutine(turn());
	}
	
	// Update is called once per frame
	void Update () {


        if (speed <= gotoSpeed)
        {
            speed += 0.05f;
        }

        if (fallSpeed>0)
        {
            fallSpeed -= 0.1f;
            this.transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        }
        else
        { 
            if (this.transform.position.y <= gotoPosition.y + 0.1f && this.transform.position.y >= gotoPosition.y - 0.1f)
            {
                gotoPosition.y = Random.Range(minY, maxY);
                speedY = Random.Range(0.2f, 1);
            }

            // If fish below position
            if (this.transform.position.y <= gotoPosition.y)
            {
                this.transform.Translate(Vector2.up * speedY * Time.deltaTime);

            }
            else
            {
                this.transform.Translate(Vector2.down * speedY * Time.deltaTime);

            }


            if (right)
            {
                if (this.transform.position.x > gotoPosition.x - speed && speed > 0.5f)
                {
                    speed -= 0.05f;
                }
                this.transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else if (left)
            {
                if (this.transform.position.x < gotoPosition.x + speed && speed > 0.2f)
                {
                    speed -= 0.05f;
                }
                this.transform.Translate(Vector2.left * speed * Time.deltaTime);
            }

            if (!turning)
            {
                if (this.transform.position.x >= gotoPosition.x && right)
                {
                    turning = true;
                    StartCoroutine(turn());

                }
                if (this.transform.position.x <= gotoPosition.x && left)
                {
                    turning = true;
                    StartCoroutine(turn());
                }
            }
        }
	}

	public IEnumerator turn()
    {
        float waitTime = 0;
        bool flip = false;

        // Make new position
        gotoPosition.x = Random.Range(minX, maxX);

        anim.speed = 1;

        speed = 0;
        speedY = 0;

        yield return new WaitForSeconds(Random.RandomRange(0.1f, 2f));

        // If position is left of fish
        if (gotoPosition.x <= this.transform.position.x)
        {
            // If fish not facing left
            if (!left)
            {
                right = false;
                left = true;
                speed = 0;
                anim.SetBool("turn", true);
                waitTime = 0.5f;
                flip = true;
            } 
        } else if (gotoPosition.x > this.transform.position.x)
        {
            if (!right)
            {
                right = true;
                left = false;
                speed = 0;
                anim.SetBool("turn", true);
                waitTime = 0.5f;
                flip = true;
            }
        }

        yield return new WaitForSeconds(waitTime);




        if (flip)
        {
            this.transform.localScale = new Vector2(-this.transform.localScale.x, this.transform.localScale.y);
        }

        gotoSpeed = speed;
        speed = 0;
        speedY = Random.Range(0.1f, 1);
        anim.SetBool("turn", false);
        int newSpeed = Random.Range(1, 4);
        speed = newSpeed;
        anim.speed = newSpeed;
        turning = false;
    }

    public void fall()
    {
        fallSpeed = Random.RandomRange(4f, 7f);
        //StartCoroutine(fallFor(Random.Range(1, 2)));
    }
}
