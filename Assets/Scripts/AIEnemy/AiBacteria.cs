using UnityEngine;
using System.Collections;

/// <summary>
/// Creates Ai behaviour(random walk)  for a CharacterController.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class AiBacteria : Unit
{
    public float health;
    private Transform myTransform;
    public float timeBeforeDirectionChange; // number of seconds bacteria swims
    public float maxHeadingChange = 360f; 
    float heading;
    public Vector3 targetRotation;

    public GameObject bacteria;
    private Vector3 splitPosition;
    public float splitTime;
    private bool splitting;
    public bool randomWalk;
    public float deathTime;
    public bool alreadydead;

    //used for animation
    private bool isbacteria2 = false;
    

   public virtual void Awake()
    {
        myTransform = transform; 
        timeBeforeDirectionChange = 1f; 
        // Set random initial rotation
        walkSpeed = 1f;
        splitTime = 20f;
        deathTime = 30f;
        health = 2;
        splitting = true;
        randomWalk = true;
        alreadydead = false;
       

    
    }

    public override void Start()
    {
        base.Start();
        heading = Random.Range(0, 360);
        myTransform.eulerAngles = new Vector3(0, heading, 0);
        StartCoroutine(RandomWalk());
        StartCoroutine(split());
        StartCoroutine(BacteriaDeath());

        if (myTransform.tag == "enemy2")
        {
            isbacteria2 = true;
        }

    }

    public override void Update()
    {
        base.Update();

        //Rotate
        myTransform.eulerAngles = Vector3.Slerp(myTransform.eulerAngles, targetRotation, Time.deltaTime * timeBeforeDirectionChange);
       //Move
        move = myTransform.TransformDirection(Vector3.forward);

       Debug.DrawRay(myTransform.position, myTransform.forward , Color.red);
   

        //Animate enemy2 (bateria staphyloccocus)
       if (isbacteria2)
       {
           myTransform.animation.CrossFade("walk", .1f);
       }

      
    }

    public virtual void takeDamage()
    {
        health--;
        
        //StopCoroutine("split");

        if (health < 1)
        {
            alreadydead = true;
            Destroy(myTransform.gameObject);
            GameHud.gamescore += 500;
            GameHud.activeBacteriaCount-=1;
        }
    }

    /// <summary>
    /// Repeatedly calculates a new direction to move towards.
    /// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
    /// </summary>
    public virtual IEnumerator RandomWalk()
    {
        while (randomWalk)
        {
            NewHeadingRoutine();
            yield return new WaitForSeconds(timeBeforeDirectionChange);
        }
    }

    /// <summary>
    /// Calculates a new direction to move towards.
    /// </summary>
    public void NewHeadingRoutine()
    {
        var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
        var ceil = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }

    public virtual IEnumerator split()
    {
        while (splitting)
        {

            yield return new WaitForSeconds(splitTime);
            splitPosition = myTransform.TransformPoint(0, 0, -.2f);
            Instantiate(bacteria, splitPosition, Quaternion.Euler(myTransform.eulerAngles.x, myTransform.eulerAngles.y, 0));
            GameHud.activeBacteriaCount += 1;
        }
        
    }

    public virtual IEnumerator BacteriaDeath()
    {
              
            yield return new WaitForSeconds(deathTime);
            Destroy(myTransform.gameObject);
            GameHud.activeBacteriaCount -= 1;

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "gradient1")
        {
           // Destroy(myTransform.gameObject);
            walkSpeed = 3f;

        }
        else
            walkSpeed = 1f;


    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "gradient1")
        {
            // Destroy(myTransform.gameObject);
            walkSpeed = 1f;

        }
       

    } 

}