using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
//using Firebase.Storage;
using UnityEngine;

public class FirebaseStorageHandler : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
    //    // Get a reference to the storage service, using the default Firebase App
    //    FirebaseStorage storage = FirebaseStorage.DefaultInstance;

    //    // Create a storage reference from our storage service
    //    StorageReference storageRef =
    //        storage.GetReferenceFromUrl("gs://motionfit-878e2.appspot.com");

    //    // File located on disk
    //    string localFile = "...";

    //    // Create a reference to the file you want to upload
    //    StorageReference riversRef = storageRef.Child("images/rivers.jpg");

    //    // Upload the file to the path "images/rivers.jpg"
    //    riversRef.PutFileAsync(localFile)
    //        .ContinueWith((Task<StorageMetadata> task) => {
    //            if (task.IsFaulted || task.IsCanceled)
    //            {
    //                Debug.Log(task.Exception.ToString());
    //        // Uh-oh, an error occurred!
    //    }
    //            else
    //            {
    //        // Metadata contains file metadata such as size, content-type, and download URL.
    //        StorageMetadata metadata = task.Result;
    //                string md5Hash = metadata.Md5Hash;
    //                Debug.Log("Finished uploading...");
    //                Debug.Log("md5 hash = " + md5Hash);
    //            }
    //        });
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
