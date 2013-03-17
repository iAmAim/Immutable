//File: CoMiner.cs

using UnityEngine;
using System.Collections;

public class FiniteStateAI1 : AiBacteria {

    Transform thisTransform;
    float distance;
    Transform target;
    float lookAtDistance = 15;
    float movespeed = 5;
    float rotationspeed = 5;
    public State state;


    public enum State
    {
        NewHeading,
        ChasePlayer,
        RandomWalk
    }

    public override void Awake()
    {
        base.Awake();
        GameObject playerObj = GameObject.FindGameObjectWithTag("character2");
        target = playerObj.transform;
        state = State.RandomWalk;
        StartCoroutine(FSM());

        
    }


    public override void Start()
    {
        base.Start();
        thisTransform = transform;

    }

    IEnumerator FSM()
    {
        // Execute the current coroutine (state)
        while (true)
            yield return StartCoroutine(state.ToString());

    }


  public override IEnumerator RandomWalk()
  {
      
      print("random walking...");
     yield return null;

      /* Now we enter in the Execute part of a state
      which will be usually inside a while - yield block */

      bool playernotinrange = true;
      //distance = Vector3.Distance(target.position, thisTransform.position);

      while (playernotinrange)
      {
          NewHeadingRoutine();
        //  distance = Vector3.Distance(target.position, thisTransform.position);

          if (distance < lookAtDistance)
          {
              playernotinrange = false;
          }


          yield return new WaitForSeconds(timeBeforeDirectionChange);
      }
          /* And finally do something before leaving the state */

          print("Chasing player...");
          state = State.ChasePlayer;
      
  }

  public override void Update()
  {

      base.Update();

     distance = Vector3.Distance(target.position, thisTransform.position);



    if (distance < lookAtDistance)
     {
         Debug.Log("asdasdasdasdasdasdas!!");
         randomWalk = false;
         //StopCoroutine("NewHeading");
         Chase();

     }
     else
     {
         base.Update();
       // StartCoroutine(NewHeading());
         randomWalk = true;
     } 


  }

  IEnumerator ChasePlayer()
  {
      print("Chasing player for real..");
      yield return null;
      print("Chasing player for real..");

      //Execute chase
      bool chasing = true;
      
      while (chasing)
      {
          Chase();

          if (distance > lookAtDistance)
          {
              chasing = false;
          }

          yield return new WaitForSeconds(1);
      }

      //Exit chase
      print("Going back to random walking..");
      state = State.RandomWalk;


  }
  Vector3 direction;
  public void Chase()
  {
      direction = target.position - transform.position;
      direction.y = 0;

     // if (direction.magnitude < 0.1)
     //return;
      // Rotate towards the target
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationspeed * Time.deltaTime);
      transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);


      move = thisTransform.TransformDirection(Vector3.forward);
      // myTransform.position = new Vector3(0,10,0);
      control.Move(move * walkSpeed * Time.deltaTime);
  }

  
}

