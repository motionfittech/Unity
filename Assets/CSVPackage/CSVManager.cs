﻿using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

public class CSVManager : MonoBehaviour
{
	
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
	private void Start()
	{
		//Invoke("call", 2);

		gameversion.text = Application.version;
	}

	public void call()
	{
		//PlayerPrefs.SetString("path", "C:/Users/asus/Downloads/Log Folder 2/AndrewDeadlift20reps.csv");
		NewreadData("C:/Users/asus/Downloads/Log Folder 2/"+csvName+".csv", false);
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
				print("DATA = "+records[i]);
				if (temprecords.Length > 2)
				{
					print("DATA = 2 " + temprecords.Length);
					Vector3 CsvPoints = new Vector3(float.Parse(temprecords[0]), float.Parse(temprecords[1]), float.Parse(temprecords[2]));
					//	Vector3 FliteredValues2 = new Vector3(float.Parse(temprecords[3]), float.Parse(temprecords[4]), float.Parse(temprecords[5]));
					print("DATA = 3 " + CsvPoints);
					if (i > 0)
					{
						print("DATA = 4 " + i);
					}
                    else
                    {
						// VI

						VIx = CsvPoints.x;
						VIy = CsvPoints.y;
						VIz = CsvPoints.z;
						print("DATA = 5 " + VIx);
					}

					print("DATA = 6 ");
					print("DATA = 6.1 " + CsvPoints);
					AccelerationPointSx.Add(CsvPoints.x);
						AccelerationPointSy.Add(CsvPoints.y);
						AccelerationPointSz.Add(CsvPoints.z);
					print("DATA = 7 " + AccelerationPointSx);
				}
                else
                {
					print("current row is empty "+ i);
                }


			}

			for(int i = 0; i < AccelerationPointSx.Count; i++)
            {
				//Velocity Equation
				float VelocityPointCalculation = (AccelerationPointSx[i] -VIx) / Time.deltaTime;
                VIx = AccelerationPointSx[i];
			
				totalVelocityX += VelocityPointCalculation;
				VelocityPointSx.Add(VelocityPointCalculation);
				

            }
			//Y
			for (int i = 0; i < AccelerationPointSy.Count; i++)
			{
				//Velocity Equation
				float VelocityPointCalculation = (AccelerationPointSy[i] -VIy) / Time.deltaTime;
				VIy = AccelerationPointSy[i];

				totalVelocityY += VelocityPointCalculation;
				VelocityPointSy.Add(VelocityPointCalculation);


			}
			//Z
			for (int i = 0; i < AccelerationPointSz.Count; i++)
			{
				//Velocity Equation
				float VelocityPointCalculation = (AccelerationPointSz[i] -VIz) / Time.deltaTime;
				VIz = AccelerationPointSz[i];

				totalVelocityZ += VelocityPointCalculation;
				VelocityPointSz.Add(VelocityPointCalculation);


			}
			 // Sum of Velocity
				float sum = totalVelocityX + totalVelocityY + totalVelocityZ;
			SumofVelocity.Add(sum);
			print("Sum of Velocity "+sum);
		
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



			print("Percantage of X = " + TotalPerX*100 + "%");
            print("Percantage of Y = " + TotalPerY*100 + "%");
			print("Percantage of Z = " + TotalPerZ*100 + "%");

			TotalDeviationX = (TotalPerX * 100) - ParameterX;
			TotalDeviationY = (TotalPerY * 100) - ParameterY;
			TotalDeviationZ = (TotalPerZ * 100) - Parameterz;
			float TotalSumDeviation = TotalDeviationX + TotalDeviationY + TotalDeviationZ;
			print("Total Deviation on X " + TotalDeviationX +"%");
			print("Total Deviation on Y " + TotalDeviationY + "%");
			print("Total Deviation on Z " + TotalDeviationZ + "%");
			print("Total Sum of Deviation " + TotalSumDeviation + "%");
			float Exerciseform = 100 - TotalSumDeviation;
			print("Exercise Form " + Exerciseform + "%");
			for(int c = 0; c< SumofVelocity.Count; c++)
            {
				speeds.Add(SumofVelocity[c]);
            }
			
			ED.callafter(speeds, true);
		}
        else
        {
			print("path is null");
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
