
using UnityEngine;

using UnityEngine.Networking;

using System.Collections;

using System.IO;

using System;

using Newtonsoft.Json;


public class FortniteAPI : MonoBehaviour
{

    public TextAsset csvfile;
    private const string base_URL = "https://parmenides.balance3ds.com/api/";

    public object rep_data; // rep data object
    public byte[] scaled_data; // bytestring of scaled data

    private static string dir = Directory.GetCurrentDirectory();
    private static string filePath = dir + "/TestDir/test.csv";

    // Used to create the propper URL for API access to perform analysis
    private string get_analysis_URL(string exercise, int reps)
    {
        return base_URL + "analysis/" + exercise + "/" + reps.ToString();
    }

    // Used to generate the propper URL for API access to scale a file
    private string get_scaling_URL()
    {
        return base_URL + "scaling/";
    }

    // when game starts
    private void Awake()
    {
        
    }

    //When script starts
    private void Start()
    {
        GenerateRequest();
    }
    //Loop
    private void Update()
    {
        
    }

    public void GenerateRequest()
    {
        StartCoroutine(UploadData(get_analysis_URL("benchpress", 10)));
        StartCoroutine(GetScaledData(get_scaling_URL()));
    }

    // Function for getting where the reps are located in set data
    private IEnumerator UploadData(string uri)
    {
        WWWForm form = new WWWForm();
        byte[] imageData = csvfile.bytes;
        form.AddBinaryData("csv",imageData);
        using (UnityWebRequest request = UnityWebRequest.Post(uri,form))
        {
            yield return request.SendWebRequest();

            // Deserialize JSON data to an object
            rep_data = JsonConvert.DeserializeObject(request.downloadHandler.text);

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(rep_data);
            }
        }
    }

    // Function for scaling set data for inverse kinemetics
    private IEnumerator GetScaledData(string uri)
    {
        WWWForm form = new WWWForm();
        byte[] imageData = csvfile.bytes;
        form.AddBinaryData("csv",imageData);
        using (UnityWebRequest request = UnityWebRequest.Post(uri,form))
        {
            yield return request.SendWebRequest();

            // Save bytestring to memory
            scaled_data = request.downloadHandler.data;

            // Convert bytestring to file
            using (Stream file = File.OpenWrite(filePath))
            {
                file.Write(scaled_data, 0, scaled_data.Length);
            }

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("Successful scale");
            }
        }
    }

}