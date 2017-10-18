using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    Vector3 target;
    NavMeshAgent agent;
    Vector2 position1 = new Vector2(0,0), position2;
    float directionX, directionY;
    Animator animator;
    SpriteRenderer spriteRenderer;
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

    private int ClassID = 0;


    private void Awake()
    {
        ClassID = Random.Range(0, 4);
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();   
    }

    public void MoveToTarget (Vector3 position, GameObject targetObject) {
        if (targetObject.tag == "Spawner")
        {
            isFinalTarget = true;
            targetObject = targetObject.transform.GetChild(Random.Range(0, 3)).gameObject;
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
        
        target = targetObject.transform.position;
        agent.destination = targetObject.transform.position;
        StartCoroutine(setTarget());
	}

    IEnumerator setTarget()
    {
        yield return new WaitForSeconds(1);
        agent.destination = target;
    }

    IEnumerator waitAtStand()
    {
        yield return new WaitForSeconds(Random.Range(5f,15f));
        StartCoroutine(setTarget());
    }

    private void FixedUpdate()
    {
        if (new Vector2(transform.position.x - target.x, transform.position.z - target.z).magnitude < destinationRange && !isFinalTarget || new Vector2(transform.position.x - target.x, transform.position.z - target.z).magnitude < safetyRange && isFinalTarget)
        {
            if (isFinalTarget)
            {
                GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawnerManager>().SpawnEnemy();
                Destroy(gameObject);
            } else
            {
                target = GameObject.FindGameObjectWithTag("EnemySpawner").transform.GetChild(0).GetChild(Random.Range(0, GameObject.FindGameObjectWithTag("EnemySpawner").transform.GetChild(0).childCount - 1)).GetChild(Random.Range(0,2)).position;
                isFinalTarget = true;
                StartCoroutine(waitAtStand());
            }
            
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
