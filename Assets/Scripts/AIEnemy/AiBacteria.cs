using UnityEngine;
using System.Collections;

/// <summary>
/// Creates Ai behaviour(random walk)  for a CharacterController.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class AiBacteria : Unit
{
    public float health = 3f;
    private Transform myTransform;
    public float changeDirectionInterval = 1f; // number of seconds bacteria swims
    public float maxHeadingChange = 360f; 
    float heading;
    Vector3 targetRotation;
    

    void Awake()
    {
       

        myTransform = transform; 
        // Set random initial rotation
        walkSpeed = 1f; 

      
    }

    public override void Start()
    {
        base.Start();
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);
        StartCoroutine(NewHeading());
    }
     
    public override void Update() 
    {
     
       transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * changeDirectionInterval);
       move = transform.TransformDirection(Vector3.forward);

       Debug.DrawRay(myTransform.position, myTransform.forward , Color.red);
       base.Update();
    }

    public void takeDamage()
    {
        health--;

        if (health < 1)
        {
            Destroy(myTransform.gameObject);
            GameScore.gamescore += 100;
        }
    }

    /// <summary>
    /// Repeatedly calculates a new direction to move towards.
    /// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
    /// </summary>
    IEnumerator NewHeading()
    {
        while (true)
        {
            NewHeadingRoutine();
            yield return new WaitForSeconds(changeDirectionInterval);
        }
    }

    /// <summary>
    /// Calculates a new direction to move towards.
    /// </summary>
    void NewHeadingRoutine()
    {
        var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
        var ceil = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }
}