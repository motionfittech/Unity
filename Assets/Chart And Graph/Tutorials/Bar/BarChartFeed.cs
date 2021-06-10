#define Graph_And_Chart_PRO
using UnityEngine;
using System.Collections;
using ChartAndGraph;

public class BarChartFeed : MonoBehaviour {
    public BarChart barChart;
    public Material mat;
	void Start () {
       
        if (barChart != null)
        {
         //  barChart.DataSource.SetValue("Player 1", "Value 1", 20);
          //  barChart.DataSource.SlideValue("Player 2", "Value 1", 15,20);
            barChart.DataSource.AddCategory("Player 5",mat);
            barChart.DataSource.SetValue("Player 5", "Value 1", 10);

        }
    }
    private void Update()
    {
    }
}
