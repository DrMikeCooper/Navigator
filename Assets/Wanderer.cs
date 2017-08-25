using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wanderer : MonoBehaviour {

    float timer;
    GameObject player;
    NavMeshAgent nv;
    public bool lineOfSight;
    public Squad squad;
    GameObject[] allies;

    void Start()
    {
        player = GameObject.Find("player");
        nv = gameObject.AddComponent<NavMeshAgent>();
        nv.speed = 6;

        allies = squad.markers;
    }

	// Update is called once per frame
	void Update ()
    {
        Ray ray = new Ray();
        ray.direction = (player.transform.position - transform.position).normalized;
        ray.origin = transform.position + Vector3.up;
        RaycastHit hit;

        lineOfSight = false;
        if (Physics.Raycast(ray, out hit, squad.range))
        {
            lineOfSight = (hit.collider.gameObject == player);
        }

        if (!lineOfSight)
            nv.SetDestination(player.transform.position);
        else
        {
            float squadDist = squad.spread;

            Vector3 target = transform.position;

            Vector3 meToPlayer = player.transform.position - transform.position;
            float meDist = meToPlayer.magnitude;
            Vector3 meDistNorm = meToPlayer / meDist;
            Vector3 sideWays = Vector3.Cross(meDistNorm,Vector3.up); // normalised sideways direction

            foreach (GameObject ally in allies)
            {
                if (ally.Equals(this) == false)
                {
                    Vector3 youToPlayer = player.transform.position - ally.transform.position;
                    float youDist = youToPlayer.magnitude;

                    // check if you're blocking our view
                    if (Vector3.Dot(meToPlayer, youToPlayer) > 0)
                    {
                        float sideDist = Vector3.Dot(sideWays, youToPlayer);
                        if (sideDist > 0 && sideDist < squadDist)
                            target += sideWays + 0.1f * meDistNorm;
                        if (sideDist < 0 && sideDist > -squadDist)
                            target -= sideWays - 0.1f * meDistNorm;
                    }
                }
            }
            nv.SetDestination(target);
        }

	}
}
