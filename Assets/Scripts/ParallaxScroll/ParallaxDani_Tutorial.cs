﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ParallaxDani_Tutorial : MonoBehaviour
{
    private float length, startPos;
    public GameObject cam;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log($"{gameObject.name} : length {length} ");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos * dist, transform.position.y, transform.position.z);
        if(temp > startPos + length)startPos += length;
        else if(temp < startPos - length) startPos -= length;
        
    }
}
