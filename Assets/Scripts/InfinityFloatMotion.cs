using System;
using System.Collections;
using UnityEngine;

public class InfinityFloatMotion : MonoBehaviour
{
    public float speed = 1.0f;          
    public float size = 1.0f;          
    public float rotationSpeed = 30f;
    public float WaveSpeed = 1.0f;
    public float WaveAngle = 1.0f;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

    }
    void Update()
    {
        float t = Time.time * speed;

     
        float x = Mathf.Sin(t) * size;
        float y = Mathf.Sin(t) * Mathf.Cos(t) * size;

        transform.position = initialPosition + new Vector3(x, y, 0);

        float wave = Mathf.Sin(Time.time * WaveSpeed) * WaveAngle;

        Quaternion rot =  Quaternion.Euler(wave,0,wave).normalized;

        transform.rotation = initialRotation * rot;
    }
}