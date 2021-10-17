using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Extensions;

public class CustomAuth : MonoBehaviour
{
    public TMP_InputField UserNameInput, PasswordInput;
    public TMP_InputField SignUpusername, SignUppassword, SignUpgmail;

    FirebaseAuth auth;
    private FirebaseUser user;
    public FirebaseSetUp FS;
    // Start is called before the first frame update
    void Start()
    {
    //    UserNameInput.text = "demofirebase@gmail.com";
     //   PasswordInput.text = "abcdefgh";

        //SignupButton.onClick.AddListener(() => Signup(SignUpgmail.text, PasswordInput.text, SignUpusername.text));
        //LoginButton.onClick.AddListener(() => Login(UserNameInput.text, PasswordInput.text));
         auth = FirebaseAuth.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateErrorMessage(string message)
    {
       
        Invoke("ClearErrorMessage", 3);
    }

    void ClearErrorMessage()
    {
       
    }

    public void LogincallButton()
    {
       if(UserNameInput.text.Length == 0)
        {
            FS.CSB.popup("Please Enter username then try again");
            return;
        }
       else if (PasswordInput.text.Length == 0)
        {
            FS.CSB.popup("Please Enter Password then try again");
            return;
        }
        Login(UserNameInput.text, PasswordInput.text);
    }
    public void SignupcallButton()
    {
      
        if(SignUpgmail.text.IndexOf('@') <= 0 || SignUpgmail.text.Length == 0)
        {
            FS.CSB.popup("Enter Valid email and try again.");

            return;
        }
        else if ( SignUppassword.text.Length < 6)
        {
           // print("passwordshould be greater then sixxtings");
            FS.CSB.popup("Enter Pasword which should have it least 6 digits.");
            return;
        }
        else if (SignUpusername.text.Length == 0)
        {
            FS.CSB.popup("Enter your username and try again..");
            return;
        }

        Signup(SignUpgmail.text, SignUppassword.text, SignUpusername.text);
    }

    public void Login(string email, string password)
    {
        print(password);
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync canceled.");
                return;
            }
            if (task.IsFaulted)
            {
              //  Debug.LogError("SignInWithEmailAndPasswordAsync error: " + task.Exception);
                FS.CSB.popup("No username " +UserNameInput.text +" found, please register your account or try with different account");
               /* if (task.Exception.InnerExceptions.Count > 0)
                    UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);

*/
                FS.CSB.Fade.enabled = false;
                return;
            }

             user = task.Result;
            FS.CSB.LoadScene();
            LocalDatabase.instance.saveData(user.UserId);
             PlayerPrefs.SetString("password", password);
            PlayerPrefs.SetString("loginMethod", "C");

        });
    }

    public void Signup(string email, string password, string username)
    {
        FS.CSB.Fade.enabled = true;
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
              //  Debug.LogError("CreateUserWithEmailAndPasswordAsync error: " + task.Exception);
                string temp = task.Exception.ToString();
                if(temp.Contains("The email address is already in use by another account"))
                {
                  
                    
                    FS.CSB.popup("User with "+ email +" already have a account please try sign in");
                   
                }
              /*  if (task.Exception.InnerExceptions.Count > 0)
                    UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
*/

                FS.CSB.Fade.enabled = false;
                return;
            }

            user = task.Result; // Firebase user has been created.
            FS.registerUser(username,email,user.UserId);
            PlayerPrefs.SetString("password", password);
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                user.DisplayName, user.UserId);
            UpdateErrorMessage("Signup Success");

        });
    }
}
