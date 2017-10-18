using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour {

    public GameObject enemy;
    private GameObject lastInstantiated;
    private int randomInt;
    private Vector3 randomPosition;
    public Vector2 sceneSize = new Vector2(531, 450);

    private void Start()
    {

        //DontDestroyOnLoad(transform.gameObject);
        //StartCoroutine(SpawnEnemysStart());
        for (int i = 0; i < 200; i++)
        {
            SpreadEnemy();
        }

    }

    IEnumerator SpawnEnemysStart()
    {
        for (int i = 0; i < 100; i++)
        {
            SpreadEnemy();
            yield return null;
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

    public void SpreadEnemy()
    {
        randomInt = Random.Range(0, 2);
        do {
            randomPosition = new Vector3(Random.Range(0,sceneSize.x)-sceneSize.x/2, 4.5f, Random.Range(0,sceneSize.y)-sceneSize.y/2);
        } while (Physics.OverlapCapsule(randomPosition - new Vector3(0,3f,0) ,randomPosition + new Vector3(0, 3f, 0), 0.5f).Length > 0);
        lastInstantiated = Instantiate(enemy, Vector3.zero, Quaternion.identity);
        lastInstantiated.GetComponent<EnemyMovement>().MoveToTarget(randomPosition, transform.GetChild(randomInt).GetChild(Random.Range(0, transform.GetChild(randomInt).childCount - 1)).gameObject);

    }
}
