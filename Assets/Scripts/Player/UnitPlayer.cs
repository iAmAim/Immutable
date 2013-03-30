using UnityEngine;
using System.Collections;

public class UnitPlayer : Unit {

	// Use this for initialization

    public  static float health = 100f;
    public float turnSpeed = 10f;
    public override void Start () {

        base.Start();
	}
	
	// Update is called once per frame
    public override void Update()
    { 
        //handle rotation on Y Axis

        if (Screen.lockCursor == true)
        {
            transform.Rotate(0f, Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime, 0f); //turnSpeed is an absolute rotation



            //movement
            // create a new vector for move which takes input values
            move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            //normalize the vector first so that it moves at a consistent speed in all directions
            move.Normalize();

            //before we call update, convert move to the direction our player is facing by using transformDirection
            //takes the local movement along x,y,z into a global movement
            move = transform.TransformDirection(move);

            //check if player wants to jump
            if (Input.GetKey(KeyCode.Space) && control.isGrounded)
            {
                jumping = true;
            }

            running = Input.GetKey(KeyCode.LeftShift);

            base.Update();
            animatePlayer();
        }
        

	}

   public static void takeDamage()
    {
        
        health--;
       
    }


   public void eatBacteria()
   {
       //create an interface and and attach it to player only if
       // the player is character2( the leukocyte(neutrophil)
       
   }


   public void animatePlayer()
   {

       if (transform.tag == "character2")
       {
           if (Input.GetKey("w"))
           {

               transform.animation.CrossFade("forward", 1.5f);
           }
           else if (Input.GetKey("a"))
           {

               transform.animation.CrossFade("left", 1.5f);
           }
           else if (Input.GetKey("s"))
           {

               transform.animation.CrossFade("right", 1.5f);
           }
           else if (Input.GetKey("d"))
           {

               transform.animation.CrossFade("back", 1.5f);
           }
           

               transform.animation.CrossFade("idle", 1.5f);
           
       }
       else
       {
           transform.animation.CrossFade("Take 001", 1.5f);
       }
   }

    // test
     void OnTriggerEnter(Collider c)
     {
         if (c.gameObject.tag == "Ground")
         {
             Debug.Log("I hit the ground!!!!");

         }

         if (c.gameObject.tag == "level1")
         {
             GameManager.level = 1;
             transform.position = GameObject.FindGameObjectWithTag("Spawnpoint_level1").transform.position;

             GameManager.startTime = Time.time;
         
             }


     } 
}
