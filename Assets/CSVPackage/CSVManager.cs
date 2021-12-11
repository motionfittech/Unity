using UnityEngine;
using System.IO;
using System.Collections.Generic;
public class CSVManager : MonoBehaviour
{
	
	private char lineSeperater = '\n'; // It defines line seperate character
	private char fieldSeperator = ','; // It defines field seperate chracter
	private int indexer = 0;
	public AccelerometerObjectControl AOC;
	public string saveFilename = "Default";
	[Header("Acceleration Value in CSV Index")]

	public List<string> a = new List<string>();
	public int IndexX = 0, IndexY = 1, IndexZ = 2 ;
	
	public string csvName;

	public EquationData ED;

	// new work
	List<Vector3> PointsLinearVelocity = new List<Vector3>();
	List<Vector3> PointsAngularVelocity = new List<Vector3>();
	Vector3 pointLinearVelocity;
	Vector3 pointAngularVelocity;
	List<Vector3> LinearVelocity = new List<Vector3>();
	List<Vector3> AngularVelocity = new List<Vector3>();
	private void Start()
	{
		Invoke("call", 2);
	}

	public void call()
	{
		PlayerPrefs.SetString("path", "C:/Users/asus/Downloads/Log Folder 2/AndrewBicepCurl23Reps.csv");
		NewreadData(PlayerPrefs.GetString("path", ""), false);
	}
	public void readData(string rawDataPath, bool _isSaving)
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

					if (i > 0)
					{
						current = current - firstvalue;

						float velocity = Vector3.Distance(previous, current);
						float tempvelocity = velocity / i * 0.1f;

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
			ED.callafter(speeds, true);
		}

	}

	public void NewreadData(string rawDataPath, bool _isSaving)
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
					if (i > 0)
					{
						PointsLinearVelocity.Add(FliteredValues);
						PointsAngularVelocity.Add(FliteredValues2);
					
					}
                    else
                    {
						pointLinearVelocity = FliteredValues;
						pointAngularVelocity = FliteredValues2;
                    }
					


				}


			}


#if UNITY_EDITOR
			UnityEditor.AssetDatabase.Refresh();
#endif
            foreach (var temp in PointsLinearVelocity)
            {
				var calculator = (temp - pointLinearVelocity) / Time.deltaTime;
				pointLinearVelocity = temp;
			
				LinearVelocity.Add(AOC.filterPos(calculator));
				
            }
			foreach (var temp in PointsAngularVelocity)
			{
				var calculator = (temp - pointAngularVelocity) / Time.deltaTime;
				pointAngularVelocity = temp;
				AngularVelocity.Add(AOC.filterPos(calculator));
				
			}
			Vector3 totalpercangtage = Vector3.zero;
			foreach(var getfilter in LinearVelocity)
            {
				print(getfilter);
				totalpercangtage += getfilter*3;
				
            }

			Vector3 getParcantage = totalpercangtage/100;
			print(Mathf.Abs(getParcantage.x) +"% "+ Mathf.Abs(getParcantage.y) + "% "+ Mathf.Abs(getParcantage.z) + "%");
			//ED.callafter(speeds, true);
		}

	}
	public void incrementList(string b)
    {
		a.Add(b);
    }


	public List<string> pushdata(List<string> temp)
    {
		return temp;
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
