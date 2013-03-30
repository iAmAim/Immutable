

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Transform))]

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public Color32  crossColor;
    public Font myFont;
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

    public string ecoliDescription;
    public string staphDescription;
    public string streptoDescription;
    public string spiroDescription;
    public string bcellDescription;
    public string neutroDescription;
    public string bullet1Description;
    public string bullet2Description;
    public string bullet3Description;
    string objectTag;

    public string[] myString;
    int organism;
    int temp = 0;



    void Awake()
    {
        myString = new string[10];
        ecoliDescription = "Bacteria Name: E. coli-                                                                       " +
        "E. coli is a common type of bacteria that can get into food, like beef and vegetables. E. coli is short for the medical term Escherichia coli" +
        ""+
        "" +
        "";

        staphDescription = "Bacteria Name: Staphyloccocus aureus - ";
        streptoDescription = "Bacteria Name: Streptococcus aureus - ";
        spiroDescription = "Bacteria Name: Spirochetes";
        bcellDescription = "Cell Name: Bcell";
        neutroDescription = "Cell Name: Neutrophil";
        bullet1Description = "Antibody Type: IgD";
        bullet2Description = "Antibody Type: IgG";
        bullet3Description = "Antibody Type: IgM";

        showDescription = false;
        myString[1] = ecoliDescription;
        myString[2] = staphDescription;
        myString[3] = streptoDescription;
        myString[4] = spiroDescription;
        myString[5] = bcellDescription;
        myString[6] = neutroDescription;
        myString[7] = bullet1Description;
        myString[8] = bullet2Description;
        myString[9] = bullet3Description;

        //neutrophil
        //bullet1
        //bullet2

    }
    void Start()

    {
        /*Crosshair position on screen */
        crosshairposition = new Rect((Screen.width - crosshairTexture.width) / 2, (Screen.height -
        crosshairTexture.height) / 2, crosshairTexture.width, crosshairTexture.height);

        /*Description panel position (bottom right)*/
        descriptionPanel = new Rect(1100, 400, 200, 200);

        Screen.lockCursor = true;

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

                objectTag = hit.collider.gameObject.tag;

                switch (objectTag)
                {
                    case "enemy1": 
                     Debug.Log("ecoli spotted");
                     crossColor = Color.red;
                     InspectObject();
                     temp = 1;
                  
             
                        break;

                    case "enemy2":
                     Debug.Log("staphylococcus spotted");
                     crossColor = Color.red;
                    InspectObject();
                     temp = 2;
                     


                        break;
                    case "enemy3":
                    crossColor = Color.red;
                    InspectObject();
                    temp = 3;

                        break;

                    case "enemy4":
                    crossColor = Color.red;
                    InspectObject();
                    temp = 4;

                        break;

                    case "tutorial_bcell":
                    crossColor = Color.green;
                    InspectObject();
                    temp = 5;

                        break;
                    case "tutorial_neutrophil":
                        crossColor = Color.green;
                        InspectObject();
                        temp = 6;

                        break;
                    case "tutorial_bullet1":
                        crossColor = Color.green;
                        InspectObject();
                        temp = 7;

                        break;
                    case "tutorial_bullet2":
                        crossColor = Color.green;
                        InspectObject();
                        temp = 8;

                        break;
                    case "tutorial_bullet3":
                        crossColor = Color.green;
                        InspectObject();
                        temp = 8;

                        break;



                    default:
                        crossColor = Color.white;
                        temp = 0;
                        break;

                }

            }
            else
            {
                rayTarget.localPosition = new Vector3(0f, 0f, 120f);
                crossColor = Color.white;
               // showDescription = false;
            }

           // Debug.Log("objectDesc is " + temp);
    }


    void OnGUI()
    {
        // Draw crosshair on the screen
        GUI.color = crossColor;
        GUI.DrawTexture(crosshairposition, crosshairTexture);

        GUI.skin.font = myFont;

        //Show description panel
       if (showDescription )
        {
            
            GUI.color = Color.cyan;
            GUI.Label(descriptionPanel, myString[organism]);

        }
       // else //hide description panel
       // {
           // showDescription = false;
       // }

 

    }

  void InspectObject()
    {
        int objectDescription = 0;
        showDescription = true;
           if (Input.GetKeyDown(KeyCode.E))
           {
               showDescription = true;
               objectDescription = temp;
               organism = temp;

           }
 
    }




}