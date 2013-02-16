using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AIBridges : MonoBehaviour {

    // define List if bridge GameObjects
    public List<GameObject> bridges;

    void Start()
    {   //dynamically create list 
        bridges = new List<GameObject> { };
    }
         
// Sectors and bridges which are in contact with each other
void  OnTriggerEnter ( Collider collider  ){
    if (collider.tag == "AISector")

        // put objects(sectors) in the List
        bridges.Add(collider.gameObject);
}
}