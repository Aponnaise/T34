using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerBehav : MonoBehaviour {

    public static float superSpeed = 4f;
    public static float speed = 12f;
    public Sprite Up;
    public Sprite Right;
    public Sprite Down;
    public Sprite Left;
    public Sprite UpLeft;
    public Sprite UpRight;
    public Sprite DownRight;
    public Sprite DownLeft;
    private Vector2 position1 = new Vector2(0,0);
    private Vector2 position2;
    public float directionX;
    public float directionY;
    private string spriteHelp;
    public static bool isSuperSpeed = false;
    private Vector3 leaveDirection;

    Vector3 startingPosition = new Vector3(0, 5.5f, -72);

    bool isInGame = true;
    bool isInSpawnWalk = false;
    public bool leaveWalk = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        startingPosition = GameObject.FindGameObjectWithTag("LoadingZoneManager").transform.GetChild((int) LoadingZoneManager.loadingZoneId).position;
        startingPosition.y = 5.5f;
        transform.position = startingPosition;
        StartCoroutine(spawnWalk());
    }

    IEnumerator spawnWalk()
    {
        isInSpawnWalk = true;
        yield return new WaitForSeconds(1f);
        isInSpawnWalk = false;
    }

    private void FixedUpdate() {

#region Movement
        Vector3 moveVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));
        if (isInSpawnWalk)
        {
            moveVec = DirectionToVector(GameObject.FindGameObjectWithTag("LoadingZoneManager").transform.GetChild((int)LoadingZoneManager.loadingZoneId).GetComponent<LoadingZoneValues>().direction);
        }
        if (leaveWalk)
        {
            moveVec = leaveDirection;
        }
        if (!isSuperSpeed && !Input.GetKey(KeyCode.L)) {
            isSuperSpeed = CrossPlatformInputManager.GetButtonDown("BuSuperSpeed");
            GetComponent<Rigidbody>().velocity = moveVec.normalized * speed; 
        } else {
            GetComponent<Rigidbody>().velocity = moveVec.normalized * speed * superSpeed; 
            if(CrossPlatformInputManager.GetButtonUp("BuSuperSpeed")) isSuperSpeed = false;
        }
        #endregion

#region Animation
        position2 = new Vector3(transform.position.x, transform.position.z);
        directionX = position2.x - position1.x; 
        directionY = position2.y - position1.y;
        animator.enabled = true;

        if (directionX < 0) {  //LEFT
            if (directionY < 0) {  //DOWN
                if (directionY <= directionX) {  //DOWN
                    Direction(false, false, -directionY / -directionX >= 2.5, false, false, false, false, -directionY / -directionX < 2.5);
                    if (-directionY / -directionX >= 2.5) spriteHelp = "Down";
                    if (-directionY / -directionX < 2.5) spriteHelp = "DownLeft";
                } else {  //LEFT
                    Direction(false, false, false, -directionY / -directionX < 0.4, false, false, false, -directionY / -directionX >= 0.4);
                    if (-directionY / -directionX < 0.4) spriteHelp = "Left";
                    if (-directionY / -directionX >= 0.4) spriteHelp = "DownLeft";
                }
            } else {  //UP
                if (directionY >= -directionX) {  //UP
                    Direction(directionY / -directionX >= 2.5, false, false, false, directionY / -directionX < 2.5, false, false, false);
                    if (directionY / -directionX >= 2.5) spriteHelp = "Up";
                    if (directionY / -directionX < 2.5) spriteHelp = "UpLeft";
                } else {  //LEFT
                    Direction(false, false, false, directionY / -directionX < 0.4, directionY / -directionX >= 0.4, false, false, false);
                    if (directionY / -directionX < 0.4) spriteHelp = "Left";
                    if (directionY / -directionX >= 0.4) spriteHelp = "UpLeft";
                }
            }
        } else if (directionX > 0) {  //RIGHT
            if (directionY < 0) {  //DOWN
                if (-directionY < directionX) {  //RIGHT
                    Direction(false, -directionY / directionX < 0.4, false, false, false, false, -directionY / directionX >= 0.4, false);
                    if (-directionY / directionX < 0.4) spriteHelp = "Right";
                    if (-directionY / directionX >= 0.4) spriteHelp = "DownRight";
                } else {  //DOWN
                    Direction(false, false, -directionY / directionX >= 2.5, false, false, false, -directionY / directionX < 2.5, false);
                    if (-directionY / directionX >= 2.5) spriteHelp = "Down";
                    if (-directionY / directionX < 2.5) spriteHelp = "DownRight";
                }
            } else {  //UP
                if (directionY >= directionX) {  //UP
                    Direction(directionY / directionX >= 2.5, false, false, false, false, directionY / directionX < 2.5, false, false);
                    if (directionY / directionX >= 2.5) spriteHelp = "Up";
                    if (directionY / directionX < 2.5) spriteHelp = "UpRight";
                } else {  //RIGHT
                    Direction(false, directionY / directionX < 0.4, false, false, false, directionY / directionX >= 0.4, false, false);
                    if (directionY / directionX < 0.4) spriteHelp = "Right";
                    if (directionY / directionX >= 0.4) spriteHelp = "UpRight";
                }
            }
        } else if (directionX == 0 && directionY != 0)
        {
            if (directionY > 0)
            {
                Direction(directionY / directionX >= 2.5, false, false, false, false, directionY / directionX < 2.5, false, false);
                spriteHelp = "Up";
            } else
            {
                Direction(false, false, -directionY / directionX >= 2.5, false, false, false, -directionY / directionX < 2.5, false);
                spriteHelp = "Down";
            }

        } else {  //NO MOVEMENT
            Direction(false, false, false, false, false, false, false, false);
            animator.enabled = false;
            if(spriteHelp == "Up") spriteRenderer.sprite = Up;
            if(spriteHelp == "Right") spriteRenderer.sprite = Right;
            if(spriteHelp == "Down") spriteRenderer.sprite = Down;
            if(spriteHelp == "Left") spriteRenderer.sprite = Left;
            if(spriteHelp == "UpLeft") spriteRenderer.sprite = UpLeft;
            if(spriteHelp == "UpRight") spriteRenderer.sprite = UpRight;
            if(spriteHelp == "DownRight") spriteRenderer.sprite = DownRight;
            if(spriteHelp == "DownLeft") spriteRenderer.sprite = DownLeft;
        }
        position1 = new Vector2(transform.position.x, transform.position.z);
        #endregion

#region PositionEvents

        RaycastHit hit;
        if (Physics.Raycast(transform.position,Vector3.down,out hit, 20) && isInGame)
        {
            if (hit.transform.gameObject.tag == "Gleis")
            {
                Debug.Log("Dies");
                //Die
                StartCoroutine(Die());
            }

            if (hit.transform.parent.tag == "LoadingZoneManager" && !leaveWalk)
            {
                if (!isInSpawnWalk && Vector3.Dot(DirectionToVector(hit.transform.gameObject.GetComponent<LoadingZoneValues>().direction), moveVec) < 0)
                {
                    leaveDirection = -DirectionToVector(hit.transform.gameObject.GetComponent<LoadingZoneValues>().direction);
                    LoadingZoneManager.LoadScene(hit.transform.gameObject.GetComponent<LoadingZoneValues>().targetId, hit.transform.gameObject.GetComponent<LoadingZoneValues>().targetScene);
                }
            }
        }
        #endregion
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.transform.gameObject.tag == "Enemy" && (isSuperSpeed || Input.GetKey(KeyCode.L))) {
            Debug.Log("Dies");
            //Die
            StartCoroutine(Die());
        }
    }

    void Direction(bool bool1, bool bool2, bool bool3, bool bool4, bool bool5, bool bool6, bool bool7, bool bool8) {
        animator.SetBool("isGoingUp", bool1);
        animator.SetBool("isGoingRight", bool2);
        animator.SetBool("isGoingDown", bool3);
        animator.SetBool("isGoingLeft", bool4);
        animator.SetBool("isGoingUpLeft", bool5);
        animator.SetBool("isGoingUpRight", bool6);
        animator.SetBool("isGoingDownRight", bool7);
        animator.SetBool("isGoingDownLeft", bool8);
    }

    private IEnumerator Die()
    {
        isInGame = false; 
        while (transform.localScale.x>0)
        {
            transform.localScale = transform.localScale - new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            yield return null;
        }

            //INSERT DEATH HERE

        transform.localScale = new Vector3(1,1,1);
        transform.position = startingPosition;
        isInGame = true;
    }

    private Vector3 DirectionToVector(int directionInt)
    {
        switch (directionInt)
        {
            case 0:
                return new Vector3(0, 0, 1);
            case 1:
                return new Vector3(1, 0, 1);
            case 2:
                return new Vector3(1, 0, 0);
            case 3:
                return new Vector3(1, 0, -1);
            case 4:
                return new Vector3(0, 0, -1);
            case 5:
                return new Vector3(-1, 0, -1);
            case 6:
                return new Vector3(-1, 0, 0);
            default:
                return new Vector3(-1, 0, 1);
        }
    }
}