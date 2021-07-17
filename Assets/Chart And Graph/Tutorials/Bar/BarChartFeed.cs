#define Graph_And_Chart_PRO
using UnityEngine;
using System.Collections;
using ChartAndGraph;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class BarChartFeed : MonoBehaviour {
    public BarChart barChart;
    public Material mat;
    public List<float> values = new List<float>();
    public Slider maxvalue;
    public GameObject canvas;
    public void Start()
    {
     //   PlayerPrefs.SetString("DailyTimer", DateTime.Now.AddHours(1).ToString());
        var unlockDate = DateTime.Parse(PlayerPrefs.GetString("DailyTimer"));
        if (unlockDate < DateTime.Now)
        {
            //object unlocked again
        }
        else
        {
            //object still locked, how long you ask?: 
            TimeSpan diff = unlockDate.Subtract(DateTime.Now);
            Debug.Log("object locked for " + diff.Minutes + " more minutes");
        }
    }

	public IEnumerator addbarValue(List<float> tempValues)
    {
        int i = 0;
//        maxvalue.maxValue = tempValues.Count;
       
        while (i < 300)
        {
           
            if (tempValues[i] > 0)
            {
                
                barChart.DataSource.AddCategory("SET "+i.ToString(), mat);
                float temp = tempValues[i] * 100;
                barChart.DataSource.SetValue("SET " + i.ToString(), "All", temp);
                
            }
            i++;
            yield return null;
        }
        barChart.gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);
      
    }

    public void addbarSingleValue(float tempvalues)
    {
        print("we are here");
        barChart.DataSource.AddCategory("SET " , mat);
        float temp = tempvalues * 100;
        barChart.DataSource.SetValue("SET " , "All", temp);
    }
}
