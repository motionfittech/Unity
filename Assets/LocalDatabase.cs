using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LocalDatabase : MonoBehaviour
{
    public string username;
    public string email;
    public string UID;
    public static LocalDatabase instance;
    private DataSnapshot levelSnapshot;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

     void Start()
    {
       
    }


    void Update()
    {
        if (Application.targetFrameRate != 60)
            Application.targetFrameRate = 60;
    }

    public List<string> Getvalue()
    {
        username = PlayerPrefs.GetString("username", "");
        email = PlayerPrefs.GetString("email", "");
        
        UID = PlayerPrefs.GetString("uid", "");
        List<string> InfoData = new List<string>();
        InfoData.Add(username);
        InfoData.Add(email);
        InfoData.Add(UID);
      
        return InfoData;
    }

    public void saveData(string uidP)
    {
      
        UID = uidP;
        PlayerPrefs.SetString("uid", UID);
        loadusername();
        loadmail();
    }



    public void saveWorkout(string Data)
    {

        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("users").Child(UID).Child("workout").SetValueAsync(Data);
        PlayerPrefs.SetString("workout",Data);
    }

    public void loadusername()
    {
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;


        dbRef.Child("users").Child(UID).Child("username").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Failure
                print("dsfdsf");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                //    print(snapshot.Value.ToString());
                PlayerPrefs.SetString("username", snapshot.Value.ToString());
                username = snapshot.Value.ToString();
                // Success
            }
        });
    }
    public void loadmail()
    {
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;


        dbRef.Child("users").Child(UID).Child("email").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Failure
                print("dsfdsf");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                //    print(snapshot.Value.ToString());
                PlayerPrefs.SetString("email", snapshot.Value.ToString());
                email = snapshot.Value.ToString();
                // Success
            }
        });
    }

    public void Loadworkout()
    {
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
       
       
            dbRef.Child("users").Child(UID).Child("workout").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    // Failure
                    print("dsfdsf");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                //    print(snapshot.Value.ToString());
                    PlayerPrefs.SetString("workout", snapshot.Value.ToString());
                    // Success
                }
            });

           
        

       
    }
    public void Loadcsvcounter()
    {
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("users").Child(UID).Child("csvCounter").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Failure
              
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                //    print(snapshot.Value.ToString());
                PlayerPrefs.SetString("csvCounter", snapshot.Value.ToString());
                // Success
            }
        });
    }
    public void savcsvcounter(string Data)
    {
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("users").Child(UID).Child("csvCounter").SetValueAsync(Data);
        PlayerPrefs.SetString("csvCounter", Data.ToString());
    }
    public void saveExerciseData(MyClass temp)
    {
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("users").Child(UID).Child("CSV_Data").Child(GameObject.FindObjectOfType<CSVManager>().ExerciseTxt.text + PlayerPrefs.GetString("csvCounter","")).SetRawJsonValueAsync(JsonUtility.ToJson(temp));
    }

    public void repData(string Exercisename, string Data)
    {
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("users").Child("ab00").Child("DailyWorkout").Child(Exercisename).Child("fatigue").SetValueAsync(Data);
        dbRef.Child("users").Child("ab00").Child("DailyWorkout").Child(Exercisename).Child("Velocity").SetValueAsync(Data);
        dbRef.Child("users").Child("ab00").Child("DailyWorkout").Child(Exercisename).Child("distance").SetValueAsync(Data);
        dbRef.Child("users").Child("ab00").Child("DailyWorkout").Child(Exercisename).Child("calories ").SetValueAsync(Data);
        dbRef.Child("users").Child("ab00").Child("WeeklyWorkout").SetValueAsync("00");
        dbRef.Child("users").Child("ab00").Child("MonthlyWorkout").SetValueAsync("00");
    }
    
    public IEnumerator getCharacter(List<GameObject> chars, GameObject FadeImage)
    {
        bool inCondition = false;
        int temp = 0;
        while (!inCondition)
        {
            Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
            dbRef.Child("users").Child(UID).Child("characterselect").GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted)
                {
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    temp = int.Parse(task.Result.Value.ToString());
                    inCondition = true;
                }
            });
            yield return null;
        }

        manageCharacter(chars, 101);
        FadeImage.SetActive(false);

    }
    public void setCharacter(string indexer)
    {
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("users").Child(UID).Child("characterselect").SetValueAsync(indexer);
    }

     void manageCharacter(List<GameObject> chars, int charValue)
    {
        for(int i = 0; i< chars.Count; i++)
        {

            if(charValue == i)
            {
                chars[i].SetActive(true);

              //  if (GameObject.FindObjectOfType<ScrollHandler>() != null)
              //  {
              // //     GameObject.FindObjectOfType<ScrollHandler>().ani = chars[i].GetComponent<Animator>();
              //  }
              //  else if (GameObject.FindObjectOfType<WorkoutHandler>() != null)
              //  {
              ////      GameObject.FindObjectOfType<WorkoutHandler>().animator = chars[i].GetComponent<Animator>();
              //  }

            }
            else
            {
                chars[i].SetActive(false);
            }

            if(i == 100)
            {
                GameObject.FindObjectOfType<WorkoutHandler>().Defaultanimator(1);
            }
            else
            {
                GameObject.FindObjectOfType<WorkoutHandler>().Defaultanimator(0);
            }
        }
    }

    public class MyClass
    {
        public int counter;
        public string[] intArray;
        public void setArray()
        {
            intArray = new string[counter];
        }
    }
}
