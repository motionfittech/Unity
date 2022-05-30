#define Graph_And_Chart_PRO
using UnityEngine;
using ChartAndGraph;
using System.Collections;
using System.Collections.Generic;

public class GraphChartFeed : MonoBehaviour
{
   public GraphChart graph;
    void Start()
    {

       //graph = GetComponent<GraphChart>();
       // if (graph != null)
       // {
       //     graph.DataSource.ClearCategory("Player 1");  // clear the categories we
       //   // have created in the inspector
       //   //  graph.DataSource.ClearCategory("Player 2");
       //     //for (int i = 0; i < 30; i++)
       //     //{
               
       //     //    //add 30 random points , each with a category and an x,y value
       //     //    graph.DataSource.AddPointToCategory("Player 1",Random.value*10f,Random.value*10f);
       //     //  //  graph.DataSource.AddPointToCategory("Player 2", Random.value * 10f,Random.value * 10f);
       //     //}
       //     graph.DataSource.EndBatch(); // end the update batch . this call will
       ////     render the graph
       //     graph.DataSource.StartBatch();  // start a new update batch

       // }
    }


    IEnumerator ClearAll()
    {
        yield return new WaitForSeconds(5f);
        GraphChartBase graph = GetComponent<GraphChartBase>();

        graph.DataSource.Clear();
    }

    public void Singcall(float x)
    {
        //print(x+" HERE");
      // graph.DataSource.Clear();
        graph.DataSource.AddPointToCategory("Player 1", x, x/2);
        
        //    graph.DataSource.EndBatch(); // end the update batch . this call will
        ////     render the graph
        //   graph.DataSource.StartBatch();  // start a new update batch
    }



    public IEnumerator Multicall(List<float> x)
    {
        
            int i = 0;
        //        maxvalue.maxValue = tempValues.Count;
        graph.DataSource.Clear();
           
            while (i < x.Count)
            {
            graph.DataSource.AddPointToCategory("Player 1", x[i], x[i]);

            i++;
            yield return null;
        }
               
            
        
    }
}
