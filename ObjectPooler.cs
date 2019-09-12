using System.Collections;
using System.Collections.Generic;
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

    public GameObject SpawnFromPool (string tag, Vector3 position){

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(false);
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;              
        objectToSpawn.transform.rotation = Quaternion.identity; 
        if( tag == "oil" || tag == "meteorite1" )  
            objectToSpawn.transform.LookAt(planetPosition);

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if( pooledObj != null ){
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;

    }

    void Update()
    {
        
    }
}
