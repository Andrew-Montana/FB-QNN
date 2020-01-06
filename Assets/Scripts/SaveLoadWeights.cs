using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadWeights : MonoBehaviour
{
    Bird bird;
    ANN ann;
    // Start is called before the first frame update
    private void Start()
    {
        bird = GameObject.Find("bird").GetComponent<Bird>();
    }

    public void SaveWeights()
    {
        ann = bird.GetANN();

        System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\Users\Ghost\Desktop\weights.txt");
        sw.Write(ann.PrintWeights());
        sw.Close();

        System.IO.StreamWriter sw2 = new System.IO.StreamWriter(@"C:\Users\Ghost\Desktop\bias.txt");
        sw2.Write(ann.PrintBias());
        sw2.Close();
    }

    public void LoadWeights()
    {
        ann = bird.GetANN();
        string weights, bias;

        System.IO.StreamReader sr = new System.IO.StreamReader(@"C:\Users\Ghost\Desktop\weights.txt");
        weights = sr.ReadToEnd();
        weights = weights.Replace(",", ".");
        sr.Close();

        System.IO.StreamReader sr2 = new System.IO.StreamReader(@"C:\Users\Ghost\Desktop\bias.txt");
        bias = sr2.ReadToEnd();
        bias = bias.Replace(",", ".");
        sr2.Close();
        //
        string[] splitWeights = weights.Split('!');
        string[] splitBias = bias.Split('!');
        //
        List<double> weightsList = new List<double>();
        List<double> biasList = new List<double>();
        foreach(string str in splitWeights)
        {
            if (str == "" || string.IsNullOrEmpty(str))
                continue;

            //  double tmp;
            //  double.TryParse(str, out tmp);
            double value = System.Convert.ToDouble(str, System.Globalization.CultureInfo.InvariantCulture);
          weightsList.Add(value);
        }
        foreach (string str in splitBias)
        {
            if (str == "" || string.IsNullOrEmpty(str))
                continue;

            double value = System.Convert.ToDouble(str, System.Globalization.CultureInfo.InvariantCulture);
            biasList.Add(value);
        }

        ann.LoadWeights(weightsList, biasList);
    }
}
