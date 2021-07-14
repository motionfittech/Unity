#define Graph_And_Chart_PRO
using UnityEngine;
using System.Collections;
using ChartAndGraph;
using System.Collections.Generic;

public class BarChartFeed : MonoBehaviour {
    public BarChart barChart;
    public Material mat;
    public List<float> values = new List<float>();

    public void start()
    {

    }

	public void addbarValue(List<float> tempValues)
    {

        for (int i = 0; i < 20; i++)
        {
            if (i > 0)
            {
                print("we are here");
                barChart.DataSource.AddCategory("SET "+i.ToString(), mat);
                barChart.DataSource.SetValue("SET " + i.ToString(), "All", i);
            }
        }
    }
}
