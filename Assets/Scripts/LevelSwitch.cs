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
    public GameObject pipeset4;

    public PipeSet pip1;
    public PipeSet pip2;
    public PipeSet pip3;
    public PipeSet pip4;

    public Bird bird;

    public void SwitchToFirst()
    {
        currentLvl = 1;
        totalPipes = 61;

        pipeset1.SetActive(true);
        pipeset2.SetActive(false);
        pipeset3.SetActive(false);
        pipeset4.SetActive(false);

        ResetEverything(currentLvl);
        bird.SetPipes(pip1);
        //
    }

    public void SwitchToSecond()
    {
        currentLvl = 2;
        totalPipes = 78;

        pipeset1.SetActive(false);
        pipeset2.SetActive(true);
        pipeset3.SetActive(false);
        pipeset4.SetActive(false);
        // 
        ResetEverything(currentLvl);
        bird.SetPipes(pip2);
        //
    }

    public void SwitchToThird()
    {
        currentLvl = 3;
        totalPipes = 78;

        pipeset1.SetActive(false);
        pipeset2.SetActive(false);
        pipeset3.SetActive(true);
        pipeset4.SetActive(false);

        ResetEverything(currentLvl);
        bird.SetPipes(pip3);
        //
    }

    public void SwitchToFourth()
    {
        currentLvl = 4;
        totalPipes = 78;

        pipeset1.SetActive(false);
        pipeset2.SetActive(false);
        pipeset3.SetActive(false);
        pipeset4.SetActive(true);

        ResetEverything(currentLvl);
        bird.SetPipes(pip4);
        //

    }

    private void ResetEverything(int index)
    {
        switch(index)
        {
            case 1:
                pip1.ResetPos();
                break;

            case 2:
                pip2.ResetPos();
                break;

            case 3:
                pip3.ResetPos();
                break;
            case 4:
                pip4.ResetPos();
                break;
        }
    }
}
