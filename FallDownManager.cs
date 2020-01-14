using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownManager : MonoBehaviour
{
    
    public GameObject spawnCharacter;
    public float planetRadius, characterHeight;
    public Vector3 planetPosition;
    public Vector3 inst_pos;
    public Transform  front;
    System.Random rand = new System.Random();
    private float nextActionTime, period, _timer;
    ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;

        nextActionTime = 0f;
        period = 0.6f;
        _timer = 0f;

        StartCoroutine(FallingDown());
    }

    private IEnumerator FallingDown()
    {        
        while (true){
            Debug.Log("dh"); 
            yield return new WaitForSeconds(period);

            if (period > 0.3f)
                period -= 0.1f;

            Vector3 spawnPosition = Random.onUnitSphere * (planetRadius + characterHeight * 0.5f) + planetPosition;
            objectPooler.SpawnFromPool("meteorite1", spawnPosition);

            if( rand.Next(0, 100) % 5 == 0 ){

                inst_pos = front.position;

                inst_pos.z += rand.Next(-40, 40);
                inst_pos.x += rand.Next(-40, 40);

                objectPooler.SpawnFromPool("meteorite1", inst_pos); 
            
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* 
        _timer += Time.deltaTime;
        if (_timer > nextActionTime ) {
            nextActionTime += period;
            if (period > 0.6f)
                period -= 0.1f;

            Vector3 spawnPosition = Random.onUnitSphere * (planetRadius + characterHeight * 0.5f) + planetPosition;
            GameObject newCharacter = Instantiate(spawnCharacter, spawnPosition, Quaternion.identity) as GameObject;
            newCharacter.transform.LookAt(planetPosition);
            newCharacter.transform.Rotate(-90, 0, 0);

            Destroy(newCharacter, 2f);

            if( rand.Next(0, 100) % 5 == 0 ){

                for(int i=1; i<= rand.Next(1, 100) % 3; i++){
                    inst_pos = front.position;

                    inst_pos.z += rand.Next(-40, 40);
                    inst_pos.x += rand.Next(-40, 40);

                    GameObject clone = (GameObject)Instantiate(spawnCharacter, inst_pos, Quaternion.identity) as GameObject;
                    Destroy (clone, 2f); 
                }
            
            }
        }
        */
    }
}

/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownManager : MonoBehaviour
{

    private float nextActionTime = 0f, period = 1f;
    public GameObject asteroid_group;
    System.Random rand = new System.Random();
    public GameObject front, back;
    private float DestroyTime = 2f;

    void Start()
    {
    }

    void create_clone(Vector3 inst_pos){
        GameObject clone = (GameObject)Instantiate(asteroid_group, inst_pos, Quaternion.identity);
        Destroy (clone, DestroyTime);      
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime ) {
            nextActionTime += period;
            //position_camera = GameObject.Find("Main Camera").transform.rotation;
            Vector3 inst_pos = front.transform.position;
            Vector3 back_pos = back.transform.position;

            if ( nextActionTime / period < 15 ){
                create_clone(back_pos);
                create_clone(inst_pos);
            }

            else if( nextActionTime / period < 40 ){
                create_clone(back_pos);
                back_pos.x -= rand.Next(50, 80);
                create_clone(back_pos);

                inst_pos.z += rand.Next(15, 40);
                inst_pos.x += rand.Next(45, 80);
                create_clone(inst_pos);  
            }

            else{
                create_clone(back_pos);
                back_pos.x += rand.Next(20, 40);

                create_clone(inst_pos);
                inst_pos.x += rand.Next(20, 40);
                inst_pos.z += rand.Next(15, 40);
                inst_pos.x += rand.Next(45, 80);
                create_clone(inst_pos);  
            }
        }
    }

}


 */