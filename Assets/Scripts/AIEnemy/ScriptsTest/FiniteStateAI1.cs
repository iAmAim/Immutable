///<summary> 
////// This class demonstrates the behaviour of spirochetes bacteria
////// 
///</summary>

using UnityEngine;
using System.Collections;

public class FiniteStateAI1 : AiBacteria {

    Transform thisTransform;
    float distanceToPlayer;
    float chaseDistance = 15;
    float rotationspeed = 15;
    public State state;
    public float attackrange = 7;
    Vector3 directionheading;

     public GameObject Target;
     public GameObject char1;
     public GameObject char2;


    public enum State
    {
        ChasePlayer,
        RandomWalk
    }

    public override void Awake()
    {
        base.Awake();
        distanceToPlayer = 1000;
    
    }


    public override void Start()
    {
        base.Start();
        thisTransform = transform;
        state = State.RandomWalk;
        StartCoroutine(StateMachine());
        char1 = GameObject.FindGameObjectWithTag("Player");
        char2 = GameObject.FindGameObjectWithTag("character2");
        if (char1 == null)
        {
            Target = char2;
        }
        else
            Target = null;
     
    }

    IEnumerator StateMachine()
    {
        // Execute the current coroutine (state)
        while (true)
            yield return StartCoroutine(state.ToString());

    }


  public override IEnumerator RandomWalk()
  {
  
     // print("random walking...");
     //////yield return null;

      /* Now we enter in the Execute part of a state
       which will be usually inside a while - yield block */

      bool playernotinrange = true;
      //distance = Vector3.Distance(target.position, thisTransform.position);

      while (playernotinrange)
      {
           
        NewHeadingRoutine();
        //  distance = Vector3.Distance(target.position, thisTransform.position);

        if (char2 != null) 
       {
            if (distanceToPlayer < chaseDistance)
            {
                playernotinrange = false;
                randomWalk = false;
          }
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

      char2 = GameObject.FindGameObjectWithTag("character2");
     
      // if target is active
     //if (Target.activeInHierarchy)
     // {
      if (char2 != null)
      {
          distanceToPlayer = Vector3.Distance(Target.transform.position, thisTransform.position);
      }
      else
          distanceToPlayer = 1000f; 
     // }


  }

  IEnumerator ChasePlayer()
  {
      print("Chasing player for real..");
      yield return null;
      print("Chasing player for real real..");

      //Execute chase
      bool chasing = true;
      running = true;

      while (chasing)
      {
          Chase();

          if (distanceToPlayer > chaseDistance || !Target.activeInHierarchy)
          {
              chasing = false;
              running = false;
          }

          yield return null;
      }

      //Exit chase
      print("Going back to random walking..");
      state = State.RandomWalk;


  }

  public void Chase()
  {
      directionheading = Target.transform.position - transform.position;
      directionheading.y = 0;

      /*if (direction.magnitude < 0.1)
     return;*/
      // Rotate towards the target
      thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, Quaternion.LookRotation(directionheading), rotationspeed * Time.deltaTime);
      thisTransform.eulerAngles = new Vector3(0, thisTransform.eulerAngles.y, 0);


     // move = thisTransform.TransformDirection(Vector3.forward);
      // myTransform.position = new Vector3(0,10,0);
     // control.Move(move * walkSpeed * Time.deltaTime);


      ////// test distance..
      float ourdistance = Vector3.Distance(Target.transform.position, thisTransform.position);

      // attack player if distance is close enough
      if (ourdistance <= attackrange)
      {
          //attack(); I used jumping for now.
          //jumping = true;
         // GameHud.playerhealth--;
          UnitPlayer.takeDamage();
          //Debug.Log("I bite you. :p");
          Debug.Log("Distance :" + distanceToPlayer);
         
      }
  }

  
}

