

using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public Color32  crossColor;
    Rect position;
   // float size = 32;
    
    //raycasting
    public Transform rayOrigin;
    float rayRange = 50;
    RaycastHit hit;
    bool hitSomething;

    void Start()
    {
        position = new Rect((Screen.width - crosshairTexture.width) / 2, (Screen.height -
          crosshairTexture.height) / 2, crosshairTexture.width, crosshairTexture.height);
        Screen.lockCursor = true;
    }
    
    void Update()
    {

        Vector3 direction;
        direction = rayOrigin.forward;
        hitSomething = Physics.Raycast(rayOrigin.position, direction, out hit, rayRange);

        if (hitSomething)
        {
            if (hit.collider.gameObject.tag == "enemy")
            {
                Debug.Log("enemy is spotted");
                crossColor = Color.red;
            }

            else if (hit.collider.gameObject.tag == "friendly")
            {
                Debug.Log("enemy is spotted");
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