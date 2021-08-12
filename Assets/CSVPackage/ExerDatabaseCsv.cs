using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using TMPro;
public class ExerDatabaseCsv : MonoBehaviour
{
	public TextAsset csvFile;
	private char lineSeperater = '\n'; // It defines line seperate character
	private char fieldSeperator = ','; // It defines field seperate chracter
	
	public string saveFilename = "Default";
	public List<float> datapoints = new List<float>();
	void Start()
	{
		readData();
	}
	public void readData()
	{
		if (csvFile == null)
			return;

		string [] lastrecordArr = csvFile.text.Split("\n"[0]);

		if (lastrecordArr.Length == 0)
			return;

		string lastrecord = lastrecordArr[lastrecordArr.Length - 1];

		string[] records = lastrecord.Split(","[0]);
		for (int i = 0; i < records.Length; i++)
		{
			
			if (records[i].Length > 0)
			{
				
				datapoints.Add(float.Parse(records[i]));
			}
                      
			if(i == records.Length - 1)
            {
				//print("called = "+datapoints.Count);
				
			StartCoroutine(GetComponent<CSVManager>().bcf.addbarValue(datapoints));
				StartCoroutine(GetComponent<CSVManager>().gcf.Multicall(datapoints));

				if (GetComponent<CSVManager>().velocityAverageTxt != null)
				{
					GetComponent<CSVManager>().velocityAverageTxt.text = records[0].ToString().Substring(0, 5) + " m/s";
				}
			}
						
		}


		
	}


	public void addData(string X)
	{

     
            File.AppendAllText(getPath() + "/Resources/" + saveFilename + ".csv",  X + fieldSeperator);
       
	
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
