using UnityEngine;
using System.Collections;

/// <summary>
/// This script is to be attached to bullet projectile and defines bullet behaviour
/// Particle explosion is also added here - bullet_explosion
///  docs.unity3d.com/Documentation/ScriptReference/ParticleAnimator-colorAnimation.html 
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
    private bool isbullet1;
    private bool isbullet2;

    public GameObject bulletexplosion;

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
        isbullet1 = false;
        isbullet2 = false;
    }

	// Use this for initialization
	void Start () {
        // Check which bullet is fired
        if (myTransform.tag.Equals("bullet1"))
        {
            isbullet1 = true;
            float rr = .9f;
            float gg = .9f;
            float bb = .5f;
            float aa = .5f;
            changeBulletExplosionColor(rr,gg,bb,aa);

        }
        else
            if (myTransform.tag.Equals("bullet2"))
         {
                isbullet2 = true;
                float rr = .59f;
                float gg = .57f;
                float bb = .9f;
                float aa = .5f;
                changeBulletExplosionColor(rr, gg, bb, aa);
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


            if (hit.transform.tag == "enemy1" && isbullet1)
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
            else if (hit.transform.tag == "enemy2" && isbullet2)
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
                
                Instantiate(bulletexplosion,hit.point, Quaternion.identity);
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

    void changeBulletExplosionColor(float r, float g, float b, float a)
    {
       
        /*change particle color*/
        ParticleAnimator particleAnimator = bulletexplosion.GetComponent<ParticleAnimator>();
        int i = 0;
        Color[] modifiedColors = particleAnimator.colorAnimation;
        while (i < modifiedColors.Length)
        {
            modifiedColors[i] = new Color(r, g, b, a);
            if (isbullet1)
            {
                b -= .1f;
            }
            else if (isbullet2)
            {
                r -= .1f;
            }
            i++;
        }
        particleAnimator.colorAnimation = modifiedColors;
            
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
