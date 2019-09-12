using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown2 : MonoBehaviour, IPooledObject
{

    public Rigidbody rb; 
    public Transform tr;
    public Transform falling_position;
    public float thrust;
    public GameObject ExplosionEffect;
    System.Random rand = new System.Random();
    ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    public void OnObjectSpawn()
    {   
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        const int size = 1;
        string[] look_at_group =  new string[size]{
                                                    "falling_position",
                                                    };
        falling_position = GameObject.Find( look_at_group[rand.Next(0,size)] ).transform;
        //ExplosionEffect = GameObject.Find("BigExplosion");

        thrust = 2000f;
        tr.LookAt(falling_position);
        float angle = 15f;
        //Vector3 dir = Quaternion.AngleAxis(Quaternion.identity, tr.forward) * tr.forward;
        rb.AddForce(tr.forward * thrust);
    }

    void Update()
    {
        //tr.position = Vector3.MoveTowards(tr.position, planet.position, 10f);
    }

    void OnCollisionEnter (Collision col)
    {
        //rb.constraints = RigidbodyConstraints.FreezePosition;
        gameObject.SetActive(false);
        objectPooler.SpawnFromPool("boom_meteortie", tr.position);
    }

}
