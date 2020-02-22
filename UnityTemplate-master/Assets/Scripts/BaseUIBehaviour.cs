﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUIBehaviour : MonoBehaviour
{
    
    
    BaseBehaviour baseInfo;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        baseInfo = transform.parent.GetComponentInParent<BaseBehaviour>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Points: " + baseInfo.points;
    }
}
