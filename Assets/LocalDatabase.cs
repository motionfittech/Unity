//using Firebase.Database;
//using Firebase.Extensions;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//public class LocalDatabase : MonoBehaviour
//{
//    public string username;
//    public string gmail;
//    public string UID;
//    public static LocalDatabase instance;
//    private DataSnapshot levelSnapshot;

//    private void Awake()
//    {
//        if (instance != null)
//        {
//            Destroy(gameObject);
//        }
//        else
//        {
//            instance = this;
//            DontDestroyOnLoad(gameObject);
//        }

//        QualitySettings.vSyncCount = 0;
//        Application.targetFrameRate = 60;
//    }

//     void Start()
//    {
       
//    }


//    void Update()
//    {
//        if (Application.targetFrameRate != 60)
//            Application.targetFrameRate = 60;
//    }

//    public List<string> Getvalue()
//    {
//        username = PlayerPrefs.GetString("username", "");
//        gmail = PlayerPrefs.GetString("gmail", "");
        
//        UID = PlayerPrefs.GetString("uid", "");
//        List<string> InfoData = new List<string>();
//        InfoData.Add(username);
//        InfoData.Add(gmail);
//        InfoData.Add(UID);
      
//        return InfoData;
//    }

//    public void saveData(string usernameP,string gmailP,string uidP)
//    {
//        username = usernameP;
//        gmail = gmailP;
//        UID = uidP;
//        PlayerPrefs.SetString("username",username);
//        PlayerPrefs.SetString("gmail", gmail);
//        PlayerPrefs.SetString("uid", UID);
       
//    }

//    public void saveWorkout(string Data)
//    {

//        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
//        dbRef.Child("users").Child(UID).Child("workout").SetValueAsync(Data);
//        PlayerPrefs.SetString("workout",Data);
//    }
//    public void Loadworkout()
//    {
//        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
       
       
//            dbRef.Child("users").Child(UID).Child("workout").GetValueAsync().ContinueWithOnMainThread(task =>
//            {
//                if (task.IsFaulted)
//                {
//                    // Failure
//                    print("dsfdsf");
//                }
//                else if (task.IsCompleted)
//                {
//                    DataSnapshot snapshot = task.Result;
//                    print(snapshot.Value.ToString());
//                    PlayerPrefs.SetString("workout", snapshot.Value.ToString());
//                    // Success
//                }
//            });

           
        

       
//    }
//    public void repData(string Exercisename, string Data)
//    {
//        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
//        dbRef.Child("users").Child(UID).Child("DailyWorkout").Child(Exercisename).Child("fatigue").SetValueAsync(Data);
//        dbRef.Child("users").Child(UID).Child("DailyWorkout").Child(Exercisename).Child("Velocity").SetValueAsync(Data);
//        dbRef.Child("users").Child(UID).Child("DailyWorkout").Child(Exercisename).Child("distance").SetValueAsync(Data);
//        dbRef.Child("users").Child(UID).Child("DailyWorkout").Child(Exercisename).Child("calories ").SetValueAsync(Data);
//        dbRef.Child("users").Child(UID).Child("WeeklyWorkout").SetValueAsync("00");
//        dbRef.Child("users").Child(UID).Child("MonthlyWorkout").SetValueAsync("00");
//    }
    
//    public IEnumerator getCharacter(List<GameObject> chars, GameObject FadeImage)
//    {
//        bool inCondition = false;
//        int temp = 0;
//        while (!inCondition)
//        {
//            Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
//            dbRef.Child("users").Child(UID).Child("characterselect").GetValueAsync().ContinueWithOnMainThread(task => {
//                if (task.IsFaulted)
//                {
//                    // Handle the error...
//                }
//                else if (task.IsCompleted)
//                {
//                    temp = int.Parse(task.Result.Value.ToString());
//                    inCondition = true;
//                }
//            });
//            yield return null;
//        }

//        manageCharacter(chars, temp);
//        FadeImage.SetActive(false);

//    }
//    public void setCharacter(string indexer)
//    {
//        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
//        dbRef.Child("users").Child(UID).Child("characterselect").SetValueAsync(indexer);
//    }

//     void manageCharacter(List<GameObject> chars, int charValue)
//    {
//        for(int i = 0; i< chars.Count; i++)
//        {

//            if(charValue == i)
//            {
//                chars[i].SetActive(true);

//                if (GameObject.FindObjectOfType<ScrollHandler>() != null)
//                {
//                    GameObject.FindObjectOfType<ScrollHandler>().ani = chars[i].GetComponent<Animator>();
//                }
//                else if (GameObject.FindObjectOfType<WorkoutHandler>() != null)
//                {
//                    GameObject.FindObjectOfType<WorkoutHandler>().animator = chars[i].GetComponent<Animator>();
//                }

//            }
//            else
//            {
//                chars[i].SetActive(false);
//            }
//        }
//    }
//}
