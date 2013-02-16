using UnityEngine;
using System.Collections;

public class MoveTowardsTargetTestAI : MonoBehaviour {

    public Transform target;
    public int movespeed;
    public int rotationspeed;

    private Transform myTransform;

    void Awake()
    {
        /*Cache transform to myTransform variable.*/
        myTransform = transform;
    }

	// Use this for initialization
	void Start () {

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        target = playerObj.transform;
	}
	
	// Update is called once per frame
	void Update () {

        // Debug statement 
        Debug.DrawLine(target.position, myTransform.position, Color.red);

        //Look at target (playerObj)
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), 
            rotationspeed * Time.deltaTime);
        // move towards target (playerObj)
        myTransform.position += myTransform.forward * movespeed * Time.deltaTime;


	}
}
