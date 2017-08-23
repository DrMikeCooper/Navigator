using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour {

    public GameObject prefab;
    public bool success;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 20; i++)
        {
            GameObject go = Instantiate(prefab, new Vector3(Random.Range(-25, 25), Random.Range(0,1), Random.Range(-25, 25)), Quaternion.AngleAxis(Random.Range(-180,180), Vector3.up));
            go.transform.parent = transform;
        }

        // craete a navmesh
        NavMeshBuildSettings settings = new NavMeshBuildSettings();
        settings.agentRadius = 0.5f;
        settings.agentHeight = 2.0f;
        settings.agentSlope = 45;
        settings.tileSize = 3;
        settings.minRegionArea = 2;

        List<NavMeshBuildSource> results = new List<NavMeshBuildSource>();
        NavMeshBuilder.CollectSources(transform, 255, NavMeshCollectGeometry.PhysicsColliders, 0,  new List<NavMeshBuildMarkup>(), results);

        Bounds bounds = new Bounds(Vector3.zero, 30 * Vector3.one);

        NavMeshData data = NavMeshBuilder.BuildNavMeshData(settings, results, bounds, Vector3.zero, Quaternion.identity);

        success = NavMeshBuilder.UpdateNavMeshData(data, settings, results, bounds);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
