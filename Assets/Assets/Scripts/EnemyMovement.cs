using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    Vector3 target;
    NavMeshAgent agent;


    public void MoveToTarget (Vector3 position, Vector3 destination) {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        agent.Warp(position);
        agent.speed = 7 + Mathf.Pow(Random.Range(0f, 3f), 2f);
        target = destination;
        agent.destination = destination;
        StartCoroutine(setTarget());
	}

    IEnumerator setTarget()
    {
        yield return new WaitForSeconds(1);
        agent.destination = target; 
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target) < 10)
        {
            GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawnerManager>().SpawnEnemy();
            Destroy(gameObject);
        }
    }
}
