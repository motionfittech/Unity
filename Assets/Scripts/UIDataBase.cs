using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.UI;
public class UIDataBase : MonoBehaviour
{
    public Text Newfeedtxt;
    public List<GameObject> character = new List<GameObject>();
    public Image Fadeimage;
    // Start is called before the first frame update
    void Start()
    {
        Getnewfeed();
        StartCoroutine(getCharacter(character,Fadeimage.gameObject));
    }

    // Update is called once per frame
  
    public void setCharacter(string indexer)
    {
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("users").Child("ab00").Child("characterselect").SetValueAsync(indexer);
    }
    public void Getnewfeed()
    {
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("News").GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                Newfeedtxt.text = "Faulted Values";
            }
            else if (task.IsCompleted)
            {
                Newfeedtxt.text = task.Result.Value.ToString();

            }
        });
    }


    public IEnumerator getCharacter(List<GameObject> chars, GameObject FadeImage)
    {
        bool inCondition = false;
        int temp = 0;
        FadeImage.SetActive(true);
        while (!inCondition)
        {
            Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
            dbRef.Child("users").Child("ab00").Child("characterselect").GetValueAsync().ContinueWithOnMainThread(task =>
            {
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

        manageCharacter(chars, temp);
        FadeImage.SetActive(false);

    }
    void manageCharacter(List<GameObject> chars, int charValue)
    {
        for (int i = 0; i < chars.Count; i++)
        {

            if (charValue == i)
            {
                chars[i].SetActive(true);

                //if (GameObject.FindObjectOfType<ScrollHandler>() != null)
                //{
                //    GameObject.FindObjectOfType<ScrollHandler>().ani = chars[i].GetComponent<Animator>();
                //}
                //else if (GameObject.FindObjectOfType<WorkoutHandler>() != null)
                //{
                //    //      GameObject.FindObjectOfType<WorkoutHandler>().animator = chars[i].GetComponent<Animator>();
                //}

            }
            else
            {
                chars[i].SetActive(false);
            }
        }
    }

    public void LoadlevelHere(int index)
    {
        Application.LoadLevel(index);
    }

}
