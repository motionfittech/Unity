﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using TMPro;
public class CSVManager : MonoBehaviour
{
	private TextAsset csvFile ; 
	private char lineSeperater = '\n'; // It defines line seperate character
	private char fieldSeperator = ','; // It defines field seperate chracter
	private int indexer = 0;
	public AccelerometerObjectControl AOC;
	public string saveFilename = "Default";
    Vector3 previous ;
    
	[Header("Acceleration Value in CSV Index")]
	public int IndexX = 0, IndexY = 1, IndexZ = 2 ;
	public List<float> speeds = new List<float>();
	public BarChartFeed bcf;
	public GraphChartFeed gcf;
	public ExerDatabaseCsv EDC;
	public TextMeshProUGUI velocityAverageTxt;
	void Start()
	{

	
	//    Invoke("readData",1);
		

	}

    // Read data from CSV file
  
    public void readData(string rawDataPath)
	{
		
		indexer = 0;
		if (File.Exists(rawDataPath))
		{
			string temptext = " ";
			temptext = File.ReadAllText(rawDataPath);
			csvFile.text.Insert(0, temptext);
		}
        else
        {
			return;
        }
		string[] records = csvFile.text.Split("\n"[0]);
		for (int i = 0; i < records.Length; i++)
		{
			// Filtring Values
			string[] temprecords = records[i].Split(","[0]);
			if (temprecords[0].Length > 0)
			{
				/*Vector3 FliteredValues = AOC.filterPos(new Vector3(float.Parse(temprecords[IndexX])+offsetX, float.Parse(temprecords[IndexY])+offsetY, float.Parse(temprecords[IndexZ])+offsetZ));*/
				Vector3 FliteredValues = new Vector3(float.Parse(temprecords[IndexX]), float.Parse(temprecords[IndexY]), float.Parse(temprecords[IndexZ]));
				// Getting Velocity
				previous = new Vector3(FliteredValues.x, FliteredValues.y, FliteredValues.z);
				float velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
				speeds.Add(velocity);
				this.transform.position = previous;
			}
			
			// Saving Value
		//	addData(FliteredValues.x.ToString(),FliteredValues.y.ToString(),FliteredValues.z.ToString(),velocity.ToString());
			
		}
		callafter();
	}

	public void callafter()
    {
		float tempAverage = returnAverage();
		velocityAverageTxt.text = tempAverage.ToString().Substring(0,5) +" m/s";
		
		bcf.addbarSingleValue(tempAverage);
		gcf.Singcall(tempAverage);
		EDC.addData(tempAverage.ToString());
		speeds.Clear();
		//StartCoroutine(bcf.addbarValue(speeds));
	}
	
	float returnAverage() //call when Finished
	{
		float averageTotal = 0;
		float finalTotal = 0;
		//BarChartFeed bcr = GameObject.FindObjectOfType<BarChartFeed>();
		for (int i = 0; i < speeds.Count; i++)
		{
			averageTotal += speeds[i];
			//bcr.barChart.DataSource.AddCategory("VEL "+i.ToString(), bcr.mat);
		//	bcr.barChart.DataSource.SetValue("VEL " + i.ToString(), "All", temp);
			//	PerVelocity.text += " " + i.ToString() + " " + speeds[i].ToString();
		}

		finalTotal = averageTotal / speeds.Count;
		 // so as the list is free for the next time
	    return finalTotal;
	
	}
	

	public void addData(string X,string Y, string Z, string vel)
	{

      //  Following line adds data to CSV file

        if (indexer > 0)
        {
            File.AppendAllText(getPath() + "/Resources/" + saveFilename + ".csv", lineSeperater + X + fieldSeperator + Y + fieldSeperator + Z + fieldSeperator + vel);
        }
        else
        {
            File.AppendAllText(getPath() + "/Resources/"+ saveFilename + ".csv", X + fieldSeperator + Y + fieldSeperator + Z + fieldSeperator + vel);
			indexer += 1;
		}
	
	}

	
	private static string getPath()
	{
#if UNITY_EDITOR
		return Application.dataPath;
#elif UNITY_ANDROID
		return Application.persistentDataPath;// +fileName;
#elif UNITY_IPHONE
		return GetiPhoneDocumentsPath();// +"/"+fileName;
#else
		return Application.dataPath;// +"/"+ fileName;
#endif
	}
	// Get the path in iOS device
	private static string GetiPhoneDocumentsPath()
	{
		string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
		path = path.Substring(0, path.LastIndexOf('/'));
		return path + "/Documents";
	}
}
