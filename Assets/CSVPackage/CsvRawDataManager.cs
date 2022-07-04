using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class CsvRawDataManager : MonoBehaviour
{
    public TextAsset csvFile; // Reference of CSV file
   // public InputField rollNoInputField;// Reference of rollno input field
  //  public InputField nameInputField; // Reference of name input filed
   private string contentArea; // Reference of contentArea where records are displayed
    private string saveName = "Default";
    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter
    public List<Vector3> Nonfilterpos = new List<Vector3>();
    public List<Vector3> filterpos = new List<Vector3>();
    public List<Vector3> NonfilterGye = new List<Vector3>();
    public List<Vector3> filterGye = new List<Vector3>();
    public AccelerometerObjectControl AOC;
    void Start()
    {
      // Invoke("readData", 2);
         //  readData();
    }
    // Read data from CSV file
    public void readData()
    {
        string[] records = csvFile.text.Split(lineSeperater);
        foreach (string record in records)
        {
            
            string[] fields = record.Split(","[0]);
            if (fields.Length > 5)
            {
               Nonfilterpos.Add(new Vector3(float.Parse(fields[0]),float.Parse(fields[1]),float.Parse(fields[2])));
                NonfilterGye.Add(new Vector3(float.Parse(fields[3]), float.Parse(fields[4]), float.Parse(fields[5])));
            }
        //    print("LOOP End " + contentArea);
          //  contentArea += '\n';
        }

        applyFilter();
    }


    void applyFilter()
    {

        foreach (Vector3 temp in Nonfilterpos)
        {
         //   filterpos.Add(AOC.filterPos(temp));
           
        }
        foreach (Vector3 temp in NonfilterGye)
        {
           // filterGye.Add(AOC.filterPos(temp));

        }

        StartCoroutine(startMoving());
    }


    IEnumerator startMoving()
    {
        int indexer = 0;
        
        Vector3 previous = Vector3.zero;
        Vector3 current = Vector3.zero;
        List<float> tempspeed = new List<float>(0);
        while(indexer < filterpos.Count)
        {
//          
            current = filterpos[indexer];
            float velocity = ((current - previous).magnitude) / Time.deltaTime;
            tempspeed.Add(velocity);
            previous = current;
            indexer += 1;
             
            print("please wait "+ indexer +" total "+ filterpos.Count);
            yield return null;
            
        }

        print(returnAverage(tempspeed));
        
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

    // Add data to CSV file
    public void addData()
    {
        // Following line adds data to CSV file
        File.AppendAllText(getPath() + "/Assets/StudentData.csv", lineSeperater + saveName + fieldSeperator + saveName);
        // Following lines refresh the edotor and print data
     //   rollNoInputField.text = "";
      //  nameInputField.text = "";
        contentArea = "";
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
        readData();
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