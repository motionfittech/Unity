using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class CSVManager : MonoBehaviour
{
	public TextAsset csvFile; 
	private char lineSeperater = '\n'; // It defines line seperate character
	private char fieldSeperator = ','; // It defines field seperate chracter
	private int indexer = 0;
	public AccelerometerObjectControl AOC;
	public string saveFilename = "Default";
    Vector3 previous ;
    [Header("Start Value of Every CSV acceleration is a Offset")]
	public float offsetX = 35, offsetY = 50, offsetZ = -17;


	[Header("Acceleration Value in CSV Index")]
	public int IndexX = 0, IndexY = 1, IndexZ = 2 ;

	public float totalVelocity = 0;
	void Start()
	{

		
	    Invoke("readData",1);
		

	}

	// Read data from CSV file
	private void readData()
	{
		
		indexer = 0;
		string[] records = csvFile.text.Split("\n"[0]);
		for (int i = 0; i < records.Length; i++)
		{
			// Filtring Values
			string[] temprecords = records[i].Split(","[0]);	
		    Vector3 FliteredValues = AOC.filterPos(new Vector3(float.Parse(temprecords[IndexX])+offsetX, float.Parse(temprecords[IndexY])+offsetY, float.Parse(temprecords[IndexZ])+offsetZ));

			// Getting Velocity
			previous = new Vector3(FliteredValues.x/offsetX, FliteredValues.y/offsetY, FliteredValues.z/offsetZ);
			var velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
			totalVelocity += velocity;
			this.transform.position = previous;

			// Saving Value
			addData(FliteredValues.x.ToString(),FliteredValues.y.ToString(),FliteredValues.z.ToString(),velocity.ToString());
		}


		
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
