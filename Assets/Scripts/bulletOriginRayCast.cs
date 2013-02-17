using UnityEngine;
using System.Collections;

public class bulletOriginRayCast : MonoBehaviour {

    public Transform bulletTarget;

    private Transform myTransform;

    void Awake()
    {
        /*Cache transform to myTransform variable.*/
        myTransform = transform;
    }

    // Use this for initialization
    void Start()
    {

        GameObject playerObj = GameObject.FindGameObjectWithTag("rayTarget");

        bulletTarget = playerObj.transform;
    
    }

    // Update is called once per frame
    void Update()
    {

        // Debug statement 
        Debug.DrawLine(bulletTarget.position, myTransform.position, Color.red);


    }

  
}
