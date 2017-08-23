using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : MonoBehaviour {

    float timer;

	// Update is called once per frame
	void Update () {
        if (timer <= 0)
        {
            transform.position = new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30));
            timer = 3;
        }
        else
            timer -= Time.deltaTime;
	}
}
