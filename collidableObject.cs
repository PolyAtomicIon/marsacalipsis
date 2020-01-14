using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidableObject : MonoBehaviour
{

    public ObjectPooler objectPooler;

    public void Start_child(){
        objectPooler = ObjectPooler.Instance;
    }

    public string after_collision_effect = "";
    public void startCollisionProcess (Vector3 object_position, string after_collision_effect)
    {
        //rb.constraints = RigidbodyConstraints.FreezePosition;
        Debug.Log(after_collision_effect + " collidable object");
        objectPooler.SpawnFromPool(after_collision_effect, object_position);
        gameObject.SetActive(false);
    }

}
