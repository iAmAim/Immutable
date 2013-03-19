using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the player to allow antibody bullet-firing
/// </summary>

public class FireBullet : MonoBehaviour {

    //----------------- Variables start ----------------------------------//
    public GameObject bullet1;
    public GameObject bullet2;
    private GameObject bulletLoaded;

    // Quick reference
    private Transform myTransform;

    //the position at which the projectile will be instantiated
    private Vector3 launchPosition = new Vector3();

    // Add fire rate: higher = faster
    private float fireRate = 0.15f;
    private float nextFire = 0;

    // Added Friday.test only
    private Transform bulletTarget;
    GameObject targetObject; 

    //----------------- Variables end ----------------------------------//

    void Awake()
    {
        myTransform = transform;  
      //  cameraTransform = Camera.main.transform; //cameraObj.transform;
        targetObject = GameObject.FindGameObjectWithTag("rayTarget");
        bulletLoaded = bullet1;
        
    }

	// Use this for initialization
    void Start()
    {
        
        bulletTarget = targetObject.transform;
       // cameraXrotation = camera.returnCameraRotationX(); 
       
    } 

	
	// Update is called once per frame
	void Update () {

        Debug.DrawLine(myTransform.position, bulletTarget.position, Color.blue);

        myTransform.LookAt(bulletTarget); // looks at rayTarget object


   

        if (Input.GetKeyDown("1"))
        {
            bulletLoaded = bullet1;
        }

        if (Input.GetKeyDown("2"))
        {
            bulletLoaded = bullet2;
        }

         
        // check if time is greater than nextfire
        if(Input.GetButton("Fire Weapon") && Time.time > nextFire)
        {

            
            nextFire = Time.time + fireRate;
            launchPosition = myTransform.TransformPoint(0, 0, 0); 
             
            //create bullet projectile at launchPosition and tilt the angle  
            // so that it is horizontal using the angle  eulerAngles.x + 90

            Instantiate(bulletLoaded, launchPosition, Quaternion.Euler(myTransform.eulerAngles.x + 90, myTransform.eulerAngles.y, 0));
            Instantiate(bulletLoaded, launchPosition + new Vector3(0,.2f,0), Quaternion.Euler(myTransform.eulerAngles.x + 90, myTransform.eulerAngles.y, 0));
            //Instantiate(bulletLoaded, launchPosition, Quaternion.Euler(myTransform.eulerAngles.x + 90, myTransform.eulerAngles.y, 0));
          
        }
	
	}
}
