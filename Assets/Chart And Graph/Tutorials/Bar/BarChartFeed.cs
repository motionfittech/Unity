#define Graph_And_Chart_PRO
using UnityEngine;
using System.Collections;
using ChartAndGraph;
using System.Collections.Generic;
using UnityEngine.UI;
public class BarChartFeed : MonoBehaviour {
    public BarChart barChart;
    public Material mat;
    public List<float> values = new List<float>();
    public Slider maxvalue;
    public void start()
    {

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
                barChart.DataSource.SetValue("SET " + i.ToString(), "All", tempValues[i]);
                
            }
            i++;
            yield return null;
        }

      
    }
}
