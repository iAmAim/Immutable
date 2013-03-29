using UnityEngine;
using System.Collections;

/// <summary>
/// Creates Ai behaviour(random walk)  for a CharacterController.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class AiBacteria : Unit
{
    public float health;
    public float turnSpeed;
    private Transform myTransform;
    public float timeBeforeDirectionChange; // number of seconds bacteria swims
    public float maxHeadingChange = 180; 
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
    private bool isbacteria3 = false;
    private string gradient;
    private bool ingradient2 = false;
    

   public virtual void Awake()
    {
        myTransform = transform; 
        timeBeforeDirectionChange = 1f; 
        // Set random initial rotation
        walkSpeed = 1f;
        deathTime = 30f;
        health = 2;
        turnSpeed = 2;
        splitting = true;
        randomWalk = true;
        alreadydead = false;
        if (myTransform.tag == "enemy2")
        {
            isbacteria2 = true;
        }
        if (myTransform.tag == "enemy3")
        {
            splitTime = 10f;
            isbacteria3 = true;
        }
        else
        {
            splitTime = 15f;
        }

        if (GameManager.level == 0)
        {
            myTransform.parent = GameObject.FindGameObjectWithTag("level0_activator").transform;
        }
        else if (GameManager.level == 1)
        {
            myTransform.parent = GameObject.FindGameObjectWithTag("level1_enemyspawner").transform;
        }



    
    }

    public override void Start()
    {
        base.Start();
        heading = Random.Range(0, 360);
        myTransform.eulerAngles = new Vector3(0, heading, 0);
        StartCoroutine(RandomWalk());
        StartCoroutine(split());
        StartCoroutine(BacteriaDeath());
    }

    public override void Update()
    {
        base.Update();

        //Rotate
        myTransform.eulerAngles = Vector3.Slerp(myTransform.eulerAngles, targetRotation, Time.deltaTime * turnSpeed); 
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
            GameManager.gamescore += 500;
            DestroyBacteria();
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
            //GameManager.activeBacteriaCount += 1;
        }
        
    }

    public virtual IEnumerator BacteriaDeath()
    {
              
            yield return new WaitForSeconds(deathTime);
            DestroyBacteria();

    }

    void OnTriggerEnter(Collider c)
    {
        gradient = c.gameObject.tag;

        switch (gradient)
        {
            case "gradient1":

                //params: speed, swimtime,anglechange   
                ingradient2 = false;
                changeStats(1.5f, 1.5f, 120);
                break;

            case "gradient2":
                ingradient2 = true;
                changeStats(2.25f, 2f, 90);
                break;

            case "gradient3":
                 
                changeStats(3, 2.5f, 60);
                break;  

            default:
                changeStats(1, 1f, 180);
                break;
        }

    }

    // changes to the Bacteria statistics
    void changeStats(float speed, float swimtime, float angleChange)
    {
        walkSpeed = speed;
        timeBeforeDirectionChange = swimtime;
        maxHeadingChange = angleChange;
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "gradient1")
        {
            if (ingradient2)
            {
                changeStats(2.25f, 2f, 90);
            }
            else
            {
                ingradient2 = false;
                changeStats(1f, 1f, 180);
            }

        }   

    }
 

    void DestroyBacteria()
    {
        Destroy(myTransform.gameObject);
       // GameManager.activeBacteriaCount -= 1;

    
    }

    // does work!
    void OnControllerColliderHit(ControllerColliderHit c)
    {
        if (c.collider.gameObject.tag == "character2" && isbacteria3)
        {

            DestroyBacteria();
            GameManager.gamescore += 500;
        }
        
    }

    // doesnt work
   /* void OnCollisionEnter(Collider c)
    {
        if (c.gameObject.tag == "character2" && isbacteria4)
        {

            DestroyBacteria();
            GameHud.gamescore += 500;
        }
    }*/
}