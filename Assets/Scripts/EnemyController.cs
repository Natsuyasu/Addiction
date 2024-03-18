using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Animator myAnimator;          

    ////---------------------Start
    protected void Start()                    
    {
        myAnimator = this.GetComponent<Animator>();      // Animator
    }

    //PUBLIC FUNCTION--------------------
    public void isDeathFun()     
    {
        myAnimator.SetTrigger("Death");        // call event when death ends
    }
   
    //Destroy
    void DestroyFun()
    {
        Destroy(this.gameObject);     
    }


}