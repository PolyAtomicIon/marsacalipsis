using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oil_ColliderAndroid : MonoBehaviour
{

    public PlayerMovementScriptAndroid player_manager;
    //private GameObject ExplosionEffect;
    private Transform tr;

    ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;

        GameObject tempObj = GameObject.Find("Player");
        if ( !player_manager )
            player_manager = tempObj.GetComponent<PlayerMovementScriptAndroid>();
        //ExplosionEffect = GameObject.Find("boom_oil");
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
            objectPooler.SpawnFromPool("boom_oil", tr.position);

            gameObject.SetActive(false);
        }
    }

}
