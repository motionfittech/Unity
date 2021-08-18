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
	public TextMeshProUGUI velocityAverageTxt,ForceTxt,WorkTxt,PowerTxt,VelocityLossTxt;
	public string csvName;
	public int Totaltime;


    private void Start()
    {
        Invoke("call", 2);
    }

    void call()
    {
        readData(PlayerPrefs.GetString("path",""),false);
    }
    public void readData(string rawDataPath,bool _isSaving)
	{
		if (rawDataPath.Length == 0)
			return;

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
			callafter(speeds,_isSaving);
		}
     
	}
	
	public void callafter( List<float> speeds,bool _isDataSaving)
    {
		float tempAverage = returnAverage(speeds);
		float tempForce = returnForce(speeds,5);
		float tempWork = returnWork(speeds, tempForce);
		float tempPower = returnPower(tempWork,speeds.Count);
		velocityAverageTxt.text = tempAverage.ToString().Substring(0, 5 )+ " m/s";
		PowerTxt.text = tempPower.ToString().Substring(0, 6) + " P";
		ForceTxt.text = tempForce.ToString().Substring(0,6) + " N";
		WorkTxt.text = tempWork.ToString().Substring(0, 6) + " J";
		if (speeds.Count > 1)
		{
			float returnLoss = returnVelocityLoss(speeds.Count - 2, speeds.Count - 1);
			if (returnLoss <= 0)
			{
				returnLoss = 0;
			}
			print(returnLoss);
			VelocityLossTxt.text = returnLoss.ToString() + " m/s";
		}
		//bcf.addbarSingleValue(tempAverage);
		//	gcf.Singcall(tempAverage);

		if (_isDataSaving)
		{
			EDC.addData("ExerciseData", tempAverage.ToString());
			EDC.addData("ForceData", tempForce.ToString());
			EDC.addData("WorkData", tempWork.ToString());
			EDC.addData("PowerData", tempPower.ToString());
		}
		
		speeds.Clear();
		
	}
	
	float returnAverage(List<float> speeds) 
	{
		float averageTotal = 0;
		
		
		for (int i = 0; i < speeds.Count; i++)
		{
			averageTotal += speeds[i];
			
		}
		// Avelocity = TotalVelocity/TotalCount
		return averageTotal / speeds.Count;
	}
	float returnForce(List<float> speeds,float mass)
    {
		float Totalacceleration = 0;
		
		for (int i = 0; i < speeds.Count; i++)
		{
			Totalacceleration += speeds[i];

		}
		 // Force = Acceleration*Mass
		return Totalacceleration * mass;
	}

	float returnWork(List<float> finalVelocity,float Force)
    {

		float Vf = 0;
		for(int i = 0; i < finalVelocity.Count; i++)
        {
			Vf += finalVelocity[i];
        }
		 // Displacemnt = 1/2(vi+vf)*t
		float Displacement = (Vf/2);
		// Work = Force * Discplacement
		
		return Force * Displacement;


    }
	float returnPower(float work,float time)
    {
		return work / time;
    }
	float returnVelocityLoss(float SetA, float SetB)
    {
		return SetA - SetB;
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
