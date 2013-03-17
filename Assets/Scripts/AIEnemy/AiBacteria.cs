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
    

   public virtual void Awake()
    {
        myTransform = transform; 
        timeBeforeDirectionChange = 1f; 
        // Set random initial rotation
        walkSpeed = 1f;
        splitTime = 100f;
        health = 2;
        splitting = true;
        randomWalk = true;

      
    }

    public override void Start()
    {
        base.Start();
       heading = Random.Range(0, 360);
       myTransform.eulerAngles = new Vector3(0, heading, 0);
        StartCoroutine(RandomWalk());
        StartCoroutine(split());
       

    }

    public override void Update()
    {
        base.Update();
        myTransform.eulerAngles = Vector3.Slerp(myTransform.eulerAngles, targetRotation, Time.deltaTime * timeBeforeDirectionChange);
       move = myTransform.TransformDirection(Vector3.forward);

       //animate does not work
      // myTransform.animation.CrossFade("walk", 1.5f);

       Debug.DrawRay(myTransform.position, myTransform.forward , Color.red);

      
       
    }

    public virtual void takeDamage()
    {
        health--;
        //StopCoroutine("split");

        if (health < 1)
        {
            Destroy(myTransform.gameObject);
            GameHud.gamescore += 500;
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
        }
        
    }

}