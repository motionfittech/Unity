using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;
using UnityEditor;

[System.Serializable]
public class DimensionValue
{
    public string name;
    public string formula;
}

[System.Serializable]
public class Dimension
{
    public string name;
    public string graph;
    public List<DimensionValue> values;
}

[System.Serializable]
public class Datapoint 
{
    public string name;
    public List<Dimension> dimensions;
    private string currentButton = "Yearly";
    
    public string getCurrentRenderedView()
    {
        return currentButton;
    }
    public void Render(NormalAnalysis normalAnalysis, string name)
    {
        currentButton = name;
        foreach(Dimension dimension in dimensions)
        {
            if (dimension.name.ToLower() == name.ToLower())
            {
                if (dimension.graph == "bar")
                {
                    BarChart chart = normalAnalysis.getBarChart();
                    chart.gameObject.SetActive(true);

                    // max value for bar graph
                    chart.DataSource.MaxValue = 100;
                    chart.DataSource.ClearGroups();
                    chart.DataSource.AddGroup(dimension.name);
                    // Data will be fetched from the backend, setting temporary values
                    foreach (DimensionValue d in dimension.values)
                    {
                        double value;
                        ExpressionEvaluator.Evaluate<double>(d.formula, out value);
                        chart.DataSource.SetValue(d.name, dimension.name, value);
                    }
                }
                else if(dimension.graph == "pie")
                {
                    PieChart chart = normalAnalysis.getPieChart();
                    chart.gameObject.SetActive(true);

                    // Data will be fetched from the backend, setting temporary values
                    int i = 1;
                    foreach (DimensionValue d in dimension.values)
                    {
                        if (chart.DataSource.HasCategory("Category " + i))
                        {
                            chart.DataSource.RenameCategory("Category " + i, d.name);
                        }
                        i++;
                        double value;
                        ExpressionEvaluator.Evaluate<double>(d.formula, out value);
                        chart.DataSource.SetValue (d.name, value);
                    }
                }
                return;
            }
        }
    }
}

[System.Serializable]
public class Exercise
{
    public string name;
    public List<Datapoint> datapoints;
    private int index = 0;

    public Datapoint getCurrent()
    {
        return datapoints[index];
    }

    public Datapoint getNext()
    {
        if (index < datapoints.Count - 1)
        {
            index++;
            return datapoints[index];
        }
        else
            return datapoints[datapoints.Count - 1];
    }

    public Datapoint getPrevious()
    {
        if (index > 0)
        {
            index--;
            return datapoints[index];
        }
        else
            return datapoints[0];
    }
}

[System.Serializable]
public class ApiResponse
{
    public int status;
    public List<Exercise> data;
    private int index = 0;

    public Exercise getCurrent()
    {
        return data[index];
    }

    public Exercise getNext()
    {
        if (index < data.Count - 1)
        {
            index++;
            return data[index];
        }
        else
            return data[data.Count - 1];
    }

    public Exercise getPrevious()
    {
        if (index > 0)
        {
            index--;
            return data[index];
        }
        else
            return data[0];
    }
}

public class Api : MonoBehaviour
{
    // this will be replaced by backend calls
    public TextAsset exercises;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ApiResponse getExercises()
    {
        ApiResponse response= JsonUtility.FromJson<ApiResponse>(exercises.text);
        return response;
    }
}
