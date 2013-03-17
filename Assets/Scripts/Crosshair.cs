

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Transform))]

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public Color32  crossColor;
    Rect crosshairposition;
    Rect descriptionPanel;
   // float size = 32;
    
    //raycasting
    public Transform rayOrigin;
    public Transform rayTarget;
    public float rayRange = 2;
    RaycastHit hit;
    bool hitSomething;
    bool showDescription;
    Vector3 rayDirection; //direction where bullet is heading

    string enemy1Desc;
   

    void Awake()
    {
        enemy1Desc = "Enemy Name: Ecoli                                                                       " +
        "E. coli is a common type of bacteria that can get into food, like beef and vegetables. E. coli is short for the medical term Escherichia coli" +
        ""+
        "" +
        ""; ;
       // GameObject rayTargetObj = GameObject.FindGameObjectWithTag("rayTarget");
       // rayTarget = rayTargetObj.transform;
        showDescription = false;
    }
    void Start()

    {
        /*Crosshair position on screen */
        crosshairposition = new Rect((Screen.width - crosshairTexture.width) / 2, (Screen.height -
        crosshairTexture.height) / 2, crosshairTexture.width, crosshairTexture.height);

        /*Description panel position (bottom right)*/
        descriptionPanel = new Rect(1100, 400, 200, 200);

        Screen.lockCursor = true;
      
        rayTarget.localPosition = new Vector3(0f, 0f, 120f); //move this to cameraSettings
        rayOrigin.localPosition = new Vector3(0f, 0f, 5f); //move this to cameraSettings
    }
    
    void Update()
    {
        // we need layermask to ignore crosshair raycast detecting bullet collider    
        int bulletLayer = 8;
        // Bit shift the index of the bulletLayer to get a bit mask
        int layerMask = 1 << bulletLayer;
        // This would cast rays only against colliders in bulletLayer.
        //But instead we want to collide against everything except bulletLayer. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        //rayDirection = camera.transform.forward;
        rayDirection = rayOrigin.forward;
       
        hitSomething = Physics.Raycast(rayOrigin.position, rayDirection, out hit, rayRange, layerMask);
        
       
        
            if (hitSomething)
            {
             
                /*Move rayTarget gameobject to the hit coordinates*/
                rayTarget.transform.position = hit.point;


                Debug.DrawLine(rayOrigin.position, hit.point, Color.green);
                /*Debug statement */

                if (hit.collider.gameObject.tag == "enemy1")
                {
                    Debug.Log("enemy is spotted");
                    crossColor = Color.red;
                   showDescription = true;

                }

                else if (hit.collider.gameObject.tag == "friendly")
                {
                    Debug.Log("friend is spotted");
                    crossColor = Color.green;
                   
                    
                }
                else
                {
                   
                    crossColor = Color.white;
                   // showDescription = false;
                }

            }
            else
            {
                rayTarget.localPosition = new Vector3(0f, 0f, 120f);
                crossColor = Color.white;
               // showDescription = false;
            }
        
       
    }


    void OnGUI()
    {
        GUI.color = crossColor;
        GUI.DrawTexture(crosshairposition, crosshairTexture);

        if (showDescription)
        {
            GUI.color = Color.cyan;
            GUI.Label(descriptionPanel, enemy1Desc);
        }
  
        
    }
}