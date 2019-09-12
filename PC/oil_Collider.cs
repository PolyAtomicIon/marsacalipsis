using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oil_Collider : MonoBehaviour
{

    public PlayerMovementScript player_manager;
    private GameObject ExplosionEffect;
    private Transform tr;
    
    private float nextActionTime, period, _timer;

    void Start()
    {
        period = 1.2f;
        _timer = 0f;
        GameObject tempObj = GameObject.Find("Player");
        if ( !player_manager )
            player_manager = tempObj.GetComponent<PlayerMovementScript>();
        ExplosionEffect = GameObject.Find("boom_oil");
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
            GameObject clone = (GameObject)Instantiate(ExplosionEffect, tr.position, Quaternion.identity);
            Destroy (clone, 1.5f);    
            gameObject.SetActive(false);
        }
    }

}
