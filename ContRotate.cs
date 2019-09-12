using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotationsPerMinute = 10.0f;
        transform.Rotate(6 * rotationsPerMinute * Time.deltaTime, 20 * rotationsPerMinute * Time.deltaTime, 0);
    }
}
