using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FirebaseSetUp : MonoBehaviour
{
    [HideInInspector]public ChangeSceneWithButton CSB;


    private void Start()
    {
        CSB = ChangeSceneWithButton.Instance;
        CSB.Fade.enabled = true;
        Invoke("checkLogin",2);
    }

    public void registerUser(string username, string email, string UID)
    {
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("users").Child(UID).Child("username").SetValueAsync(username);
        dbRef.Child("users").Child(UID).Child("email").SetValueAsync(email);
        dbRef.Child("users").Child(UID).Child("characterselect").SetValueAsync("0");
        dbRef.Child("users").Child(UID).Child("csvCounter").SetValueAsync("0");
        LocalDatabase.instance.saveData(UID);
        CSB.LoadScene();
        Destroy(this.GetComponent<CustomAuth>());
        DontDestroyOnLoad(this.gameObject);
    }
    public void savedata(Vector3 acc,Vector3 rot)
    {
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("users").Child(LocalDatabase.instance.UID).Child("CSV").Child("CSV").SetValueAsync(acc.x.ToString());
    }
    public void checkLogin()
    {



        if(PlayerPrefs.GetString("uid","").Length > 0)
        {
           List<string> tempSignData = LocalDatabase.instance.Getvalue();

            if (PlayerPrefs.GetString("loginMethod", "C") == "C")
            {
               
                GetComponent<CustomAuth>().Login(tempSignData[1], PlayerPrefs.GetString("password", "0").ToString());
            }
            else if (PlayerPrefs.GetString("loginMethod", "C") == "F")
            {
                GetComponent<facebookLogin>().FBlogin();
            }
            else if (PlayerPrefs.GetString("loginMethod", "C") == "G")
            {
                //GetComponent<GoogleSignInDemo>().SignInWithGoogle();
            }

            //   CSB.LoadScene();
            return;
        }
        StartCoroutine(CSB.startFade(CSB.Signup, CSB.Login, CSB.DownSignUpPanel, CSB.DownLoginPanel));
        
    }

}


