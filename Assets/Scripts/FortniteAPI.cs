
using UnityEngine;

using UnityEngine.Networking;

using System.Collections;


public class FortniteAPI : MonoBehaviour
{

    public TextAsset csvfile;
    private const string URL = "https://parmenides.balance3ds.com/api/analysis/benchpress/10";
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
        StartCoroutine(ProcessRequest(URL));
    }

    private IEnumerator ProcessRequest(string uri)
    {
        WWWForm form = new WWWForm();
        byte[] imageData = csvfile.bytes;
        form.AddBinaryData("csv",imageData);
        using (UnityWebRequest request = UnityWebRequest.Post(uri,form))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.result);
            }
        }
    }
}