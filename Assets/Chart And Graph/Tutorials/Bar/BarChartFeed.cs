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
            barChart.DataSource.AddCategory("Player 5",mat);
            barChart.DataSource.SetValue("Player 5", "All", 10);
            barChart.DataSource.AddCategory("Player 6", mat);
            barChart.DataSource.SetValue("Player 6", "All", 20);
            barChart.DataSource.AddCategory("Player 7", mat);
            barChart.DataSource.SetValue("Player 7", "All", 5);
            barChart.DataSource.AddCategory("Player 8", mat);
            barChart.DataSource.SetValue("Player 8", "All", 15);
            barChart.DataSource.AddCategory("Player 9", mat);
            barChart.DataSource.SetValue("Player 9", "All", 30);
            barChart.DataSource.AddCategory("Player 10", mat);
            barChart.DataSource.SetValue("Player 10", "All", 40);

        }
    }
    private void Update()
    {
    }
}
