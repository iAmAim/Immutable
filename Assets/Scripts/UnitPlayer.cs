using UnityEngine;
using System.Collections;

public class UnitPlayer : Unit {

	// Use this for initialization
	public override void Start () {

        base.Start();
	}
	
	// Update is called once per frame
    public override void Update()
    { 
        //rotate
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
        if (Input.GetKey(KeyCode.Space) && control.isGrounded )
        {
            jumping = true;
        }

        running = Input.GetKey(KeyCode.LeftShift);

        base.Update();
	}
}
