// Bird.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Replay
{
    public List<double> states;
    public double reward;

    public Replay(double dtop, double dbot, double horD, double topD, double botD, double r)
    {
        states = new List<double>();
        states.Add(dtop);
        states.Add(dbot);
        states.Add(horD);
        states.Add(topD);
        states.Add(botD);
        reward = r;
    }
}

public class Bird : MonoBehaviour
{
    #region ann part
    ANN ann;
    private float reward = 0.0f;                            //reward to associate with actions
    private List<Replay> replayMemory = new List<Replay>(); //memory - list of past actions and rewards
    private int mCapacity => 10000;                          //memory capacity

    private float discount => 0.99f;                         //how much future states affect rewards
    private float exploreRate = 100.0f;                     //chance of picking random action
    private float maxExploreRate => 100.0f;					//max chance value
    private float minExploreRate => 0.01f;                   //min chance value
    private float exploreDecay => 0.0001f;                   //chance decay amount for each update

    int failCount = 0;
    #endregion

    private Rigidbody2D myBody;
    private Vector3 startPos;
    private bool dead = false;

    private bool screenPressed = false;
    const float height = 2f; // Distance of the center to the top/bottom
    const float pipeSpace = .6f;  // Pipes are offset by .6 on Y-axis

    public PipeSet pipes;
    public float counter = 0f;

    private void Start()
    {
        Time.timeScale = 1;
        ann = new ANN(5, 2, 1, 6, 0.2f);
        myBody = GetComponent<Rigidbody2D>();
        startPos = transform.localPosition;
    }
    bool isScale = false;
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            // ResetBird();
            if (isScale)
            { isScale = false;
                Time.timeScale = 1f;
            }
            else
            { isScale = true;
                Time.timeScale = 75f;
            }
        }
    }

    private void FixedUpdate()
    {
        counter += Time.deltaTime;
        // Init
        List<double> states = new List<double>();
        List<double> qs = new List<double>();

        // Observe states
        states = CollectObservations();

        // Get Action
        qs = SoftMax(ann.CalcOutput(states));
        double maxQ = qs.Max();
        int maxQIndex = qs.ToList().IndexOf(maxQ);
        exploreRate = Mathf.Clamp(exploreRate - exploreDecay, minExploreRate, maxExploreRate);

        //if (Random.Range(0, 100) < exploreRate)
          //  maxQIndex = Random.Range(0, 2);

        // Perform Action
        AgentAction(maxQIndex);

        // Access Replay Memory
        List<double> newStates = CollectObservations();
        // Replay lastMemory = new Replay(newStates[0], newStates[1], newStates[2], newStates[3], newStates[4], reward);
        Replay lastMemory = new Replay(states[0], states[1], states[2], states[3], screenPressed ? 1f : -1, reward);
        if (replayMemory.Count > mCapacity)
            replayMemory.RemoveAt(0);

        replayMemory.Add(lastMemory);

        if (dead)
            TrainAfterDead();

    }

    private void TrainAfterDead()
    {
        for (int i = replayMemory.Count - 1; i >= 0; i--)
        {
            List<double> toutputsOld = new List<double>();
            List<double> toutputsNew = new List<double>();
            toutputsOld = SoftMax(ann.CalcOutput(replayMemory[i].states));

            double maxQOld = toutputsOld.Max();
            int action = toutputsOld.ToList().IndexOf(maxQOld);

            double feedback;
            if (i == replayMemory.Count - 1 || replayMemory[i].reward == -1)
                feedback = replayMemory[i].reward;
            else
            {
                toutputsNew = SoftMax(ann.CalcOutput(replayMemory[i + 1].states));
                double maxQ = toutputsNew.Max();
                feedback = (replayMemory[i].reward +
                    discount * maxQ);
            }

            toutputsOld[action] = feedback;
            ann.Train(replayMemory[i].states, toutputsOld);
        }

        Done();
        replayMemory.Clear();
        failCount++;
    }

    private void Push()
    {
        myBody.velocity = Vector3.zero;
        myBody.AddForce(Vector2.up * 20, ForceMode2D.Force);
    }

    public List<double> CollectObservations()
    {
        Vector3 pipePos = pipes.GetNextPipe().localPosition;
        List<double> myStates = new List<double>();
        myStates.Add(gameObject.transform.localPosition.y);
        myStates.Add(pipePos.x);
       // string str = string.Format("velocity.y = {0}, MathfClamp is {1} and final result is {2}",myBody.velocity.y, Mathf.Clamp(myBody.velocity.y, -height, height), Mathf.Clamp(myBody.velocity.y, -height, height) / height);
       // Debug.Log(str);

        myStates.Add((pipePos.y - pipeSpace) );
        myStates.Add((pipePos.y + pipeSpace) );
        myStates.Add(screenPressed ? 1f : -1);
        return myStates;
    }

    public void AgentAction(int actionIndex)
    {
        if (dead)
        {
            reward = -1f;
         //   Done();
        }
        else
        {
            reward = 0.01f;

            int tap = Mathf.FloorToInt(actionIndex);
            if (tap == 0)
            {
                screenPressed = false;
            }
            if (tap == 1 && !screenPressed)
            {
                screenPressed = true;
                Push();
            }
        }
    }

    public void Done()
    {
        AgentReset();
    }

    public void AgentReset()
    {
        myBody.velocity = Vector3.zero;
        transform.localPosition = startPos;
        dead = false;
        pipes.ResetPos();
        counter = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision2d)
    {
        dead = true;
    }

    List<double> SoftMax(List<double> oSums)
    {
        double max = oSums.Max();

        float scale = 0.0f;
        for (int i = 0; i < oSums.Count; ++i)
            scale += Mathf.Exp((float)(oSums[i] - max));

        List<double> result = new List<double>();
        for (int i = 0; i < oSums.Count; ++i)
            result.Add(Mathf.Exp((float)(oSums[i] - max)) / scale);

        return result;
    }
}
