// Pipes.cs
using UnityEngine;
public class Pipes1 : MonoBehaviour
{
    private const float spacing = 2f; // Distance between pipes
   // private const int totalPipes = 61;
    private Vector3 startPos;
    public float pipeVariance = .5f;
    System.Random rand;
    PipeSet pipeSet;

    private void Awake()
    {
        startPos = transform.localPosition;
        RandomizeY();
        rand = new System.Random();
        pipeSet = transform.parent.gameObject.GetComponent<PipeSet>();
    }

    private void LateUpdate()
    {
        transform.Translate(Vector3.left * Time.deltaTime);
        if (transform.localPosition.x < -1)
        {
            // next to last actually 
            Transform lastPipe = pipeSet.GetLastPipe();

            transform.position = new Vector3(lastPipe.position.x + 1.02f, 0f, lastPipe.position.z);

            float x = PipeSet.GetRandomX();
            if (x != 0)
                transform.Translate(Vector3.right * x);

            if((transform.position.x - lastPipe.position.x) > 2.4f)
            {
                RandomizeY();
            }
            else
            {
                System.Random rand = new System.Random();
                int myx = rand.Next(0, 3);
                if(myx == 0)
                    transform.position = new Vector3(transform.position.x, lastPipe.position.y, transform.position.z);
                else if(myx == 1)
                    transform.position = new Vector3(transform.position.x, lastPipe.position.y + 0.1f, transform.position.z);
                else
                    transform.position = new Vector3(transform.position.x, lastPipe.position.y - 0.1f, transform.position.z);
            }
           
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
            * Random.Range(-1f, 1f));
    }
}