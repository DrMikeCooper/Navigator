using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour {

    public GameObject prefab;
    public bool success;

	// Use this for initialization
	void Start () {
        // add 20 random big cuboids into the level.
        // The prefab has been tagged as StaticBatchingUtility Navigation in The Navigation editor
        for (int i = 0; i < 20; i++)
        {
            GameObject go = Instantiate(prefab, new Vector3(Random.Range(-25, 25), Random.Range(0,1), Random.Range(-25, 25)), Quaternion.AngleAxis(Random.Range(-180,180), Vector3.up));
            go.transform.parent = transform;
        }

        // Use the standard settings from the editor (I think)
        NavMeshBuildSettings settings = NavMesh.GetSettingsByID(0);

        // gather all the physics colliders which are children of this transform (or you can do this by volume)
        List<NavMeshBuildSource> results = new List<NavMeshBuildSource>();
        NavMeshBuilder.CollectSources(transform, 255, NavMeshCollectGeometry.PhysicsColliders, 0,  new List<NavMeshBuildMarkup>(), results);

        // make a 100m box around the origin
        Bounds bounds = new Bounds(Vector3.zero, 100 * Vector3.one);

        // Build the actual navmesh
        NavMeshData data = NavMeshBuilder.BuildNavMeshData(settings, results, bounds, Vector3.zero, Quaternion.identity);
        NavMesh.AddNavMeshData(data);
        success = NavMeshBuilder.UpdateNavMeshData(data, settings, results, bounds);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
