using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour {

    public GameObject enemy;
    private GameObject lastInstantiated;

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        StartCoroutine(SpawnEnemysStart());
    }

    IEnumerator SpawnEnemysStart()
    {
        for (int i = 0; i < 50; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnEnemy();
        }
	}

    public void SpawnEnemy()
    {
        lastInstantiated = Instantiate(enemy,transform.GetChild(Random.Range(0,transform.childCount)).GetChild(Random.Range(0,3)).position, Quaternion.identity);
        lastInstantiated.GetComponent<EnemyMovement>().MoveToTarget(transform.GetChild(Random.Range(0, transform.childCount - 1)).GetChild(Random.Range(0, 3)).position, transform.GetChild(Random.Range(0, transform.childCount - 1)).GetChild(Random.Range(0, 3)).position);
    }
}
