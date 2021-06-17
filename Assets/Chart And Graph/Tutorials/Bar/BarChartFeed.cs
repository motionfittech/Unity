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
       //    barChart.DataSource.SetValue("Category 1", "All", 5);
           // barChart.DataSource.SetValue("Category 2", "All", 10);
            //  barChart.DataSource.SlideValue("Player 2", "Value 1", 15,20);
            barChart.DataSource.AddCategory("SET 1",mat);
            barChart.DataSource.SetValue("SET 1", "All", 1);
            barChart.DataSource.AddCategory("SET 2", mat);
            barChart.DataSource.SetValue("SET 2", "All", 0.5f);
            barChart.DataSource.AddCategory("SET 3", mat);
            barChart.DataSource.SetValue("SET 3", "All", 0.7f);
            barChart.DataSource.AddCategory("SET 4", mat);
            barChart.DataSource.SetValue("SET 4", "All", 0.4f);
            barChart.DataSource.AddCategory("SET 5", mat);
            barChart.DataSource.SetValue("SET 6", "All", 0.8f);
            barChart.DataSource.AddCategory("SET 7", mat);
           // barChart.DataSource.SetValue("Player 10", "All", 40);

        }
    }
    private void Update()
    {
    }
}
