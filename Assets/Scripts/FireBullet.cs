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
    private Transform cameraTransform;

    //the position at which the projectile will be instantiated
    private Vector3 launchPosition = new Vector3();

    // Add fire rate
    private float fireRate = 0.2f;
    private float nextFire = 0;

    // Added Friday.test only
    private Transform bulletTarget;

    //----------------- Variables end ----------------------------------//

    void Awake()
    {
        myTransform = transform;  
      //  cameraTransform = Camera.main.transform; //cameraObj.transform;

        
    }

	// Use this for initialization
	/*void Start () {

        myTransform = transform;
        ///cameraHeadTransform = myTransform.FindChild("Main Camera");   
        //GameObject cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        cameraHeadTransform = Camera.main.transform; //cameraObj.transform;
       
        //myTransform.Find();
	}*/

    void Start()
    {
        GameObject playerTargetObj = GameObject.FindGameObjectWithTag("rayTarget");

        bulletTarget = playerTargetObj.transform;
       // cameraXrotation = camera.returnCameraRotationX(); 
       
    } 

	
	// Update is called once per frame
	void Update () {

        Debug.DrawLine(myTransform.position, bulletTarget.position, Color.blue);

        myTransform.LookAt(bulletTarget); // looks at rayTarget object
         
        // check if time is greater than nextfire
        if(Input.GetButton("Fire Weapon") && Time.time > nextFire)
        {

            
            nextFire = Time.time + fireRate;
            launchPosition = myTransform.TransformPoint(0, 0, 0); 
             
            //create bullet projectile at launchPosition and tilt the angle  
            // so that it is horizontal using the angle  eulerAngles.x + 90

         // Instantiate(bullet, launchPosition, Quaternion.Euler(myTransform.eulerAngles.x +90, myTransform.eulerAngles.y, 0));  

            
            
           // Instantiate(bullet, launchPosition, Quaternion.Euler(myTransform.eulerAngles.x + 90 + cameraXrotation + addAngle, bulletTarget.eulerAngles.y, 0));

            Instantiate(bullet, launchPosition, Quaternion.Euler(myTransform.eulerAngles.x + 90, myTransform.eulerAngles.y, 0));

          
        }


	
	}
}
