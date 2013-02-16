using UnityEngine;
using System.Collections;

/// <summary>
/// Creates wandering behaviour for a CharacterController.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class Wander : Unit
{
    private Transform myTransform;
    public float changeDirectionInterval = 1; // number of seconds bacteria swims
    public float maxHeadingChange = 30;
    public float speed = 5;

    CharacterController controller;
    float heading;
    Vector3 targetRotation;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        myTransform = transform; 
        // Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        StartCoroutine(NewHeading());
    }

    void Update()
    {
        ///float x = myTransform.position.x;
      /// x = x + 2f;
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * changeDirectionInterval);
        var forward = transform.TransformDirection(Vector3.forward);
        controller.SimpleMove(forward * speed);
       /// Debug.DrawLine(myTransform.position, x , Color.red); 
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