﻿// Pipes.cs
using UnityEngine;

public class Pipes : MonoBehaviour
{
    private const float spacing = 2f; // Distance between pipes
    private const int totalPipes = 3;
    private Vector3 startPos;
    public float pipeVariance = .5f;

    private void Awake()
    {
        startPos = transform.localPosition;
        RandomizeY(); 
    }

    private void LateUpdate()
    {
        transform.Translate(Vector3.left * Time.deltaTime);
        if (transform.localPosition.x < -spacing)
        {
            transform.Translate(Vector3.right *
                spacing * totalPipes);
       //     transform.position = new Vector3(transform.position.x, transform.position.y + UnityEngine.Random.Range(-0.1f,0.1f), transform.position.z);
        }
    }

    public void InitialPosition()
    {
        transform.localPosition = startPos;
        RandomizeY();
    }

    private void RandomizeY()
    {
        transform.Translate(Vector3.up
            * Random.Range(-pipeVariance, pipeVariance));
    }
}