// PipeSet.cs
using UnityEngine;
public class PipeSet : MonoBehaviour
{
    private static System.Random rand = new System.Random();

    public void ResetPos()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Pipes1>().InitialPosition();
        }
    }

    public static float GetRandomX()
    {
       return rand.Next(0, 5);
    }

    public Transform GetLastPipe()
    {
        float lastPipe = -999f;
        Transform lastChild = null;
        foreach(Transform child in transform)
        {
            if (child.localPosition.x > lastPipe)
            {
                lastPipe = child.localPosition.x;
                lastChild = child;
            }
        }
        return lastChild;
    }

    public Transform GetNextPipe()
    {
        float leftMost = float.MaxValue;
        Transform leftChild = null;
        foreach (Transform child in transform)
        {
            if (child.localPosition.x < leftMost &&
                child.localPosition.x > -.3f)
            {
                leftChild = child;
                leftMost = child.localPosition.x;
            }
        }
        return leftChild;
    }
}