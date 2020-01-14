using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oil_manager : MonoBehaviour
{
    private float nextActionTime, period, _timer, decrementionActionTime, decrementionPeriod;

    public Vector3 planetPosition;
    public float planetRadius;
    public float characterHeight;

    ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        nextActionTime = 0f;
        decrementionActionTime = 0f;
        decrementionPeriod = 5f;
        period = 1.2f;
        _timer = 0f;
    }
    
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > nextActionTime ) {
            nextActionTime += period;
            Vector3 spawnPosition = Random.onUnitSphere * (planetRadius + characterHeight * 0.5f) + planetPosition;

            if( _timer > decrementionActionTime ){
                decrementionActionTime += decrementionPeriod;
                objectPooler.SpawnFromPool("oil", spawnPosition, true);
            }
            else
                objectPooler.SpawnFromPool("oil", spawnPosition);
        }
    }
}
