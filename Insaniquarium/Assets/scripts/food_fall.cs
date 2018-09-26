using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food_fall : MonoBehaviour {

    public GameObject console;
    public float minY;
    public float foodValue;

	// Use this for initialization
	void Start () {
        console = GameObject.FindGameObjectWithTag("console");
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = this.transform.position;
        pos.z = 0;

        this.transform.position = pos;
        if (this.transform.position.y >= minY)
        {
            this.transform.Translate(Vector3.down * 2 * Time.deltaTime);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(50, 0, 0);
            Color color = this.GetComponent<SpriteRenderer>().color;
            color.a -= 0.01f;
            this.GetComponent<SpriteRenderer>().color = color;

            if (color.a <= 0)
            {
                console.GetComponent<food_spawn>().currentFoodCount--;
                Destroy(this.gameObject);
            }
        }

    }
}
