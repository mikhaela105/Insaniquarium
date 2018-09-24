using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_movement2 : MonoBehaviour {

    public float speedX, speedXMax, acceleration;
    public float speedY, minX, maxX, minY, maxY, fallSpeed;

    public float moveForMin, moveForMax;

    public Vector2 boundaryX;
    public Vector2 boundaryY;

    public float moveForTime;

    public Animator animator;

    public bool moveRight, moveLeft=true;
    public bool turning = false;
    public bool onEdge = false;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (fallSpeed > 0)
        {
            fallSpeed -= 0.1f;
            this.transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
            return;
        }

        if (turning)
        {

        }else { //moving

            if (moveForTime <= 1)
            {
                speedX -= acceleration;
            }
            else if (speedX < speedXMax)
            {
                speedX += acceleration;
            }

            if (moveRight)
            {
                if (this.transform.position.x < maxX)
                {
                    this.transform.Translate(Vector2.right * speedX * Time.deltaTime);
                    onEdge = false;
                }
                else
                {
                    onEdge = true;
                }
                    
            }else if (moveLeft)
            {
                if (this.transform.position.x > minX)
                {
                    this.transform.Translate(Vector2.left * speedX * Time.deltaTime);
                    onEdge = false;
                }
                else
                {
                    onEdge = true;
                }
            }

            moveForTime -= Time.deltaTime;
            if (moveForTime <= 0)
            {
                if (onEdge)
                {
                    if (moveLeft)
                    {
                        goRight();
                    }
                    else
                    {
                        goLeft();
                    }
                }
                else
                {
                    int direction = Random.Range(0, 2);
                    if (direction == 0)
                    {
                        goLeft();
                    }
                    else if (direction == 1)
                    {
                        goRight();
                    }

                }
                moveForTime = Random.Range(moveForMin, moveForMax);
            }

        }

	}

    public void goRight()
    {
        speedX = 0;
        speedXMax = Random.Range(0.1f, 4);
        if (moveLeft)
        {
            StartCoroutine(turn());
        }
        moveRight = true;
        moveLeft = false;
    }

    public void goLeft()
    {
        speedX = 0;
        speedXMax = Random.Range(0.1f, 4);
        if (moveRight)
        {
            StartCoroutine(turn());
        }
        moveLeft = true;
        moveRight = false;
    }

    public IEnumerator turn()
    {
        turning = true;
        animator.SetBool("turn", true);
        yield return new WaitForSeconds(0.5f);//animation turn length
        this.transform.localScale = new Vector2(-this.transform.localScale.x, this.transform.localScale.y);
        animator.SetBool("turn", false);
        turning = false;
    }

    public void fall()
    {
        fallSpeed = Random.RandomRange(4f, 11f);
    }
}
