using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] allBacteria =new GameObject[4];
   // public GameObject bacteria1;
   // public GameObject bacteria2;
   // public GameObject bacteria3;
   // public GameObject bacteria4;
    public Transform level1spawner;
    private Transform myTransform;
    int randomBacteria;
    Vector3 offset;
    int rangeX;
    int rangeZ; 

    int randomSpawnPoint;

    void Awake()
    {
        myTransform = transform;
        
        

    }
	// Use this for initialization
	void Start () {

        level1spawner = GameObject.FindGameObjectWithTag("level1_enemyspawner").transform;
        
	}
	
	// Update is called once per frame
	void Update () {
        

        // spawn more enemies if enemy count is less than 2
    
        if (GameManager.activeBacteriaCount < 2)
        {
            randomBacteria = Random.Range(0, 4);
            randomRange();
            print(" bacteria added to scene");
            Instantiate(allBacteria[randomBacteria], level1spawner.position, //+ offset
                Quaternion.Euler(myTransform.eulerAngles.x, myTransform.eulerAngles.y, 0));
          
            
        }
	}

    void randomRange()
    {
        rangeX = Random.Range(-20,0);
        rangeZ = Random.Range(-15,5);
        offset = new Vector3(rangeX, -110, rangeZ);
    }
}
