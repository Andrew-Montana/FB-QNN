using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitch : MonoBehaviour
{
    static public int currentLvl = 1;
    static public int totalPipes = 61;

    public GameObject pipeset1;
    public GameObject pipeset2;
    public GameObject pipeset3;

    public PipeSet pip1;
    public PipeSet pip2;
    public PipeSet pip3;

    public Bird bird;

    public void SwitchToFirst()
    {
        currentLvl = 1;
        totalPipes = 61;

        pipeset1.SetActive(true);
        pipeset2.SetActive(false);
        pipeset3.SetActive(false);

        bird.SetPipes(pip1);
        //
        ResetEverything();
    }

    public void SwitchToSecond()
    {
        currentLvl = 2;
        totalPipes = 78;

        pipeset1.SetActive(false);
        pipeset2.SetActive(true);
        pipeset3.SetActive(false);
        // 
        bird.SetPipes(pip2);
        //
        ResetEverything();
    }

    public void SwitchToThird()
    {
        currentLvl = 3;
        totalPipes = 78;

        pipeset1.SetActive(false);
        pipeset2.SetActive(false);
        pipeset3.SetActive(true);

        bird.SetPipes(pip3);
        //
        ResetEverything();
    }

    private void ResetEverything()
    {
        pip1.ResetPos();
        pip2.ResetPos();
    }
}
