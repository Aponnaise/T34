using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerBahav : MonoBehaviour {

    public float superSpeed = 4f;
    public float speed = 20;
    public Sprite Up;        
    public Sprite Right;    
    public Sprite Down;      
    public Sprite Left;      
    public Sprite UpLeft;    
    public Sprite UpRight;   
    public Sprite DownRight; 
    public Sprite DownLeft;  
    Vector2 position1 = new Vector2(0,0);  
    Vector2 position2; 
    float directionX; 
    float directionY;
    string spriteHelp;
    bool isSuperSpeed = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        Vector3 moveVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));
        if(!isSuperSpeed) {
            isSuperSpeed = CrossPlatformInputManager.GetButtonDown("BuSuperSpeed");
            GetComponent<Rigidbody>().velocity = moveVec.normalized * speed; 
        } else {
            GetComponent<Rigidbody>().velocity = moveVec.normalized * speed * superSpeed; 
            if(CrossPlatformInputManager.GetButtonUp("BuSuperSpeed")) isSuperSpeed = false;
        }
        
        position2 = new Vector3(transform.position.x, transform.position.z);
        directionX = position2.x - position1.x; 
        directionY = position2.y - position1.y;
        animator.enabled = true;
       
        if(directionX < 0) {  //LEFT
            if(directionY < 0) {  //DOWN
                if(directionY <= directionX) {  //DOWN
                    Direction(false, false, -directionY / -directionX >= 3.3, false, false, false, false, -directionY / -directionX < 3.3);
                    if(-directionY / -directionX >= 2.5) spriteHelp = "Down";
                    if(-directionY / -directionX < 2.5) spriteHelp = "DownLeft";
                } else {  //LEFT
                    Direction(false, false, false, -directionY / -directionX < 0.33, false, false, false, -directionY / -directionX >= 0.33);
                    if(-directionY / -directionX < 0.4) spriteHelp = "Left";
                    if(-directionY / -directionX >= 0.4) spriteHelp = "DownLeft";
                }
            } else {  //UP
                if(directionY >= -directionX) {  //UP
                    Direction(directionY / -directionX >= 3.3, false, false, false, directionY / -directionX < 3.3, false, false, false);
                    if(directionY / -directionX >= 2.5) spriteHelp = "Up";
                    if(directionY / -directionX < 2.5) spriteHelp = "UpLeft";
                } else {  //LEFT
                    Direction(false, false, false, directionY / -directionX < 0.33, directionY / -directionX >= 0.33, false, false, false);
                    if(directionY / -directionX < 0.4) spriteHelp = "Left";
                    if(directionY / -directionX >= 0.4) spriteHelp = "UpLeft";
                }
            } 
        } else if(directionX > 0) {  //RIGHT
            if(directionY < 0) {  //DOWN
                if(-directionY < directionX) {  //RIGHT
                    Direction(false, -directionY / directionX < 0.33, false, false, false, false, -directionY / directionX >= 0.33, false);
                    if(-directionY / directionX < 0.4) spriteHelp = "Right";
                    if(-directionY / directionX >= 0.4) spriteHelp = "DownRight";
                } else {  //DOWN
                    Direction(false, false, -directionY / directionX >= 3.3, false, false, false, -directionY / directionX < 3.3, false);
                    if(-directionY / directionX >= 2.5) spriteHelp = "Down";
                    if(-directionY / directionX < 2.5) spriteHelp = "DownRight";
                }
            } else {  //UP
                if(directionY >= directionX) {  //UP
                    Direction(directionY / directionX >= 3.3, false, false, false, false, directionY / directionX < 3.3, false, false);
                    if(directionY / directionX >= 2.5) spriteHelp = "Up";
                    if(directionY / directionX < 2.5) spriteHelp = "UpRight";
                } else {  //RIGHT
                    Direction(false, directionY / directionX < 0.33, false, false, false, directionY / directionX >= 0.33, false, false);
                    if(directionY / directionX < 0.4) spriteHelp = "Right";
                    if(directionY / directionX >= 0.4) spriteHelp = "UpRight";
                }
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
}
