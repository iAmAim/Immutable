using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class Unit : MonoBehaviour
{
	protected CharacterController control;
    protected Vector3 move = Vector3.zero;
    public float walkSpeed = 2.5f; 
    public float runSpeed = 4f;
    public float turnSpeed = 90f;
    public float jumpSpeed = 5f;

    public bool jumping;
    public bool  running;   

    //Add gravity to char
    protected Vector3 gravity = Vector3.zero;
	// Use this for initialization
	public virtual void Start ()
	{

        control = GetComponent<CharacterController>();

        //check if CharacterController is available
		if(!control)
		{
			Debug.LogError("Unit.Start() " + name + "has no charactercontroller attached");
			enabled = false; //disable the script
		}
		
	}
	
	// Update is called once per frame
	public virtual void Update ()
	{
        checkPlayerAction(); 
        // add gravity to move Vector
        move += gravity; 

        // Move function is used instead of SimpleMove to enable our character to jump
      control.Move(move * walkSpeed * Time.deltaTime); 



	}

    public void checkPlayerAction()
    {
        if (running)
        {
            move *= runSpeed;
        }
        else
        {
            move *= walkSpeed;
        }

        //check if character is off the ground then apply gravity
        if (!control.isGrounded)
        {
            gravity += Physics.gravity * Time.deltaTime;
        }
        else
        {
            gravity = Vector3.zero;

            if (jumping)
            {
                gravity.y = jumpSpeed;
                jumping = false;
            }
        }
    }
     
}//end
