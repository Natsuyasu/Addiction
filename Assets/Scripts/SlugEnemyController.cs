using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugEnemyController : EnemyController
{

    //variable
    private Rigidbody2D myRigidbody2D;      
    private Transform left;            
    private Transform right;           
    //speed
    public float moveSpeed;                 
    
    private float leftRange;                
    private float rightRange;               
    
    public bool isOriginal_FaceToLeft;       // init face direction
    private float original_FaceDirection;       // -1 is left

    ////--------------------------Start
    void Start()
    {
        base.Start();
        Initialization();       
        MoveRangeFun();         
    }

    
    void Initialization()
    {
        myRigidbody2D = this.GetComponent<Rigidbody2D>();       
        left = this.transform.GetChild(0);                 // fetch 1st subobject
        right = this.transform.GetChild(1);                
    }

    
    void MoveRangeFun()
    {
        leftRange = left.position.x;       // absolute x value
        rightRange = right.position.x;     
        Destroy(left.gameObject);          
        Destroy(right.gameObject);         
    }


    ////-------------Update
    void Update()
    {
        FaceDirectionFunction();        
        MoveMentFunction();             
    }

    
    void MoveMentFunction()
    {
        myRigidbody2D.velocity = new Vector2(this.transform.localScale.x * moveSpeed * original_FaceDirection, myRigidbody2D.velocity.y);        // chara moves
    }

    //chara facing
    void FaceDirectionFunction()
    {

        if (isOriginal_FaceToLeft)          
        {
            if (this.transform.position.x <= leftRange)      
            {
                this.transform.localScale = new Vector3(-1, 1, 1);      
            }
            else if (this.transform.position.x >= rightRange)   
            {
                this.transform.localScale = new Vector3(1, 1, 1);      
            }
            original_FaceDirection = -1;            // left
        }
        else                                
        {
            if (this.transform.position.x <= leftRange)      
            {
                this.transform.localScale = new Vector3(1, 1, 1);     
            }
            else if (this.transform.position.x >= rightRange)   
            {
                this.transform.localScale = new Vector3(-1, 1, 1);      
            }
            original_FaceDirection = 1;            
        }
    }
}