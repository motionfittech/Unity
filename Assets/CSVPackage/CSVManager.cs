using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CSVManager : MonoBehaviour
{
	public TextAsset csvFile;
	private char lineSeperater = '\n'; // It defines line seperate character
	private char fieldSeperator = ','; // It defines field seperate chracter
	private int indexer = 0;
//	public AccelerometerObjectControl AOC;
	public string saveFilename = "Default";
	[Header("Acceleration Value in CSV Index")]

	//public List<string> a = new List<string>();
	//public int IndexX = 0;
	//public int IndexY = 1;
	//public int IndexZ = 2 ;


	//public EquationData ED;
	//public string csvName;
	////New inistal velocity
	//float VIx = 0;
	//float VIy = 0;
	//float VIz = 0;
	List<float> AccelerationPointSx = new List<float>();
	List<float> AccelerationPointSy = new List<float>();
	List<float> AccelerationPointSz = new List<float>();
	List<float> AccelerationRotationSx = new List<float>();
	List<float> AccelerationRotationSy = new List<float>();
	List<float> AccelerationRotationSz = new List<float>();
	// List<float> VelocityPointSx = new List<float>();
	// List<float> VelocityPointSy = new List<float>();
	// List<float> VelocityPointSz = new List<float>();
	//float totalVelocityX;
	//float totalVelocityY;
	//float totalVelocityZ;
	//List<float> SumofVelocity = new List<float>();
	//float TotalPerX;
	//float TotalPerY;
	//float TotalPerZ;
	//public float ParameterX = 20;
	//public float ParameterY = -30;
	//public float Parameterz = 50;
	//float TotalDeviationX;
	//float TotalDeviationY;
	//float TotalDeviationZ;
	//public Text gameversion;
	//public TextMeshProUGUI exerciseform;


	public GameObject Loadingscreen;
//	public TextMeshProUGUI UpdataLoadingtxt;
	public TextMeshProUGUI ExerciseTxt;



	private void Start()
	{
		// 	Invoke("call", 2);

		//	gameversion.text = Application.version;
		LocalDatabase.instance.Loadcsvcounter();
	}

	public void call()
	{
		//PlayerPrefs.SetString("path", "C:/Users/asus/Downloads/Log Folder 2/AndrewBenchPress_1.csv");
		//	NewreadData("C:/Users/asus/Downloads/Log Folder 2/"+csvName+".csv", false);
		//NewreadDataCSV(csvFile,true);
	}

	public void NewreadData(string rawDataPath, bool _isSaving)
	{


		if (rawDataPath.Length == 0)
			return;

		
		if (File.Exists(rawDataPath))
		{

			string temptext = File.ReadAllText(rawDataPath);
			string[] records = temptext.Split("\n"[0]);
			calculateAllData(records);

	    }
	}

	public void NewreadDataCSV(TextAsset rawDataPath, bool _isSaving)
	{
		string[] records = rawDataPath.text.Split(lineSeperater);

		if (records.Length == 0)
			return;
		calculateAllData(records);
	}
	

	public void calculateAllData(string [] records)
    {
		indexer = 0;
		Vector3 previous = Vector3.zero;
		Vector3 current = Vector3.zero;
		Vector3 firstvalue = Vector3.zero;
		List<float> speeds = new List<float>();
		for (int i = 0; i < records.Length; i++)
		{

			string[] temprecords = records[i].Split(","[0]);
			
			if (temprecords.Length > 2)
			{
				
				Vector3 CsvPoints = new Vector3(float.Parse(temprecords[0]), float.Parse(temprecords[1]), float.Parse(temprecords[2]));
				Vector3 CsvPoints2 = new Vector3(float.Parse(temprecords[3]), float.Parse(temprecords[4]) , float.Parse(temprecords[5]));
				

				Loadingscreen.SetActive(true);
			//	UpdataLoadingtxt.gameObject.SetActive(true);
			//	UpdataLoadingtxt.text = "Getting Data please wait , " + i.ToString();

                AccelerationPointSx.Add(CsvPoints.x);
                AccelerationPointSy.Add(CsvPoints.y);
                AccelerationPointSz.Add(CsvPoints.z);
                AccelerationRotationSx.Add(CsvPoints2.x);
                AccelerationRotationSy.Add(CsvPoints2.y);
                AccelerationRotationSz.Add(CsvPoints2.z);

            }
			else
			{
					print("current row is empty " + i);
			}


		}

	//	for (int i = 0; i < AccelerationPointSx.Count; i++)
	//	{
	//		//Velocity Equation // i is time here
	//		float VelocityPointCalculation = VIx+(i/10)* AccelerationPointSx[i];
	//		VIx = VelocityPointCalculation;

	//		totalVelocityX += VelocityPointCalculation;
	//		VelocityPointSx.Add(VelocityPointCalculation);


	//	}
		//Y
	//	for (int i = 0; i < AccelerationPointSy.Count; i++)
	//	{
			//Velocity Equation
	//		float VelocityPointCalculation = VIy + (i / 10) * AccelerationPointSy[i];
	//		VIy = VelocityPointCalculation;

	//		totalVelocityY += VelocityPointCalculation;
	//		VelocityPointSy.Add(VelocityPointCalculation);


	//	}
		//Z
	//	for (int i = 0; i < AccelerationPointSz.Count; i++)
	//	{
			//Velocity Equation
	//		float VelocityPointCalculation = VIz + (i / 10) * AccelerationPointSz[i];
	//		VIz = VelocityPointCalculation;

	//		totalVelocityZ += VelocityPointCalculation;
	//		VelocityPointSz.Add(VelocityPointCalculation);


	//	}
		// Sum of Velocity
	//	float sum = (totalVelocityX + totalVelocityY + totalVelocityZ)/2;
	//	SumofVelocity.Add(sum);
	//	print("Sum of Velocity " + sum);

	//	for (int i = 0; i < VelocityPointSx.Count; i++)
	//	{
	//		float ParcantageCalculation = VelocityPointSx[i] / sum;
	//		TotalPerX += ParcantageCalculation;

	//	}

		//for (int i = 0; i < VelocityPointSy.Count; i++)
		//{
		///	float ParcantageCalculation = VelocityPointSy[i] / sum;
		//	TotalPerY += ParcantageCalculation;

		//}

	///	for (int i = 0; i < VelocityPointSz.Count; i++)
	///	{
	//		float ParcantageCalculation = VelocityPointSz[i] / sum;
	//		TotalPerZ += ParcantageCalculation;

	//	}


//
	//	print("Percantage of X = " + TotalPerX * 100 + "%");
	//	print("Percantage of Y = " + TotalPerY * 100 + "%");
	//	print("Percantage of Z = " + TotalPerZ * 100 + "%");
//
	//	TotalDeviationX = (TotalPerX * 100) - ParameterX;
	//	TotalDeviationY = (TotalPerY * 100) - ParameterY;
	//	TotalDeviationZ = (TotalPerZ * 100) - Parameterz;
	//	float TotalSumDeviation = TotalDeviationX + TotalDeviationY + TotalDeviationZ;
	//	print("Total Deviation on X " + TotalDeviationX + "%");
	//	print("Total Deviation on Y " + TotalDeviationY + "%");
	//	print("Total Deviation on Z " + TotalDeviationZ + "%");
	//	print("Total Sum of Deviation " + TotalSumDeviation + "%");
	//	float Exerciseform = 100 - TotalSumDeviation;
	//	print("Exercise Form " + Exerciseform + "%");
	//	exerciseform.text = Exerciseform.ToString() + "%";
	//	for (int c = 0; c < SumofVelocity.Count; c++)
	//	{
	//		LocalDatabase.instance.saveVelocityData(SumofVelocity[c]);
	//		speeds.Add(SumofVelocity[c]);
	//	}

		//ED.callafter(speeds, true);
		StartCoroutine(uploaddata());       //}

	}

	IEnumerator uploaddata()
	{
		LocalDatabase.MyClass temp = new LocalDatabase.MyClass();
		temp.counter = AccelerationPointSx.Count;
		temp.setArray();
		int indexer = 0;
		while (temp.intArray.Length > indexer)
		{
			temp.intArray[indexer] = AccelerationPointSx[indexer].ToString() + "," + AccelerationPointSy[indexer].ToString() + "," + AccelerationPointSz[indexer].ToString() + "," + AccelerationRotationSx[indexer].ToString() + "," + AccelerationRotationSy[indexer].ToString() + "," + AccelerationRotationSz[indexer].ToString();
			indexer += 1;
			print("uploading data.....");
			Loadingscreen.SetActive(true);
//			UpdataLoadingtxt.text = "Uploading Data please wait , " + indexer.ToString();
			
			yield return new WaitForSeconds(0.01f);
		}
		AccelerationPointSx = new List<float>();
		AccelerationPointSy = new List<float>();
		AccelerationPointSz = new List<float>();
		AccelerationRotationSx = new List<float>();
		AccelerationRotationSy = new List<float>();
		AccelerationRotationSz = new List<float>();
		LocalDatabase.instance.saveExerciseData(temp);
		int temp1 = int.Parse(PlayerPrefs.GetString("csvCounter", "0"));
		temp1 += 1;
		PlayerPrefs.SetString("csvCounter", temp1.ToString());
		//print(temp1);
		//		LocalDatabase.instance.savcsvcounter(temp1.ToString());


		Loadingscreen.SetActive(false);
	//	UpdataLoadingtxt.gameObject.SetActive(false);
		print("uploading DONE.");
	}

	//public void incrementList(string b)
 //   {
	//	a.Add(b);
 //   }


//	public List<string> pushdata(List<string> temp)
//    {
//		return temp;
//    }

//	public void addData(string X,string Y, string Z,string GX,string GY, string GZ)
//	{

//      //  Following line adds data to CSV file

//        if (indexer > 0)
//        {
//            File.AppendAllText(getPath() + "/Resources/" + saveFilename + ".csv", lineSeperater + X + fieldSeperator + Y + fieldSeperator + Z + fieldSeperator + GX + fieldSeperator + GY + fieldSeperator + GZ);
//        }
//        else
//        {
//            File.AppendAllText(getPath() + "/Resources/"+ saveFilename + ".csv", X + fieldSeperator + Y + fieldSeperator + Z + fieldSeperator + GX + fieldSeperator + GY + fieldSeperator + GZ);
//			indexer += 1;
//		}
	
//	}

	
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