

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Transform))]

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public Color32  crossColor;
    Rect position;
   // float size = 32;
    
    //raycasting
    public Transform rayOrigin;
    public Transform rayTarget;
    public float rayRange = 2;
    RaycastHit hit;
    bool hitSomething;
    Vector3 rayDirection; //direction where bullet is heading

   
    void Start()

    {
        position = new Rect((Screen.width - crosshairTexture.width) / 2 , (Screen.height -
          crosshairTexture.height) / 2, crosshairTexture.width, crosshairTexture.height);
        Screen.lockCursor = true;
        GameObject rayTarget = GameObject.FindGameObjectWithTag("rayTarget");
        
        
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


                rayTarget.transform.position = hit.point;
                Debug.DrawLine(rayOrigin.position, hit.point, Color.green);

                if (hit.collider.gameObject.tag == "enemy")
                {
                    Debug.Log("enemy is spotted");
                    crossColor = Color.red;
                   // Debug.DrawLine(rayOrigin.position,hit.point);
                }

                else if (hit.collider.gameObject.tag == "friendly")
                {
                    Debug.Log("friend is spotted");
                    crossColor = Color.green;
                    
                }
                else
                {
                    
                    crossColor = Color.white;
                }

            }
            else
            {
                crossColor = Color.white;
            }
        
       
    }

    void OnGUI()
    {
        GUI.color = crossColor;
        GUI.DrawTexture(position, crosshairTexture);
        
    }
}