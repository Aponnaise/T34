using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour {

    public GameObject enemy;
    private GameObject lastInstantiated;
    private int randomInt;
    private Vector3 randomPosition;
    public Vector2 sceneSize = new Vector2(531, 450);
    public int enemyCount;
    private GameObject PlayerSpawns;
    private Vector3 spawnPosition;
    private GameObject targetObject;
    private RaycastHit hit;
    public GameObject bottomLeft, topRight;
    private GameObject player;
    private int skipedIterations = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("1");
        bottomLeft = GameObject.Find("Spawner (3)");
        Debug.Log("2");
        topRight = GameObject.Find("Spawner (11)");
        Debug.Log("3");
        PlayerSpawns = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).gameObject;
        Debug.Log("4");
        for (int i = 0; i < enemyCount; i++)
        {
           SpawnEnemy();
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        /*randomInt = Random.Range(0, 2);
        lastInstantiated = Instantiate(enemy, Vector3.zero, Quaternion.identity);
        lastInstantiated.GetComponent<EnemyMovement>().MoveToTarget(transform.GetChild(0).GetChild(Random.Range(0, transform.GetChild(0).childCount - 1)).GetChild(Random.Range(0, 3)).position, transform.GetChild(randomInt).GetChild(Random.Range(0, transform.GetChild(randomInt).childCount - 1)).gameObject);
        */

        skipedIterations = 0;

        do
        {
            do
            {
                skipedIterations++;
                spawnPosition = PlayerSpawns.transform.GetChild(Random.Range(0, 32)).position + new Vector3(0*Random.Range(0, -10*player.GetComponent<PlayerBehav>().directionX), 0, 0*Random.Range(0, -10*player.GetComponent<PlayerBehav>().directionY));
            } while (!Physics.Raycast(spawnPosition, Vector3.down, out hit, 20));
        } while (skipedIterations < 50 && (Physics.OverlapCapsule(spawnPosition - new Vector3(0, 3f, 0), spawnPosition + new Vector3(0, 3f, 0), 0.5f).Length > 0 || hit.transform.name != "Plane"));

        do
        {
            do
            {
                skipedIterations++;
                targetObject = PlayerSpawns.transform.GetChild(Random.Range(0, 32)).gameObject;
            } while (!Physics.Raycast(targetObject.transform.position, Vector3.down, out hit, 20));
        } while (skipedIterations < 50 && (Physics.OverlapCapsule(targetObject.transform.position - new Vector3(0, 3f, 0), targetObject.transform.position + new Vector3(0, 3f, 0), 0.5f).Length > 0 || hit.transform.name != "Plane"));

        if (skipedIterations < 50)
        {
            lastInstantiated = Instantiate(enemy, Vector3.zero, Quaternion.identity);
            lastInstantiated.GetComponent<EnemyMovement>().MoveToTarget(spawnPosition, targetObject);
        } else
        {
            StartCoroutine(delaySpawn());
        }
        
    }

    IEnumerator delaySpawn()
    {
        yield return new WaitForSeconds(0.1f);
        SpawnEnemy();
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
