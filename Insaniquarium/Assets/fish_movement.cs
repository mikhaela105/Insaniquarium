using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_movement : MonoBehaviour {

	public float speed, minX, maxX, minY, maxY;
    public bool turning;

	public bool left, right;
	public Animator anim = new Animator();

    public Vector2 gotoPosition;

	// Use this for initialization
	void Start () {
		anim=this.GetComponent<Animator>();
        gotoPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        if (gotoPosition.x <= this.transform.position.x)
        {
            left = true;
        }
        else
        {
            right = true;
            turning = true;
            StartCoroutine(turn());
        }

	}
	
	// Update is called once per frame
	void Update () {

        if (right)
        {
            this.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (left)
        {
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

	public IEnumerator turn()
    {
        float waitTime = 0;
        bool flip = false;

        // Make new position
        gotoPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        

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
                waitTime = anim.GetCurrentAnimatorStateInfo(0).length;
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
                waitTime = anim.GetCurrentAnimatorStateInfo(0).length ;
                Debug.Log(anim.GetCurrentAnimatorStateInfo(0).length);
                flip = true;
            }
        }

        yield return new WaitForSeconds(waitTime-0.3f);
        if (flip)
        {
            this.transform.localScale = new Vector2(-this.transform.localScale.x, this.transform.localScale.y);
        }

        anim.SetBool("turn", false);
        int newSpeed = Random.Range(1, 4);
        speed = newSpeed;
        anim.speed = newSpeed;
        turning = false;
    }
}
