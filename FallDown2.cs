using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown2 : collidableObject, IPooledObject
{

    public Rigidbody rb; 
    public Transform tr;
    public Transform falling_position;
    public float thrust;
    public GameObject ExplosionEffect;
    System.Random rand = new System.Random();
    // ObjectPooler objectPooler;

    //public float period = 2f, nextActionTime = 0f, _timer= 0f, left = 1;

    void Start()
    {
        Start_child();
        // super.Start_child();
        after_collision_effect = "boom_meteortie";
        // objectPooler = ObjectPooler.Instance;
    }

    public void OnObjectSpawn()
    {   
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        const int size = 1;
        string[] look_at_group =  new string[size]{
                                                    "forest",
                                                    };
        falling_position = GameObject.Find( look_at_group[rand.Next(0,size)] ).transform;
        //ExplosionEffect = GameObject.Find("BigExplosion");

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        thrust = 4000f;
        tr.LookAt(falling_position);
        float angle = 15f;
        //Vector3 dir = Quaternion.AngleAxis(Quaternion.identity, tr.forward) * tr.forward;
        rb.AddForce(tr.forward * thrust);
    }

    void Update()
    {

    }

    void OnCollisionEnter (Collision col)
    {
        //rb.constraints = RigidbodyConstraints.FreezePosition;
        startCollisionProcess(tr.position, after_collision_effect);
        //gameObject.SetActive(false);
        // objectPooler.SpawnFromPool(after_collision_effect, tr.position);
        
        //objectPooler.SpawnFromPool("boom_meteortie", tr.position);
    }

}
