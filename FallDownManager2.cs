using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownManager2 : MonoBehaviour
{
    
    public GameObject spawnCharacter;
    public float planetRadius, characterHeight;
    public Vector3 planetPosition;
    public Vector3 inst_pos;
    private string[] look_at_group;
    const int size = 3;
    //public Transform  front;
    System.Random rand = new System.Random();
    private float nextActionTime, period, _timer;

    ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;

        const int size = 1;
        look_at_group = new string[size]{
                                                    "FallFromHere",
                                                    };
        nextActionTime = 0f;
        period = 1.2f;
        _timer = 0f;

        StartCoroutine(FallingDown());

    }

    private IEnumerator FallingDown()
    {        
        while (true){
            Debug.Log("pnact"); 
            yield return new WaitForSeconds(period);

            if (period > 0.3f)
                period -= 0.1f;

            inst_pos = GameObject.Find( look_at_group[ rand.Next(0, size) ] ).transform.position;

            objectPooler.SpawnFromPool("meteorite2", inst_pos);
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*
        _timer += Time.deltaTime;
        if (_timer > nextActionTime ) {
            nextActionTime += period;
            if (period > 0.4f)
                period -= 0.1f;

            Vector3 last = new Vector3(0, 0, 0);

            for(int i=1; i <= 1; i++){
                
                while(true){
                    inst_pos = GameObject.Find( look_at_group[ rand.Next(0, size) ] ).transform.position;
                    if( last != inst_pos )
                        break;
                }

                GameObject clone = (GameObject)Instantiate(spawnCharacter, inst_pos, Quaternion.identity) as GameObject;
                Destroy (clone, 2f); 
                last = inst_pos;
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