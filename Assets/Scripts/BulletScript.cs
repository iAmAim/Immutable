using UnityEngine;
using System.Collections;

/// <summary>
/// This script is to be attached to bullet projectile and defines bullet behaviour
/// </summary>

public class BulletScript : MonoBehaviour {

    //----------------- Variables start ----------------------------------//

    // A quick reference
    private Transform myTransform;

    //projectile flight speed
    private float projectileSpeed = 30;

    //prevent projectile from causing further harm once it hits something
    private bool alreadyhitsomething = false;

    /* Ray projected in front of the projectile
    to see if it will recognize a collider */
    private RaycastHit hit;

    // The range of the ray
    private float range = 1f;  //.2f  

    // lifespan of the projectile in seconds

    private float bulletLife = 4;

    //----------------- Variables end ----------------------------------//

	// Use this for initialization
	void Start () {
        //assign to myTransform of whatever the script is attached to (attached to Bullet)
        myTransform = transform;

        // As soon as projectile is created, start countdown then destroy game object.
        StartCoroutine(destroyBullet());
	
	}
	
	// Update is called once per frame
	void Update () {

        //Translate the projectile in the up direction cause bullet travels on  Y axis
        myTransform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);

        // if the ray hits something then execute this code.

        if (Physics.Raycast(myTransform.position, myTransform.up, out hit, range) &&
            alreadyhitsomething == false)
        {
            //if collider has tag 'Ground'
            if (hit.transform.tag == "Ground")
            {
                alreadyhitsomething = true;
                //make projectile disappear
                myTransform.renderer.enabled = false;

                //turn off light component as well ( halo will also disappear)
                myTransform.light.enabled = false;
            }
            if (hit.transform.tag == "enemy")
            {
               /* // Dynamically create a fixedjoint to stick bullets to the enemy 
                var joint = myTransform.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = hit.transform.rigidbody;
                projectileSpeed = 0;  
                */
                alreadyhitsomething = true;
                projectileSpeed = 0; 
               //hit.transform.SendMessage ("IsHit"); // send message to the enemy being hit
                Debug.Log("enemy(bacteria) is hit");
                  
                myTransform.parent = hit.transform;           
              
            }
              
         
        }
	}

    IEnumerator destroyBullet()
    {
        // Wait for the timer to count til time expires, then destroy the bullet projectile
        // make coroutine hang on until 5 seconds then it destroys the projectile
        yield return new WaitForSeconds(bulletLife);
        Destroy(myTransform.gameObject);
    }

    

  /* void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "enemy")
        {
            Debug.Log("Bullet hits enemy!!!!");
           // c.transform.parent = myTransform;
            var joint = c.gameObject.AddComponent<FixedJoint>();  

            joint.connectedBody = c.rigidbody;
        }
              
       
    } */

}
