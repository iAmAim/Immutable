using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the player to allow projectile firing
/// </summary>

public class FireBullet : MonoBehaviour {

    //----------------- Variables start ----------------------------------//
    public GameObject bullet;

    // Quick reference
    private Transform myTransform;
    private Transform cameraHeadTransform;

    //the position at which the projectile will be instantiated
    private Vector3 launchPosition = new Vector3();

    // Add fire rate
    private float fireRate = 0.2f;
    private float nextFire = 0;


    //----------------- Variables end ----------------------------------//

	// Use this for initialization
	void Start () {

        myTransform = transform;
        cameraHeadTransform = myTransform.FindChild("Main Camera");
      
	}
	
	// Update is called once per frame
	void Update () {

        // check if time is greater than nextfire
        if(Input.GetButton("Fire Weapon") && Time.time > nextFire)
        {
         

            nextFire = Time.time + fireRate;
            launchPosition = cameraHeadTransform.TransformPoint(0, 0, 5); 
             
            //create bullet projectile at launchPosition and tilt the angle  
            // so that it is horizontal using the angle  eulerAngles.x + 90

            Instantiate(bullet,launchPosition,Quaternion.Euler(cameraHeadTransform.eulerAngles.x + 90,
                                                                myTransform.eulerAngles.y, 0));
        }
	
	}
}
