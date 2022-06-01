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
        float temptoPlus = 0;
        barChart.DataSource.ClearCategories();
        //        maxvalue.maxValue = tempValues.Count;
       // print(tempValues.Count);
        while (i < tempValues.Count)
        {
           
            if (tempValues[i] >= 6)
            {
                barChart.DataSource.AddCategory("SET " + counter.ToString(), mat1);
            }
            else if (tempValues[i] >= 2 && tempValues[i] < 6)
            {
                barChart.DataSource.AddCategory("SET " + counter.ToString(), mat2);
            }
            else
            {
                barChart.DataSource.AddCategory("SET " + counter.ToString(), mat3);
            }

            //   barChart.DataSource.AddCategory("SET "+i.ToString(), mat1);
            temptoPlus = Mathf.Abs(tempValues[i]);
            barChart.DataSource.SetValue("SET " + counter.ToString(), "Exercise", temptoPlus);

//            print(counter+"gfgd");
            i++;
            counter++;
            maxvalue.maxValue += 1.5f;
            yield return null;
        }


     
    }

    public void addbarSingleValue(float tempvalues)
    {
        barChart.DataSource.ClearCategories();
      //  counter += 1;
      //  maxvalue.maxValue += 1.5f;
       
            barChart.DataSource.AddCategory("Exercise 1", mat1);
       
       
        barChart.DataSource.SetValue("Exercise 1", "Exercise", tempvalues);
    }
}
