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
    private bool expended = false;

    /* Ray projected in front of the projectile
    to see if it will recognize a collider */
    private RaycastHit hit;

    // The range of the ray
    private float range = 1.5f;

    // lifespan of the projectile

    private float bulletLife = 5;

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

        //Translate the projectile in the up diretction cause bullet travels on  Y axis
        myTransform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);

        // if the ray hits something then execute this code.

        if (Physics.Raycast(myTransform.position, myTransform.up, out hit, range) &&
            expended == false)
        {
            //if collider has tag 'Ground'
            if (hit.transform.tag == "Ground")
            {
                expended = true;
                //make projectile disappear
                myTransform.renderer.enabled = false;

                //turn off light component as well ( halo will also disappear)
                myTransform.light.enabled = false;
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
}
