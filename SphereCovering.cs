using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCovering : MonoBehaviour
{

    public Vector3 planet_center;
    public float radius;
    public GameObject blackMaterial;
    public GameObject planet;

    ObjectPooler objectPooler;
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        blackMaterial = GetComponent<GameObject>();
        planet = GameObject.Find("forest");
        radius = 20f;
        planet_center = planet.transform.position;  
        /*
        // Generate Objects around planet
        int r = (int) radius;
        for(int x = -r; x <= r; x+=2){
            for(int y = -r; y <= r; y+=2){
                float a = (x - planet_center.x) * (x - planet_center.x); 
                float b = (y - planet_center.x) * (y - planet_center.y); 
                float c = radius * radius - a - b;
                if ( c < 0 ) continue;
                float z = Mathf.Sqrt(c) + planet_center.z;
                Vector3 spawnPosition = new Vector3(x, y, z);
                objectPooler.SpawnFromPool("cover", spawnPosition);
            }
        }
        */

    }
    
    void Update()
    {
        
    }
}
