using UnityEngine;
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
	public List<Vector3> Quat = new List<Vector3>();
	public string saveFilename = "Default";
	public Transform IkPos;
    Vector3 previous ;
    float velocity;
	public float offsetX, offsetY, offsetZ;
	void Start()
	{

		//print("Reading will start in 5 Sec wait");
	    Invoke("readData",1);
		//InvokeRepeating("waitforCall",2,2);

	}

	// Read data from CSV file
	private void readData()
	{
		print("Reading");
		indexer = 0;
		string[] records = csvFile.text.Split("\n"[0]);
		for (int i = 0; i < records.Length; i++)
		{

			string[] temprecords = records[i].Split(","[0]);

			//addData(temprecords[5], temprecords[6], temprecords[7], "1");
			//print(temprecords[5] +"  "+ temprecords[6] + "  " + temprecords[7]);
		Vector3 FliteredValues = AOC.filterPos(new Vector3(float.Parse(temprecords[0])+offsetX, float.Parse(temprecords[1])+offsetY, float.Parse(temprecords[2])+offsetZ));
				//	print(float.Parse(temprecords[5])+" "+ float.Parse(temprecords[6]) + " " + float.Parse(temprecords[7]) + " " + float.Parse(temprecords[8]));
				//Vector3 tempQuat = new Vector3(float.Parse(temprecords[1]),float.Parse(temprecords[2]),float.Parse(temprecords[3]));

			//	this.transform.rotation = new Quaternion(float.Parse(temprecords[5]), float.Parse(temprecords[6]), float.Parse(temprecords[7]), float.Parse(temprecords[8]));
			Quat.Add(FliteredValues);
			
		//		addData(FliteredValues.x.ToString(),FliteredValues.y.ToString(),FliteredValues.z.ToString(), indexer.ToString());
			
          
		}
		StartCoroutine(ReadingEnd());
		//if (Quat.Count > 0)
		//{
		//	this.transform.position = new Vector3(Quat[0].x / 245, Quat[0].y / -1266, Quat[0].z / 328);
		//	StartCoroutine(callrot());
		//}

		//StartCoroutine(addValue());
	}
	public IEnumerator ReadingEnd()
	{
		while (Quat.Count <= 0)
		{
			yield return new WaitForSeconds(0.5f);
		}
		StartCoroutine(PlayingEnd());

	}
	public IEnumerator PlayingEnd()
	{
		int counter = 0;
		while (Quat.Count-1 != counter)
		{
			IkPos.localPosition = Quat[counter];
		
			counter += 1;
			yield return new WaitForSeconds(0.01f);
		}
	//	IkPos.localPosition = Quat[0];
	//	print("ReadingEnd");


	}
	public IEnumerator callrot()
    {
		int temp = Quat.Count;
		int counter = 0;
		while(temp > 0)
        {
			previous = new Vector3(Quat[counter].x/ 245, Quat[counter].y/ -1266, Quat[counter].z/ 328);
			var velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
			this.transform.position = previous;
			print(velocity);
		//	saveVelocityRaw(velocity.ToString());
			//this.transform.position = Quat[counter];
			
			yield return new WaitForSeconds(0.1f);
			temp -= 1;
			counter += 1;
		}


    }

	public void waitforCall()
    {
		//addData(indexer.ToString(),"1a","1b","1c");
		//indexer += 1;
    }
	// Add data to CSV file
	


	public void saveVelocityRaw(string velocity)
    {
		File.AppendAllText(getPath() + "/Resources/VelocityExerciseData.csv", lineSeperater + velocity);
	}

	public void addData(string X,string Y, string Z, string rep)
	{

      //  Following line adds data to CSV file

        if (indexer > 0)
        {
            File.AppendAllText(getPath() + "/Resources/" + saveFilename + ".csv", lineSeperater + X + fieldSeperator + Y + fieldSeperator + Z + fieldSeperator + rep);
        }
        else
        {
            File.AppendAllText(getPath() + "/Resources/"+ saveFilename + ".csv", X + fieldSeperator + Y + fieldSeperator + Z + fieldSeperator + rep);
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
