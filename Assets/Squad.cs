using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour {

    public int size;
    public float range;
    public float spread;

    public GameObject[] markers;
    public GameObject prefab;

	// Use this for initialization
	void Start ()
    {
        markers = new GameObject[size];
        for (int i = 0; i < size; i++)
        {
            markers[i] = Instantiate(prefab);
            Wanderer w = markers[i].AddComponent<Wanderer>();
            w.squad = this;
        }
	}
}
