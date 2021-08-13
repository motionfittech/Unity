using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using TMPro;
public class CSVManager : MonoBehaviour
{
	
	private char lineSeperater = '\n'; // It defines line seperate character
	private char fieldSeperator = ','; // It defines field seperate chracter
	private int indexer = 0;
	public AccelerometerObjectControl AOC;
	public string saveFilename = "Default";


	[Header("Acceleration Value in CSV Index")]
	public int IndexX = 0, IndexY = 1, IndexZ = 2 ;
	
	public BarChartFeed bcf;
	public GraphChartFeed gcf;
	public ExerDatabaseCsv EDC;
	public TextMeshProUGUI velocityAverageTxt;
	public string csvName;



   /*   private void Start()
    {
		Invoke("call",2);
    }

    void call()
    {
        readData("Assets/CSVPackage/Log files/"+csvName+".csv");
    }*/
    public void readData(string rawDataPath)
	{
		
		indexer = 0;
		Vector3 previous = Vector3.zero;
		Vector3 current = Vector3.zero;
		Vector3 firstvalue = Vector3.zero;
		List<float> speeds = new List<float>();
		if (File.Exists(rawDataPath))
		{
			
			string temptext = File.ReadAllText(rawDataPath);
			string[] records = temptext.Split("\n"[0]);
			
			for (int i = 0; i < records.Length; i++)
			{
				
				string[] temprecords = records[i].Split(","[0]);
				if (temprecords[0].Length > 0)
				{
					
					Vector3 FliteredValues = new Vector3(float.Parse(temprecords[IndexX]), float.Parse(temprecords[IndexY]), float.Parse(temprecords[IndexZ]));
					Vector3 FliteredValues2 = new Vector3(float.Parse(temprecords[3]), float.Parse(temprecords[4]), float.Parse(temprecords[5]));
					current = FliteredValues;
				
					if (i > 0 ) {
						current = current - firstvalue;
						
						float velocity = Vector3.Distance(previous,current);
						float tempvelocity = velocity / i*0.01f;
					
						speeds.Add(Mathf.Abs(tempvelocity));
                    }
                    else
                    {
                        firstvalue = current;
                    }

                    previous = current;
					
				
				}

			
			}

			
#if UNITY_EDITOR
			UnityEditor.AssetDatabase.Refresh();
#endif
			callafter(speeds);
		}
     
	}
	
	public void callafter( List<float> speeds)
    {
		float tempAverage = returnAverage(speeds);
		velocityAverageTxt.text = tempAverage.ToString().Substring(0, 5 )+ " m/s";
		
		bcf.addbarSingleValue(tempAverage);
		gcf.Singcall(tempAverage);
		EDC.addData("ExerciseData", tempAverage.ToString());

		speeds.Clear();
		
	}
	
	float returnAverage(List<float> speeds) 
	{
		float averageTotal = 0;
		float finalTotal = 0;
		
		for (int i = 0; i < speeds.Count; i++)
		{
			averageTotal += speeds[i];
			
		}

		finalTotal = averageTotal / speeds.Count;
		
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
