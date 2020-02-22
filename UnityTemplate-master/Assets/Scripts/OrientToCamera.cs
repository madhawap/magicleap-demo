﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientToCamera : MonoBehaviour
{
    private GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (camera.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}
