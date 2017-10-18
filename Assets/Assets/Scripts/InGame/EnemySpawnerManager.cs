using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour {

    public GameObject enemy;
    private GameObject lastInstantiated;
    private int randomInt;

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        StartCoroutine(SpawnEnemysStart());
    }

    IEnumerator SpawnEnemysStart()
    {
        for (int i = 0; i < 100; i++)
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
        randomInt = Random.Range(0, 2);
        lastInstantiated = Instantiate(enemy, Vector3.zero, Quaternion.identity);
        lastInstantiated.GetComponent<EnemyMovement>().MoveToTarget(transform.GetChild(0).GetChild(Random.Range(0, transform.GetChild(0).childCount - 1)).GetChild(Random.Range(0, 3)).position, transform.GetChild(randomInt).GetChild(Random.Range(0, transform.GetChild(randomInt).childCount - 1)).gameObject);
    }
}
