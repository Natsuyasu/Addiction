using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    
    private Rigidbody2D myRigidbody2D;           // 获得刚体
    [SerializeField] private Collider2D myCollider2D;             
    
    [SerializeField] private bool isGound;      
    [SerializeField] private int StarNum;
    [SerializeField] private int CarrotNum;
    
    public Transform groundCheck;               
    public LayerMask ground;                    
    // Animator
    private Animator myAnimator;                

    
    public float moveSpeed;                       
    
    public float jumpForce;                       
    private bool isJumpPressed;                   
    public int jumpCount;                        
    [SerializeField] private int currentJumpCount; 
    private bool isJump;                          
    private bool isDrop;                          
    private bool isAlreadyJump = false;
    private bool isHitDestination = false;

    [SerializeField] private bool isHurt;                          
    //UI
    public Text StarNumUI;
    public Text CarrotNumUI;

    //time
    private float myDelayTime = 0;               
    private bool isdelayTimeOn;                 

    ////----------------------Start
    void Start()
    {
        Initialization();       
    }

    
    void Initialization()
    {
        myRigidbody2D = this.GetComponent<Rigidbody2D>();        
        myCollider2D = this.GetComponent<BoxCollider2D>();          
        myAnimator = this.GetComponent<Animator>();              
        EmptyPosFun();                                           
    }
    
    void EmptyPosFun()
    {
        groundCheck.localPosition = new Vector3(-myCollider2D.offset.x, -1.0f, 0.0f);       //x为碰撞体的-x，y为-1.0
    }

    ////-------------------------Update
    void Update()
    {
        DelayTimeFun();         
        MoveAllFunction();      // Movement
        AnimatorAllFunction();  // Animation
        if (isHitDestination)   
        {
            if (Input.GetKeyDown(KeyCode.E))    
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);     
            }
        }
    }

    void DelayTimeFun()
    {
        if (isdelayTimeOn)    
        {
            myDelayTime += Time.deltaTime;       
            if (isHurt)     
            {
                if (myDelayTime >= 0.3)         
                {
                    isHurt = false;            
                    isdelayTimeOn = false;     
                    myDelayTime = 0;            
                }
            }
        }
    }

    
    void MoveAllFunction()
    {
        isGound = Physics2D.OverlapBox(groundCheck.position, new Vector2(myCollider2D.bounds.size.x, 0.3f), 0.0f, ground);      
        if (!isHurt)        
        {
            GoundMoveFun();      
            SkipDropFun();       
        }
    }

    
    void GoundMoveFun()    //chara movement
    {
        
        float horizontalMove = Input.GetAxisRaw("Horizontal");      
        myRigidbody2D.velocity = new Vector2(horizontalMove * moveSpeed, myRigidbody2D.velocity.y);    
                                                                                                       
        if (horizontalMove != 0)    
        {
            this.transform.localScale = new Vector3(horizontalMove, 1, 1);      
        }
    }

    
    void SkipDropFun()
    {

        
        if (Input.GetButtonDown("Jump"))
        {
            if (currentJumpCount > 0)          
            {
                isJumpPressed = true;
            }
        }

        //jump
        if (isJumpPressed && currentJumpCount > 0)       
        {
            JumpFun();
            //isJump = true;      
            //myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpForce);      
            currentJumpCount--;        
            //isAlreadyJump = true;  
            //isJumpPressed = false;  
        }

      
        if (myRigidbody2D.velocity.y > 0 && !isGound)   
        {
            isDrop = false;
            isJump = true;
        }

        
        if (myRigidbody2D.velocity.y < 0 && !isGound)   
        {
            isDrop = true;
            isJump = false;
           
            if (!isAlreadyJump)         
            {
                currentJumpCount--;     
                isAlreadyJump = true;   
            }
        }


        
        if (isGound)
        {
            isJump = false;     
            isDrop = false;     
            isAlreadyJump = false; 

            if (!isJump)    
            {
                currentJumpCount = jumpCount;   
            }
        }
    }

    void JumpFun()
    {
        isJump = true;      //chara is jumping
        myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpForce);     
        isAlreadyJump = true;   
        isJumpPressed = false;  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collection_Star"))      
        {

            StarNum++;                                
            collision.enabled = false;
            Destroy(collision.gameObject);
            EatingStar_UIFun();
        }
        if (collision.CompareTag("Collection_Carrot"))
        {

            CarrotNum++;
            collision.enabled = false;
            Destroy(collision.gameObject);
            EatingCarrot_UIFun();
        }
        if (collision.CompareTag("BottomLine"))
        {
            StartCoroutine(DeathDelay());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("House"))
        {
            isHitDestination = true;
        }
    }

    private System.Collections.IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(1f);
    }

    
    void AnimatorAllFunction()
    {
        
        myAnimator.SetFloat("RunningPara", Mathf.Abs(myRigidbody2D.velocity.x));           

       
        
        if (isJump && !isDrop)
        {
            myAnimator.SetBool("JumpPara", true);          
            myAnimator.SetBool("DropPara", false);       
        }
       
        else if (isDrop && !isJump)
        {
            myAnimator.SetBool("DropPara", true);      
            myAnimator.SetBool("JumpPara", false);      
        }
       
        else if (!isDrop && !isJump)
        {
            myAnimator.SetBool("DropPara", false);   
            myAnimator.SetBool("JumpPara", false);       
        }
        
        if (isHurt)     // play hurt animation
        {
            myAnimator.SetBool("HurtPara", true);         
        }
        else         
        {
            myAnimator.SetBool("HurtPara", false);          
        }

    }

    

    
    private void OnCollisionEnter2D(Collision2D col)        // 2 collider touch each other
    {
        EnemyController ThisEnemy = col.gameObject.GetComponent<EnemyController>();

        if (col.gameObject.CompareTag("EnemyTag"))     
        {
            if (isDrop)      
            {
                // defeat enemy
                ThisEnemy.isDeathFun();                    
                                                            
                JumpFun();      
                currentJumpCount = jumpCount - 1;           
            }
            else           
            {
                if (transform.position.x < col.gameObject.transform.position.x)   
                {
                    myRigidbody2D.velocity = new Vector2(-5.0f, 1.0f);                  
                    isHurt = true;    
                }
                else            
                {
                    myRigidbody2D.velocity = new Vector2(5.0f, 1.0f);                  
                    isHurt = true;      
                }
                isdelayTimeOn = true;      
            }
        }
        
    }
    void EatingStar_UIFun()
    {
        StarNumUI.text = StarNum.ToString();         
    }
    void EatingCarrot_UIFun()
    {
        CarrotNumUI.text = CarrotNum.ToString();     
    }
}



