using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool{
        public string tag;
        public GameObject prefab;
        public int size;
        public float characterHeight;
    }

    enum Constant_values{
        min_number_oils = 5
    }

    #region Singleton

    public static ObjectPooler Instance;

    private void Awake(){
        Instance = this;
    }

    #endregion

    public Vector3 planetPosition;

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools){
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++ ){

                GameObject obj = Instantiate(pool.prefab) as GameObject;
                obj.SetActive(false);

                objectPool.Enqueue(obj);

            }

            poolDictionary.Add(pool.tag, objectPool);
        }

    }

    public GameObject SpawnFromPool (string tag, Vector3 position, bool is_destroy = false){
        
        Debug.Log(tag + " what is it ?");

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(false);
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;              
        objectToSpawn.transform.rotation = Quaternion.identity; 
        if( tag == "oil" || tag == "meteorite1" )  
            objectToSpawn.transform.LookAt(planetPosition); 

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        
        try {
            Debug.Log(tag + " polymorphism ??? Whooaaaaaahaha");
            pooledObj.OnObjectSpawn();
        } catch (NullReferenceException e) {
            Debug.Log("Exception caught: object is null");
        } 
        
        if (!is_destroy || poolDictionary[tag].Count <= (int) Constant_values.min_number_oils)
            poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;

    }

    void Update()
    {
        
    }
}
