﻿using UnityEngine;
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
	public AccelerometerObjectControl AOC;
	public string saveFilename = "Default";
	[Header("Acceleration Value in CSV Index")]

	public List<string> a = new List<string>();
	public int IndexX = 0;
	public int IndexY = 1;
	public int IndexZ = 2 ;


	public EquationData ED;
	public string csvName;

	//New
	float VIx;
	float VIy;
	float VIz;
	List<float> AccelerationPointSx = new List<float>();
	List<float> AccelerationPointSy = new List<float>();
	List<float> AccelerationPointSz = new List<float>();
	List<float> AccelerationRotationSx = new List<float>();
	List<float> AccelerationRotationSy = new List<float>();
	List<float> AccelerationRotationSz = new List<float>();
	List<float> VelocityPointSx = new List<float>();
	List<float> VelocityPointSy = new List<float>();
	List<float> VelocityPointSz = new List<float>();
	float totalVelocityX;
	float totalVelocityY;
	float totalVelocityZ;
	List<float> SumofVelocity = new List<float>();
	float TotalPerX;
	float TotalPerY;
	float TotalPerZ;
	public float ParameterX = 20;
	public float ParameterY = -30;
	public float Parameterz = 50;
	float TotalDeviationX;
	float TotalDeviationY;
	float TotalDeviationZ;
	public Text gameversion;
	public TextMeshProUGUI exerciseform;
	public TextMeshProUGUI fatiguetxt;
	public TextMeshProUGUI muscleBalancetxt;
	public Toggle _istimeon;
	private void Start()
	{
    //	Invoke("call", 2);

		gameversion.text = Application.version;
	}

	public void call()
	{
		//PlayerPrefs.SetString("path", "C:/Users/asus/Downloads/Log Folder 2/AndrewDeadlift20reps.csv");
		//	NewreadData("C:/Users/asus/Downloads/Log Folder 2/"+csvName+".csv", false);
		//NewreadDataCSV(csvFile,true);
	}
	//	public void readData(string rawDataPath, bool _isSaving)
	//	{


	//		if (rawDataPath.Length == 0)
	//			return;

	//		indexer = 0;
	//		Vector3 previous = Vector3.zero;
	//		Vector3 current = Vector3.zero;
	//		Vector3 firstvalue = Vector3.zero;
	//		List<float> speeds = new List<float>();
	//		if (File.Exists(rawDataPath))
	//		{

	//			string temptext = File.ReadAllText(rawDataPath);
	//			string[] records = temptext.Split("\n"[0]);

	//			for (int i = 0; i < records.Length; i++)
	//			{

	//				string[] temprecords = records[i].Split(","[0]);
	//				if (temprecords[0].Length > 0)
	//				{

	//					Vector3 FliteredValues = new Vector3(float.Parse(temprecords[IndexX]), float.Parse(temprecords[IndexY]), float.Parse(temprecords[IndexZ]));
	//					Vector3 FliteredValues2 = new Vector3(float.Parse(temprecords[3]), float.Parse(temprecords[4]), float.Parse(temprecords[5]));
	//					current = FliteredValues;

	//					if (i > 0)
	//					{
	//						current = current - firstvalue;

	//						float velocity = Vector3.Distance(previous, current);
	//						float tempvelocity = velocity / i * 0.1f;

	//						speeds.Add(Mathf.Abs(tempvelocity));
	//					}
	//					else
	//					{
	//						firstvalue = current;
	//					}

	//					previous = current;


	//				}


	//			}


	//#if UNITY_EDITOR
	//			UnityEditor.AssetDatabase.Refresh();
	//#endif
	//			ED.callafter(speeds, true);
	//		}

	//	}

	// ReadData from path new one

	 IEnumerator readData( string [] records)
    {
		int i = 0;
		
		while (i < records.Length)
        {
			
			string[] temprecords = records[i].Split(","[0]);
			//print("DATA = " + records[i]);
			if (temprecords.Length > 2)
			{
				//	print("DATA = 2 " + temprecords.Length);
				Vector3 CsvPoints = new Vector3(float.Parse(temprecords[0]) / 1000.9f, float.Parse(temprecords[1]) / 1000.9f, float.Parse(temprecords[2]) / 1000.9f);
				Vector3 CsvPoints2 = new Vector3(float.Parse(temprecords[3]) / 10000.9f, float.Parse(temprecords[4]) / 10000.9f, float.Parse(temprecords[5]) / 10000.9f);
				//addData(CsvPoints[0].ToString(), CsvPoints[1].ToString(), CsvPoints[2].ToString(), CsvPoints2[0].ToString(), CsvPoints2[1].ToString(), CsvPoints2[2].ToString());
				//	Vector3 FliteredValues2 = new Vector3(float.Parse(temprecords[3]), float.Parse(temprecords[4]), float.Parse(temprecords[5]));
				//	print("DATA = 3 " + CsvPoints);
				if (i > 0)
				{
					//	print("DATA = 4 " + i);
				}
				else
				{
					// VI

					VIx = CsvPoints.x;
					VIy = CsvPoints.y;
					VIz = CsvPoints.z;
					ParameterX = Mathf.Abs(CsvPoints.x);
					ParameterY = Mathf.Abs(CsvPoints.y);
					//	print("DATA = 5 " + VIx);
					Parameterz = Mathf.Abs(CsvPoints.z);
				}

				//print("DATA = 6 ");
				//print("DATA = 6.1 " + CsvPoints);
				AccelerationPointSx.Add(CsvPoints.x);
				AccelerationPointSy.Add(CsvPoints.y);
				AccelerationPointSz.Add(CsvPoints.z);
				AccelerationRotationSx.Add(CsvPoints2.x);
				AccelerationRotationSy.Add(CsvPoints2.y);
				AccelerationRotationSz.Add(CsvPoints2.z);
				//print("DATA = 7 " + AccelerationPointSx);
			}
			else
			{
				//	print("current row is empty " + i);
			}
			i++;
			yield return null;
        }

		StartCoroutine(getAcceleration());
    }
	IEnumerator getAcceleration()
    {
		int i = 0;
		
		while (i < AccelerationPointSx.Count)
        {
			
				//Velocity Equation
				float VelocityPointCalculationx = 0;
				if (_istimeon.isOn)
				{
					VelocityPointCalculationx = (AccelerationPointSx[i] - VIx) / Time.deltaTime;
				}
				else
				{
					VelocityPointCalculationx = (AccelerationPointSx[i] - VIx);

				}
				VIx = AccelerationPointSx[i];

				totalVelocityX += VelocityPointCalculationx;
				VelocityPointSx.Add(VelocityPointCalculationx);


			
				//Velocity Equation
				float VelocityPointCalculationy = 0;
				if (_istimeon.isOn)
				{
					VelocityPointCalculationy = (AccelerationPointSy[i] - VIy) / Time.deltaTime;
				}
				else
				{
					VelocityPointCalculationy = (AccelerationPointSy[i] - VIy);
				}
				VIy = AccelerationPointSy[i];

				totalVelocityY += VelocityPointCalculationy;
				VelocityPointSy.Add(VelocityPointCalculationy);


			
			
				//Velocity Equation
				float VelocityPointCalculationz = 0;
				if (_istimeon.isOn)
				{
					VelocityPointCalculationz = (AccelerationPointSz[i] - VIz) / Time.deltaTime;
				}
				else
				{
					VelocityPointCalculationz = (AccelerationPointSz[i] - VIz);
				}
				VIz = AccelerationPointSz[i];

				totalVelocityZ += VelocityPointCalculationz;
				VelocityPointSz.Add(VelocityPointCalculationz);
			i++;

			
			yield return null;
        }
		StartCoroutine(getPercantage());
	}

	IEnumerator getPercantage() {

		int i = 0;
		indexer = 0;
		Vector3 previous = Vector3.zero;
		Vector3 current = Vector3.zero;
		Vector3 firstvalue = Vector3.zero;
		List<float> speeds = new List<float>();
		float sum = totalVelocityX + totalVelocityY + totalVelocityZ;
		SumofVelocity.Add(sum);
		while (i < VelocityPointSx.Count)
		{
			
				float ParcantageCalculationx = VelocityPointSx[i] / sum;
				TotalPerX += ParcantageCalculationx;
		
				float ParcantageCalculationy = VelocityPointSy[i] / sum;
				TotalPerY += ParcantageCalculationy;
		
				float ParcantageCalculationz = VelocityPointSz[i] / sum;
				TotalPerZ += ParcantageCalculationz;

			i++;
			yield return null;
		}
		
		print("Percantage of X = " + TotalPerX * 100 + "%");
		print("Percantage of Y = " + TotalPerY * 100 + "%");
		print("Percantage of Z = " + TotalPerZ * 100 + "%");

		TotalDeviationX = (TotalPerX * 100) - ParameterX;
		TotalDeviationY = (TotalPerY * 100) - ParameterY;
		TotalDeviationZ = (TotalPerZ * 100) - Parameterz;
		float TotalSumDeviation = TotalDeviationX + TotalDeviationY + TotalDeviationZ;
		
		float Exerciseform = 100 - TotalSumDeviation;
		
		exerciseform.text = Exerciseform.ToString() + "%";
		for (int c = 0; c < SumofVelocity.Count; c++)
		{
		
			speeds.Add(SumofVelocity[c]);
			muscleBalance(SumofVelocity[c]);
			fatigue(SumofVelocity[c]);
		}

		ED.callafter(speeds, true);

		StartCoroutine(uploaddata());


	}



	public void NewreadData(string rawDataPath)
	{


		if (rawDataPath.Length == 0)
			return;

		
		if (File.Exists(rawDataPath))
		{

			string temptext = File.ReadAllText(rawDataPath);
			string[] records = temptext.Split("\n"[0]);
			StartCoroutine(readData(records));
			
			
		}
	}
	IEnumerator uploaddata()
	{
		LocalDatabase.MyClass temp = new LocalDatabase.MyClass();
		temp.counter = AccelerationPointSx.Count;
		temp.setArray();
		int indexer = 0;
		while (temp.intArray.Length > indexer)
		{
			temp.intArray[indexer] = AccelerationPointSx[indexer].ToString() + "," + AccelerationPointSy[indexer].ToString() + "," + AccelerationPointSz[indexer].ToString() +","+ AccelerationRotationSx[indexer].ToString() + "," + AccelerationRotationSy[indexer].ToString() + "," + AccelerationRotationSz[indexer].ToString();
			indexer += 1;
			print("uploading data.....");
			yield return new WaitForSeconds(0.01f);
		}
		AccelerationPointSx = new List<float>();
		AccelerationPointSy = new List<float>();
		AccelerationPointSz = new List<float>();
		AccelerationRotationSx = new List<float>();
		AccelerationRotationSy = new List<float>();
		AccelerationRotationSz = new List<float>();
		int temp1 = int.Parse(PlayerPrefs.GetString("csvCounter", "0"));
		temp1 += 1;
		LocalDatabase.instance.savcsvcounter(temp1.ToString());
		LocalDatabase.instance.saveExerciseData(temp);
//		print("uploading DONE.");
	}

	public void fatigue(float Csv2)
    {
		float Csv1 = PlayerPrefs.GetFloat("previousCSV",0);

		float Subfatigue = (Csv1 - Csv2);
		print(Subfatigue);
		float Getfatigue = 0;
		if (Csv1 != 0)
		{
			float VelocityLoss = (Subfatigue / Csv1);
			
			Getfatigue = VelocityLoss / 4;
			
		}
       

		fatiguetxt.text = Getfatigue.ToString()+ "/10";

		PlayerPrefs.SetFloat("previousCSV",Csv2);
    }
	public void muscleBalance(float Csv2)
	{
		float Csv1 = PlayerPrefs.GetFloat("previousCSV", 0);
		muscleBalancetxt.text = "Right " + Csv1.ToString() + " %" + " Left" + Csv2.ToString() + " %";
	}

	public void NewreadDataCSV(TextAsset rawDataPath, bool _isSaving)
	{


	//	if (rawDataPath.Length == 0)
		//	return;

		indexer = 0;
		Vector3 previous = Vector3.zero;
		Vector3 current = Vector3.zero;
		Vector3 firstvalue = Vector3.zero;
		List<float> speeds = new List<float>();
	//	if (File.Exists(rawDataPath))
	//	{

			//string temptext = File.ReadAllText(rawDataPath);
			string[] records = rawDataPath.text.Split(lineSeperater);

		for (int i = 0; i < records.Length; i++)
			{

				string[] temprecords = records[i].Split(","[0]);
				//print("DATA = " + records[i]);
				if (temprecords.Length > 2)
				{
				//	print("DATA = 2 " + temprecords.Length);
				Vector3 CsvPoints = new Vector3(float.Parse(temprecords[0]) / 1000.9f, float.Parse(temprecords[1]) / 1000.9f, float.Parse(temprecords[2]) / 100000.9f);
				Vector3 CsvPoints2 = new Vector3(float.Parse(temprecords[3]) / 1000.9f, float.Parse(temprecords[4]) / 1000.9f, float.Parse(temprecords[5]) / 100000.9f);
//				addData(CsvPoints[0].ToString(), CsvPoints[1].ToString(), CsvPoints[2].ToString(), CsvPoints2[0].ToString(), CsvPoints2[1].ToString(), CsvPoints2[2].ToString());
				//	Vector3 FliteredValues2 = new Vector3(float.Parse(temprecords[3]), float.Parse(temprecords[4]), float.Parse(temprecords[5]));
				//	print("DATA = 3 " + CsvPoints);
				if (i > 0)
					{
					//	print("DATA = 4 " + i);
					}
					else
					{
						// VI

						VIx = CsvPoints.x;
						VIy = CsvPoints.y;
						VIz = CsvPoints.z;
					ParameterX = Mathf.Abs(CsvPoints.x);
					ParameterY = Mathf.Abs(CsvPoints.y);
					//	print("DATA = 5 " + VIx);
					Parameterz= Mathf.Abs(CsvPoints.z);
				}

					//print("DATA = 6 ");
					//print("DATA = 6.1 " + CsvPoints);
					AccelerationPointSx.Add(CsvPoints.x);
					AccelerationPointSy.Add(CsvPoints.y);
					AccelerationPointSz.Add(CsvPoints.z);
					//print("DATA = 7 " + AccelerationPointSx);
				}
				else
				{
				//	print("current row is empty " + i);
				}


			}

			for (int i = 0; i < AccelerationPointSx.Count; i++)
			{
				//Velocity Equation
				float VelocityPointCalculation = (AccelerationPointSx[i] - VIx) / Time.deltaTime;
				VIx = AccelerationPointSx[i];

				totalVelocityX += VelocityPointCalculation;
				VelocityPointSx.Add(VelocityPointCalculation);


			}
			//Y
			for (int i = 0; i < AccelerationPointSy.Count; i++)
			{
				//Velocity Equation
				float VelocityPointCalculation = (AccelerationPointSy[i] - VIy) / Time.deltaTime;
				VIy = AccelerationPointSy[i];

				totalVelocityY += VelocityPointCalculation;
				VelocityPointSy.Add(VelocityPointCalculation);


			}
			//Z
			for (int i = 0; i < AccelerationPointSz.Count; i++)
			{
				//Velocity Equation
				float VelocityPointCalculation = (AccelerationPointSz[i] - VIz) / Time.deltaTime;
				VIz = AccelerationPointSz[i];

				totalVelocityZ += VelocityPointCalculation;
				VelocityPointSz.Add(VelocityPointCalculation);


			}
			// Sum of Velocity
			float sum = totalVelocityX + totalVelocityY + totalVelocityZ;
			SumofVelocity.Add(sum);
			print("Sum of Velocity " + sum);

			for (int i = 0; i < VelocityPointSx.Count; i++)
			{
				float ParcantageCalculation = VelocityPointSx[i] / sum;
				TotalPerX += ParcantageCalculation;

			}

			for (int i = 0; i < VelocityPointSy.Count; i++)
			{
				float ParcantageCalculation = VelocityPointSy[i] / sum;
				TotalPerY += ParcantageCalculation;

			}

			for (int i = 0; i < VelocityPointSz.Count; i++)
			{
				float ParcantageCalculation = VelocityPointSz[i] / sum;
				TotalPerZ += ParcantageCalculation;

			}



			print("Percantage of X = " + TotalPerX * 100 + "%");
			print("Percantage of Y = " + TotalPerY * 100 + "%");
			print("Percantage of Z = " + TotalPerZ * 100 + "%");

			TotalDeviationX = (TotalPerX * 100) - ParameterX;
			TotalDeviationY = (TotalPerY * 100) - ParameterY;
			TotalDeviationZ = (TotalPerZ * 100) - Parameterz;
			float TotalSumDeviation = TotalDeviationX + TotalDeviationY + TotalDeviationZ;
			print("Total Deviation on X " + TotalDeviationX + "%");
			print("Total Deviation on Y " + TotalDeviationY + "%");
			print("Total Deviation on Z " + TotalDeviationZ + "%");
			print("Total Sum of Deviation " + TotalSumDeviation + "%");
			float Exerciseform = 100 - TotalSumDeviation;
			print("Exercise Form " + Exerciseform + "%");
		exerciseform.text = Exerciseform.ToString() + "%";
			for (int c = 0; c < SumofVelocity.Count; c++)
			{
			print(SumofVelocity[c]);
				speeds.Add(SumofVelocity[c]);
			}

			ED.callafter(speeds, true);
	//	GameObject.FindObjectOfType<FirebaseStorageHandler>().uploadData(LocalDatabase.instance.UID.ToString(), getPath() + "/Resources/" + saveFilename + ".csv");
		//}
		//else
		//{
		//	print("path is null");
		//}

	}

//	public void incrementList(string b)
//    {
//		a.Add(b);
//    }


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
