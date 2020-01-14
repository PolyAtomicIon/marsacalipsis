using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oil_ColliderAndroid : collidableObject, IPooledObject
{
    public PlayerMovementScriptAndroid player_manager;
    //private GameObject ExplosionEffect;
    private Transform tr;
    string after_collision_effect;
    
    // public ObjectPooler objectPooler;
    void Start()
    {
        Start_child();
        after_collision_effect = "boom_oil";
        //objectPooler = ObjectPooler.Instance;
        GameObject tempObj = GameObject.Find("Player");
        if ( !player_manager )
            player_manager = tempObj.GetComponent<PlayerMovementScriptAndroid>();
        //ExplosionEffect = GameObject.Find("boom_oil");
    }

    public void OnObjectSpawn(){   
        tr = GetComponent<Transform>();

        tr.Rotate(-90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.name == "Player")
        {
            player_manager.update_fuel();
            Debug.Log("+ 25 oil");
            // objectPooler.SpawnFromPool(after_collision_effect, tr.position);
            startCollisionProcess(tr.position, after_collision_effect);
        }
    }

}
