#define Graph_And_Chart_PRO
using UnityEngine;
using System.Collections;
using ChartAndGraph;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class BarChartFeed : MonoBehaviour {
    public BarChart barChart;
    public Material mat1,mat2,mat3;
    public List<float> values = new List<float>();
    public Slider maxvalue;
  
    int counter = 0;
    public void Start()
    {
     //   PlayerPrefs.SetString("DailyTimer", DateTime.Now.AddHours(1).ToString());
        //var unlockDate = DateTime.Parse(PlayerPrefs.GetString("DailyTimer"));
        //if (unlockDate < DateTime.Now)
        //{
        //    //object unlocked again
        //}
        //else
        //{
        //    //object still locked, how long you ask?: 
        //    TimeSpan diff = unlockDate.Subtract(DateTime.Now);
        //    Debug.Log("object locked for " + diff.Minutes + " more minutes");
        //}
    }

	public IEnumerator addbarValue(List<float> tempValues)
    {
        int i = 0;
//        maxvalue.maxValue = tempValues.Count;
       
        while (i < 300)
        {
           
            if (tempValues[i] > 0)
            {
                
                barChart.DataSource.AddCategory("SET "+i.ToString(), mat1);
                float temp = tempValues[i] * 100;
                barChart.DataSource.SetValue("SET " + i.ToString(), "All", temp);
                
            }
            i++;
            yield return null;
        }
       
       
      
    }

    public void addbarSingleValue(float tempvalues)
    {
      
        counter += 1;
        maxvalue.maxValue = counter+2;
        if (tempvalues >= 6)
        {
            barChart.DataSource.AddCategory("SET " + counter.ToString(), mat1);
        }
        else if(tempvalues >= 2 && tempvalues < 6)
        {
            barChart.DataSource.AddCategory("SET " + counter.ToString(), mat2);
        }
        else
        {
            barChart.DataSource.AddCategory("SET " + counter.ToString(), mat3);
        }
       
        barChart.DataSource.SetValue("SET " + counter.ToString(), "All", tempvalues);
    }
}
