// Pipes.cs
using UnityEngine;
public class Pipes1 : MonoBehaviour
{
    private const float spacing = 2f; // Distance between pipes
   // private const int totalPipes = 61;
    private Vector3 startPos;
    public float pipeVariance = .5f;
    System.Random rand;

    private void Awake()
    {
        startPos = transform.localPosition;
        RandomizeY();
        rand = new System.Random();
    }

    private void LateUpdate()
    {
        transform.Translate(Vector3.left * Time.deltaTime);
        if (transform.localPosition.x < -1)
        {
            //float x = rand.Next(12, 15);
            transform.Translate(Vector3.right * 12);
            RandomizeY();
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
            * Random.Range(-0.9f, 0.9f));
    }
}