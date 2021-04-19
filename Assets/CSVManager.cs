﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class CSVManager : MonoBehaviour
{
	public TextAsset csvFile; // Reference of CSV file
	//public InputField rollNoInputField;// Reference of rollno input field
	//public InputField nameInputField; // Reference of name input filed
	//public Text contentArea; // Reference of contentArea where records are displayed

	private char lineSeperater = '\n'; // It defines line seperate character
	private char fieldSeperator = ','; // It defines field seperate chracter
	private int indexer = 0;
	public AccelerometerObjectControl AOC;
	void Start()
	{

		//print("Reading will start in 5 Sec wait");
	    Invoke("readData",1);
		//InvokeRepeating("waitforCall",2,2);

	}

	// Read data from CSV file
	private void readData()
	{
		indexer = 0;
		string[] records = csvFile.text.Split("\n"[0]);
		for (int i = 0; i < records.Length; i++)
		{

			string[] temprecords = records[i].Split(","[0]);
			if (temprecords.Length > 6)
			{
				//addData(temprecords[5], temprecords[6], temprecords[7], "1");
				Vector3 FliteredValues = AOC.filterPos(new Vector3(float.Parse(temprecords[5]), float.Parse(temprecords[6]), float.Parse(temprecords[7])));
				addData(FliteredValues.x.ToString(),FliteredValues.y.ToString(),FliteredValues.z.ToString(),"1");
			}
          
		}
		
		
		//StartCoroutine(addValue());
	}
	

	public void waitforCall()
    {
		//addData(indexer.ToString(),"1a","1b","1c");
		//indexer += 1;
    }
	// Add data to CSV file
	public void addData(string X,string Y, string Z, string rep)
	{

		// Following line adds data to CSV file
		if (indexer > 0)
		{
			File.AppendAllText(getPath() + "/Resources/ExerciseData.csv", lineSeperater + X + fieldSeperator + Y + fieldSeperator + Z + fieldSeperator + rep);
		}
        else
        {
			File.AppendAllText(getPath() + "/Resources/ExerciseData.csv", X + fieldSeperator + Y + fieldSeperator + Z + fieldSeperator + rep);
			indexer += 1;
		}
		// Following lines refresh the edotor and print data
	//	rollNoInputField.text = "";
	//	nameInputField.text = "";
//	//	contentArea.text = "";
//#if UNITY_EDITOR
//		UnityEditor.AssetDatabase.Refresh();
//#endif
//		readData();
	}

	// Get path for given CSV file
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
