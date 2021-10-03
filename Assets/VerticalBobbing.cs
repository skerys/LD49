using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBobbing : MonoBehaviour
{
    
    public float bobbingAmount;
    public float bobbingFrequency;

    float randomOffset;

    void Start()
    {
        randomOffset = Random.Range(0, 2*Mathf.PI);
    }
    void Update()
    {
        transform.Translate(Vector3.up * Mathf.Sin(bobbingFrequency * (Time.time + randomOffset)) * bobbingAmount * Time.deltaTime);
    }
}
