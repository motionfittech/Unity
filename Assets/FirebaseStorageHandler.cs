using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.UI;
public class FirebaseStorageHandler : MonoBehaviour
{


    public void uploadData(string userId, string pather)
    {

        StartCoroutine(ShowLoadDialogCoroutine(userId, pather));
    }

    public IEnumerator ShowLoadDialogCoroutine(string userID, string path)
    {
        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // Create a storage reference from our storage service
        StorageReference storageReference =
            storage.GetReferenceFromUrl("gs://motionfit-878e2.appspot.com");

        print("we are in");
        yield return null;

        //  Debug.Log(FileBrowser.Success);

        //if (FileBrowser.Success)
        // {
        // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
        //   for (int i = 0; i < FileBrowser.Result.Length; i++)
        //     Debug.Log(FileBrowser.Result[i]);

        Debug.Log("File Selected");
        // Create a texture the size of the screen, RGB24 format
        /*  int width = Screen.width;
          int height = Screen.height;
          Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

          // Read screen contents into the texture
          tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
          tex.Apply();

          // Encode texture into PNG
          byte[] bytes = tex.EncodeToPNG();*/
        // byte[] bytes =   // FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
        //Editing Metadata
        var newMetadata = new MetadataChange();
        //         newMetadata.ContentType = "image/jpeg";

        //Create a reference to where the file needs to be uploaded
        DateTime theTime = DateTime.Now;
        string date = theTime.ToString("yyyy-MM-dd\\Z");
        string time = theTime.ToString("HH:mm:ss\\Z");
        string datetime = theTime.ToString("yyyy-MM-dd\\THH:mm:ss\\Z");
        StorageReference uploadRef = storageReference.Child("CSV/" + userID + "/" + datetime.ToString() + ".csv");
        Debug.Log("File upload started");
        uploadRef.PutFileAsync(path).ContinueWithOnMainThread((task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception.ToString());
            }
            else
            {
                Debug.Log("File Uploaded Successfully!");
            }
        });


        //  }
    }


}
