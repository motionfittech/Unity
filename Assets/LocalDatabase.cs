using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
public class LocalDatabase : MonoBehaviour
{
    public string username;
    public string email;
    public string UID;
    public static LocalDatabase instance;
    private DataSnapshot levelSnapshot;
    public int ExeCounter = 0;
    public bool DisplayData = false;
    public List<int> indexofGraphs = new List<int>();
    public List<string> indexofNameGraphs = new List<string>();
    public List<float> indexofVelocityGraphs = new List<float>();
    public List<float> indexofformGraphs = new List<float>();
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

        Firebase.Database.FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
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
        // getExerciseCount();
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
                print("Failed to load CSV count");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
//                   print(snapshot.ChildrenCount);
                foreach (var temp in snapshot.Children)
                {
                    PlayerPrefs.SetString("csvCounter", temp.Value.toString());
                }
                // Success
            }
        });
    }
    public void LoadSeeData()
    {
        indexofGraphs = new List<int>(0);
        indexofNameGraphs = new List<string>(0);
        indexofVelocityGraphs = new List<float>(0);
        indexofformGraphs = new List<float>(0);
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("users").Child(UID).Child("CSV_Data").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Failure

            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
//                print("Total seeData Count "+ snapshot.ChildrenCount);
                if(snapshot.ChildrenCount > 10)
                {
                    int tempTotal = (int)snapshot.ChildrenCount;
                    tempTotal -= 1;
                    int totalValues = 11;

                    for(int i = 0; i< 11; i++)
                    {
                        totalValues -= 1;
                        indexofGraphs.Add(tempTotal - totalValues);
                        foreach (var temp in snapshot.Children)
                        {
                            //print(Regex.Replace(temp.Key.ToString(), "[^0-9]", ""));
                            if (Regex.Replace(temp.Key.ToString(), "[^0-9]", "") == (indexofGraphs[i]).ToString())
                            {
                                //     print(temp.Key);
                                indexofNameGraphs.Add(temp.Key);
                                LoadMatricPerExerciseData(temp.Key);
                                IDictionary dictUser = (IDictionary)temp.Value;
                                print("Inside LoadSeeData; Classification: " + dictUser["ml"]);
                            }
                        }
                    }
                }
              
                // Success
            }
        });
    }
    public void LoadMatricPerExerciseData(string exerciseName)
    {
        print("Entered metrics function");

        EquationData tempED = GameObject.FindObjectOfType<EquationData>();

        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("users").Child(UID).Child("CSV_Data").Child(exerciseName).Child("metrics").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Failure
                print("Fault in metrics DB call");
            } 
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (var temp in snapshot.Children)
                {
//                   print(temp.Key);

                    if (temp.Key == "form")
                    {
                      //  print(temp);
                        print("Form: " + temp.Value.ToString().Substring(0, 8));
                        tempED.form.text = temp.Value.ToString().Substring(0,8);
                        indexofformGraphs.Add(float.Parse(tempED.form.text));
                    }
                    if(temp.Key == "velocity")
                    {
                      //  print(temp);
                        print("Velocity: " + temp.Value.ToString().Substring(0, 8));
                        tempED.velocity.text = temp.Value.ToString().Substring(0, 8);
                        indexofVelocityGraphs.Add(float.Parse(tempED.velocity.text));
                    }
                    if(temp.Key == "velocity_loss")
                    {
                     //   print(temp);
                        print("Velocity loss: " + temp.Value.ToString().Substring(0, 8));
                        tempED.velocity_loss.text = temp.Value.ToString().Substring(0, 8);
                    }
                    if(temp.Key == "imbalance")
                    {
                        // TODO: get imbalance readings from DB working
                        // tempED.imbalance_l.text = temp.Value[0].ToString().Substring(0, 8);
                        // tempED.imbalance_r.text = temp.Value[1].ToString().Substring(0, 8);
                    }
                   
                    
                }

                // Success
            }
        });

        loadClassificationPerExercise(exerciseName, tempED);

    }

    public void loadClassificationPerExercise(string exerciseName, EquationData data) {
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("users").Child(UID).Child("CSV_Data").Child(exerciseName).Child("ml").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Failure
                print("Fault in classification DB call");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (var temp in snapshot.Children)
                {

                    if (temp.Key == "classification")
                    {
                        print("Classification: " + temp.Value.ToString().Substring(0, 8));
                        data.classification.text = temp.Value.ToString().Substring(0, 8);
                    }
                    if (temp.Key == "confidence")
                    {
                        print("Confidence: " + temp.Value.ToString().Substring(0, 8));
                        data.confidence.text = temp.Value.ToString().Substring(0, 8);
                    }
                    
                }

                // Success
            }
        });
    }

    //public void savcsvcounter(string Data)
    //{
    //    Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
    //    dbRef.Child("users").Child(UID).Child("csvCounter").SetValueAsync(Data);
    //    PlayerPrefs.SetString("csvCounter", Data.ToString());
    //}
    public void saveExerciseData(MyClass temp)
    {
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("users").Child(UID).Child("CSV_Data").Child(GameObject.FindObjectOfType<CSVManager>().ExerciseTxt.text + PlayerPrefs.GetString("csvCounter","")).SetRawJsonValueAsync(JsonUtility.ToJson(temp));
        dbRef.Child("users").Child(UID).Child("CSV_Data").Child(GameObject.FindObjectOfType<CSVManager>().ExerciseTxt.text + PlayerPrefs.GetString("csvCounter", "")).Child("Hand").SetValueAsync(GameObject.FindObjectOfType<WorkoutManager>()._isLeft);
       

    }

    // public void saveVelocityData(float value)
    // {
      
    //     Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
    //     dbRef.Child("users").Child(UID).Child("exercisedata").Child("averagevelocity").Child(ExeCounter.ToString()).SetRawJsonValueAsync(value.ToString());
       
    // }
    // public void repData(string Exercisename, string Data)
    // {
    //     Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
    //     dbRef.Child("users").Child("ab00").Child("DailyWorkout").Child(Exercisename).Child("fatigue").SetValueAsync(Data);
    //     dbRef.Child("users").Child("ab00").Child("DailyWorkout").Child(Exercisename).Child("Velocity").SetValueAsync(Data);
    //     dbRef.Child("users").Child("ab00").Child("DailyWorkout").Child(Exercisename).Child("distance").SetValueAsync(Data);
    //     dbRef.Child("users").Child("ab00").Child("DailyWorkout").Child(Exercisename).Child("calories ").SetValueAsync(Data);
    //     dbRef.Child("users").Child("ab00").Child("WeeklyWorkout").SetValueAsync("00");
    //     dbRef.Child("users").Child("ab00").Child("MonthlyWorkout").SetValueAsync("00");
    // }
    
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
//     public void getExerciseCount()
//     {
     
      
        
//             Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
//             dbRef.Child("users").Child(UID).Child("exercisedata").Child("averagevelocity").GetValueAsync().ContinueWithOnMainThread(task =>
//             {
//                 if (task.IsFaulted)
//                 {
//                     // Handle the error...
//                 }
//                 else if (task.IsCompleted)
//                 {
                   
//                     DataSnapshot snapshot = task.Result;
                  
//                     ExeCounter = (int)snapshot.ChildrenCount;
// //                    print(ExeCounter +"aaaa");
//                 }
//             });

       
//      }

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
