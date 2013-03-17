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
    private float projectileSpeed;

    //prevent projectile from causing further harm once it hits something
    private bool alreadyhitsomething;

    /* Ray projected in front of the projectile
    to see if it will recognize a collider */
    private RaycastHit hit;

    // The range of the ray
    private float range;  //.2f  

    // lifespan of the projectile in seconds

    private float bulletLife;
    private bool bullet1;
    private bool bullet2;


    //----------------- Variables end ----------------------------------//


    void Awake()
    {
        myTransform = transform;
        // Ignore bullet colliding with Player 
        Physics.IgnoreLayerCollision(8, 9);
        // Ignore bullet colliding with bullet 
        Physics.IgnoreLayerCollision(8, 8);
        projectileSpeed = 30;
        bulletLife = 4;
        range = 0.5f;
        alreadyhitsomething = false;
        bullet1 = false;
        bullet2 = false;
    }

	// Use this for initialization
	void Start () {
        // Check which bullet is fired
        if (myTransform.tag.Equals("bullet1"))
        {
            bullet1 = true;
        }
        else
            if (myTransform.tag.Equals("bullet2"))
         {
                bullet2 = true;
          }
          
        //assign to myTransform of whatever the script is attached to (attached to Bullet) This saves Transform component for caching 
     
        // As soon as projectile is created, start countdown then destroy game object.
        StartCoroutine(destroyBullet());  
	
	}
	
	// Update is called once per frame
	void Update () {

        //Translate the projectile in the up direction cause bullet travels on  Y axis
        myTransform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
        
        // TODO: ADd particle effects here


        // if the ray hits something then execute this code.

        if (Physics.Raycast(myTransform.position, myTransform.up, out hit, range) &&
            alreadyhitsomething == false)
        {


            if (hit.transform.tag == "enemy1" && bullet1)
            {
                /* // Dynamically create a fixedjoint to stick bullets to the enemy 
                 var joint = myTransform.gameObject.AddComponent<FixedJoint>();
                 joint.connectedBody = hit.transform.rigidbody;
                 projectileSpeed = 0;    this fails.
                 */
                alreadyhitsomething = true;
                projectileSpeed = 0;
               // myTransform.collider.enabled = false;
                myTransform.collider.isTrigger = true;
                   // myTransform.parent = hit.transform.gameObject.transform;
                myTransform.parent = hit.transform;
       
             
                // bacteria takes damage
               AiBacteria aibacteria = hit.collider.gameObject.GetComponent<AiBacteria>();
                  aibacteria.takeDamage(); // bacteria takes damage 
                   Debug.Log("enemy(bacteria) is hit");      
                
            }
            else if (hit.transform.tag == "enemy2" && bullet2)
            {
              
                alreadyhitsomething = true;
                projectileSpeed = 0;
                myTransform.collider.isTrigger = true;
                myTransform.parent = hit.transform;

                // bacteria takes damage
                AiBacteria aibacteria = hit.collider.gameObject.GetComponent<AiBacteria>();
                aibacteria.takeDamage(); // bacteria takes damage 
                Debug.Log("enemy(bacteria) is hit");

            }
        

            else
            {
                // TODO: Add particle effects here
                Destroy(myTransform.gameObject);
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
