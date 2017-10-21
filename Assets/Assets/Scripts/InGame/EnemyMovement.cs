using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    private GameObject target;
    private GameObject player;
    private GameObject enemyManager;
    private NavMeshAgent agent;
    private Vector2 position1 = new Vector2(0,0), position2;
    private float directionX, directionY;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    string spriteHelp;
    public Sprite Up;
    public Sprite Right;
    public Sprite Down;
    public Sprite Left;
    public Sprite UpLeft;
    public Sprite UpRight;
    public Sprite DownRight;
    public Sprite DownLeft;
    private bool isFinalTarget;

    private int destinationRange = 30;
    private int safetyRange = 2;
    private float despawnDistance = 60;
    private float despawnSafety = 1f;

    private int ClassID = 0;


    private void Awake()
    {
        enemyManager = GameObject.FindGameObjectWithTag("EnemySpawner");
        player = GameObject.FindGameObjectWithTag("Player");
        ClassID = Random.Range(0, 4);
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();   
    }

    public void MoveToTarget (Vector3 position, GameObject targetObject) {
        if (targetObject.tag == "Spawner")
        {
            isFinalTarget = true;
            //targetObject = targetObject.transform.GetChild(Random.Range(0, 3)).gameObject;
        } else 
        {
            isFinalTarget = false;
        }
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        agent.Warp(position);

        switch (ClassID)
        {
            case 0:
                agent.speed = 2 + Mathf.Pow(Random.Range(0f, 2f), 2f);
                break;
            case 1:
                agent.speed = 5 + Mathf.Pow(Random.Range(0f, 2f), 2f);
                break;
            case 2:
                agent.speed = 7 + Mathf.Pow(Random.Range(0f, 3f), 2f);
                break;
            default:
                agent.speed = 7 + Mathf.Pow(Random.Range(0f, 4f), 2f);
                break;
        }
        
        target = targetObject;
        agent.destination = targetObject.transform.position;
        StartCoroutine(setTarget());
	}

    IEnumerator setTarget()
    {
        yield return new WaitForSeconds(0.02f);
        agent.destination = target.transform.position;
    }

    IEnumerator waitAtStand()
    {
        yield return new WaitForSeconds(Random.Range(5f,15f));
        StartCoroutine(setTarget());
    }

    private void FixedUpdate()
    {
        if (new Vector2(transform.position.x - target.transform.position.x, transform.position.z - target.transform.position.z).magnitude < destinationRange && !isFinalTarget || new Vector2(transform.position.x - target.transform.position.x, transform.position.z - target.transform.position.z).magnitude < safetyRange && isFinalTarget
            //|| Vector3.Distance(transform.position, player.transform.position) > despawnDistance)
            || transform.position.x - despawnSafety > enemyManager.GetComponent<EnemySpawnerManager>().topRight.transform.position.x
            || transform.position.z - despawnSafety > enemyManager.GetComponent<EnemySpawnerManager>().topRight.transform.position.z
            || transform.position.x + despawnSafety < enemyManager.GetComponent<EnemySpawnerManager>().bottomLeft.transform.position.x
            || transform.position.z + despawnSafety < enemyManager.GetComponent<EnemySpawnerManager>().bottomLeft.transform.position.z)
        {
            if (isFinalTarget)
            {
                GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawnerManager>().SpawnEnemy();
                Destroy(gameObject);
            } else
            {
                target = GameObject.FindGameObjectWithTag("EnemySpawner").transform.GetChild(0).GetChild(Random.Range(0, GameObject.FindGameObjectWithTag("EnemySpawner").transform.GetChild(0).childCount - 1)).GetChild(Random.Range(0,2)).gameObject;
                isFinalTarget = true;
                StartCoroutine(waitAtStand());
            }
            
        } else
        {
            agent.destination = target.transform.position;
        }

        #region Animation

        position2 = new Vector2(transform.position.x, transform.position.z);
        directionX = position2.x - position1.x;
        directionY = position2.y - position1.y;
        animator.enabled = true;

        if (directionX < 0)
        {  //LEFT
            if (directionY < 0)
            {  //DOWN
                if (directionY <= directionX)
                {  //DOWN
                    Direction(false, false, -directionY / -directionX >= 3.3, false, false, false, false, -directionY / -directionX < 3.3);
                    if (-directionY / -directionX >= 2.5) spriteHelp = "Down";
                    if (-directionY / -directionX < 2.5) spriteHelp = "DownLeft";
                }
                else
                {  //LEFT
                    Direction(false, false, false, -directionY / -directionX < 0.33, false, false, false, -directionY / -directionX >= 0.33);
                    if (-directionY / -directionX < 0.4) spriteHelp = "Left";
                    if (-directionY / -directionX >= 0.4) spriteHelp = "DownLeft";
                }
            }
            else
            {  //UP
                if (directionY >= -directionX)
                {  //UP
                    Direction(directionY / -directionX >= 3.3, false, false, false, directionY / -directionX < 3.3, false, false, false);
                    if (directionY / -directionX >= 2.5) spriteHelp = "Up";
                    if (directionY / -directionX < 2.5) spriteHelp = "UpLeft";
                }
                else
                {  //LEFT
                    Direction(false, false, false, directionY / -directionX < 0.33, directionY / -directionX >= 0.33, false, false, false);
                    if (directionY / -directionX < 0.4) spriteHelp = "Left";
                    if (directionY / -directionX >= 0.4) spriteHelp = "UpLeft";
                }
            }
        }
        else if (directionX > 0)
        {  //RIGHT
            if (directionY < 0)
            {  //DOWN
                if (-directionY < directionX)
                {  //RIGHT
                    Direction(false, -directionY / directionX < 0.33, false, false, false, false, -directionY / directionX >= 0.33, false);
                    if (-directionY / directionX < 0.4) spriteHelp = "Right";
                    if (-directionY / directionX >= 0.4) spriteHelp = "DownRight";
                }
                else
                {  //DOWN
                    Direction(false, false, -directionY / directionX >= 3.3, false, false, false, -directionY / directionX < 3.3, false);
                    if (-directionY / directionX >= 2.5) spriteHelp = "Down";
                    if (-directionY / directionX < 2.5) spriteHelp = "DownRight";
                }
            }
            else
            {  //UP
                if (directionY >= directionX)
                {  //UP
                    Direction(directionY / directionX >= 3.3, false, false, false, false, directionY / directionX < 3.3, false, false);
                    if (directionY / directionX >= 2.5) spriteHelp = "Up";
                    if (directionY / directionX < 2.5) spriteHelp = "UpRight";
                }
                else
                {  //RIGHT
                    Direction(false, directionY / directionX < 0.33, false, false, false, directionY / directionX >= 0.33, false, false);
                    if (directionY / directionX < 0.4) spriteHelp = "Right";
                    if (directionY / directionX >= 0.4) spriteHelp = "UpRight";
                }
            }
        }
        else
        {  //NO MOVEMENT
            Direction(false, false, false, false, false, false, false, false);
            animator.enabled = false;
            if (spriteHelp == "Up") spriteRenderer.sprite = Up;
            if (spriteHelp == "Right") spriteRenderer.sprite = Right;
            if (spriteHelp == "Down") spriteRenderer.sprite = Down;
            if (spriteHelp == "Left") spriteRenderer.sprite = Left;
            if (spriteHelp == "UpLeft") spriteRenderer.sprite = UpLeft;
            if (spriteHelp == "UpRight") spriteRenderer.sprite = UpRight;
            if (spriteHelp == "DownRight") spriteRenderer.sprite = DownRight;
            if (spriteHelp == "DownLeft") spriteRenderer.sprite = DownLeft;
        }
        position1 = new Vector2(transform.position.x, transform.position.z);

#endregion

    }

    void Direction(bool bool1, bool bool2, bool bool3, bool bool4, bool bool5, bool bool6, bool bool7, bool bool8)
    {
        animator.SetBool("isGoingUp", bool1);
        animator.SetBool("isGoingRight", bool2);
        animator.SetBool("isGoingDown", bool3);
        animator.SetBool("isGoingLeft", bool4);
        animator.SetBool("isGoingUpLeft", bool5);
        animator.SetBool("isGoingUpRight", bool6);
        animator.SetBool("isGoingDownRight", bool7);
        animator.SetBool("isGoingDownLeft", bool8);
    }
}
