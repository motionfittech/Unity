using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using TMPro;
using ChartAndGraph;

public class ExerDatabaseCsv : MonoBehaviour
{
	private char lineSeperater = '\n'; // It defines line seperate character
	private char fieldSeperator = ','; // It defines field seperate chracter
	private EquationData ED;
	//public List<string> csvfiles = new List<string>();
	public List<float> GraphDataPoints = new List<float>();
	public List<float> datapoints = new List<float>();
    private void Start()
    {
//		ED = GetComponent<CSVManager>().ED;

		
		//csvfiles.Add(getPath() +"ExerciseData" +".csv");
		//csvfiles.Add(getPath() + "ForceData" + ".csv");
		//csvfiles.Add(getPath() + "PowerData" + ".csv");
		//csvfiles.Add(getPath() + "WorkData" + ".csv");
		//Invoke("runData",2);
	}

	public void runData()
    {
		//readData(0);
		//readData(1);
		//readData(2);
	}

    public void readData(int index)
	{
//		if (csvfiles[index].Length == 0)
//			return;

//		print("File exite");
//		datapoints = new List<float>(0);
////		ED.bcf.barChart.DataSource.ClearCategories();
		
//		ED.gcf.graph.DataSource.Clear();
//		if (File.Exists(csvfiles[index]))
//		{
//			string temptext = File.ReadAllText(csvfiles[index]);

//			string[] lastrecordArr = temptext.Split("\n"[0]);

//			if (lastrecordArr.Length == 0)
//				return;

//			string lastrecord = lastrecordArr[lastrecordArr.Length - 1];

//			string[] records = lastrecord.Split(","[0]);

//			for (int i = 0; i < records.Length; i++)
//			{

//				if (records[i].Length > 0)
//				{
//					float tempvalue = float.Parse(records[i]);
				
//					//	datapoints.Add(tempvalue);
//				//	ED.bcf.addbarSingleValue(tempvalue);
//					ED.gcf.Singcall(tempvalue);
//				}

//				if (i == records.Length - 1)
//				{
//					//print("called = "+datapoints.Count);


//					if (ED.velocityAverageTxt != null)
//					{
//						ED.velocityAverageTxt.text = records[0].ToString().Substring(0, 5) + " m/s";
//					}
//				}

//			}
//		}
//		//	ED.bcf.addbarValue(datapoints);
//		ED.gcf.Multicall(datapoints);


	//	ED.bcf.addbarSingleValue(GraphDataPoints[index]);
		//ED.gcf.Singcall(GraphDataPoints[index]);

	}


	public void addData(int index,string X)
	{

		
			//File.AppendAllText(csvfiles[index],  X + fieldSeperator);
       
	
	}

	
//	private static string getPath()
//	{
//#if UNITY_EDITOR
//		return Application.dataPath;
//#elif UNITY_ANDROID
//		return Application.persistentDataPath;// +fileName;
//#elif UNITY_IPHONE
//		return GetiPhoneDocumentsPath();// +"/"+fileName;
//#else
//		return Application.dataPath;// +"/"+ fileName;
//#endif
//	}
//	// Get the path in iOS device
//	private static string GetiPhoneDocumentsPath()
//	{
//		string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
//		path = path.Substring(0, path.LastIndexOf('/'));
//		return path + "/Documents";
//	}
}
