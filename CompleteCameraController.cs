using UnityEngine;
using System.Collections;
using System;

public class CompleteCameraController : MonoBehaviour {

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Update(){

        transform.LookAt(target);

    }

}