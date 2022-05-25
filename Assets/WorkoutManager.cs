using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class WorkoutManager : MonoBehaviour
{
    [SerializeField] private GameObject player = null;

  //  public Renderer startBt;


    [Header("workoutScene1")]
    public List<GameObject> workout1 = new List<GameObject>();
   /* public Vector3 CameraPos1;
    public Vector3 CameraRot1;*/
    [Header("workoutScene2")]
    public List<GameObject> workout2 = new List<GameObject>();
  /*  public Vector3 CameraPos2;
    public Vector3 CameraRot2;*/
    [Header("Imbalance")]
    public List<GameObject> workout3 = new List<GameObject>();
    public GameObject VelocityPanel;
    private int sceneSwitcher = 0;

    public List<string> nameofAnimations = new List<string>();
    public List<string> selectedAnimations = new List<string>();
    public WorkoutHandler WH;
    public TextMeshProUGUI CenterButtonExercisetxt;
    public Button CenterButton;
    public WorkoutScriptableObject currentWorkoutSO;
    private bool _isIKon;
    [HideInInspector] public bool _Dowehaveanimation = false;
    private bool _startcounting;
    private int anicounter;

    [Header("POPUP")]
    public GameObject popupPanel;
    public TextMeshProUGUI PopupText;
    public ModalWindowManager myModalWindow;

    public TextMeshProUGUI currentAniTxt, work2AniTxt,Clickedtext;
 //   public FitCapTest FCT;
    Camera main_Camera;

    [Header("Side Menu")]
    public GameObject sideMenuObject;
    private bool _isOpened = true;
    public GameObject listofSelectedObject;
    public RectTransform prefabofSelectedObject;
    public float testingSpeed;
    public Vector2 OpeningPos, ClosingPos;
    public Sprite openSp, closeSp;
    public Image currentSideImageIcon;
    public ExerDatabaseCsv EDC;
    public TextMeshProUGUI form, imbalance, velocity, velocityloss;
    public string _isLeft = "Left";

    private void Awake()
    {
      //  startBt.material.SetColor("_Outline_Color", Color.black);
      //  startBt.material.SetFloat("TileX", 0.05f);
       // startBt.material.SetFloat("TileY", 0.05f);

        if(PlayerPrefs.GetInt("firsttime",0) == 0)
        {
           
            Application.LoadLevel(5);
        }
    }

    public void activeLoading()
    {
        EDC.GetComponent<CSVManager>().Loadingscreen.SetActive(true);
        switchBt(2);
        updateExerciseTxt();
        callshowData();
        Invoke("callseeDatawithWait",3);

    }
    public void callseeDatawithWait()
    {
        
        EDC.GetComponent<CSVManager>().Loadingscreen.SetActive(false);
    }
    public void switchHand(string Hand)
    {
        _isLeft = Hand;
    }
   public void callshowData()
    {
        LocalDatabase.instance.LoadSeeData();
    }
    private void Start()
    {
      /*  main_Camera = Camera.main;
        main_Camera.transform.position = CameraPos1;
        main_Camera.transform.eulerAngles = CameraRot1;*/
        loadworkoutData();
        
     //   PlayerPrefs.SetString("workout",LocalDatabase.instance.workoutData);
     
    }
    private void LateUpdate()
    {
        OnCompleteAttackAnimation();   
    }

    void OnCompleteAttackAnimation()
    {
        if (!_startcounting)
            return;

        if (WH.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
//            LocalDatabase.instance.repData(HS.label.text,anicounter.ToString());
            anicounter += 1;
        }
        // TODO: Do something when animation did complete
    }
    public void centerworkoutBt()
    {
        if (!_isIKon)
        {

            if (!_Dowehaveanimation)
            {
                switchBt(7);
            
                return;
            }
            

          //  NewWorkout(currentWorkoutSO);
            readworkoutData();
            popupPanel.SetActive(false);
            WH.animator.speed = 1;
            //      StartCoroutine(startCounter());
            //            FCT.OnButtonPress_StartStopButton();

            CenterButton.GetComponent<Image>().color = Color.red;
            CenterButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "STOP";
            _isIKon = true;
          //  GameObject.FindObjectOfType<CSVManager>().call();
        }
        else
        {
            stopWorkout(currentWorkoutSO);
          
            Doneanimation();
            popupPanel.SetActive(false);
            WH.animator.speed = 1;
            //            FCT.OnButtonPress_DisconnectButton();
            CenterButton.GetComponent<Image>().color = Color.green;
            CenterButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "START";
            _isIKon = false;
          

        }
    }

    //IEnumerator startCounter()
    //{
    ///    Doneanimation();
    //    int temp = 0;
    //    popupPanel.SetActive(true);
    //    PopupText.fontSize = 400;
    //    PopupText.text = temp.ToString();
    //    while (temp > 0)
    //    {
    //        yield return new WaitForSeconds(1);
    //        temp -= 1;
    //        PopupText.text = temp.ToString();
           
    //    }
    //    readworkoutData();
    //    StartCoroutine(startingPosCounter());
    //}

//    IEnumerator startingPosCounter()
//    {
//        int temp = 10;
//        popupPanel.SetActive(true);
//        PopupText.fontSize = 150;
//        PopupText.text = "Exercise will start in "+ temp.ToString()+" sec";
//      ///  GetComponent<WorkoutHandler>().PlayerLeftHandModel.SetActive(true);
//      //  GetComponent<WorkoutHandler>().PlayerRightHandModel.SetActive(true);
//        while (temp > 0)
//        {
//            yield return new WaitForSeconds(1);
//            temp -= 1;
//            PopupText.text = "Exercise will start in " + temp.ToString() + " sec";
//            if(temp > 1)
//            {
//                WH.animator.speed = 0;
//            }
//        }
////        FCT.OnButtonPress_DisconnectButton();
//        popupPanel.SetActive(false);
//        WH.animator.speed = 1;
//    }
    #region Circle Work button
    public void NewWorkout(WorkoutScriptableObject currentWorkout)
    {
      
     //   startBt.material.SetColor("_Outline_Color", Color.blue);
     //   startBt.material.SetFloat("_TileX",10.0f);
      //  startBt.material.SetFloat("_TileY", 10.0f);

        //  this.GetComponent<IHandleWorkouts>().NewWorkout(currentWorkout);
    }

    public void stopWorkout(WorkoutScriptableObject currentWorkout)
    {
        //startBt.material.SetColor("_Outline_Color", Color.blue);
       // startBt.material.SetFloat("_TileX", 0.05f);
        //startBt.material.SetFloat("_TileY", 0.05f);
        //if (player)
        //    player.GetComponent<IHandleWorkouts>().StopWorkout(currentWorkout);
    }
    public void ReadUserData()
    {
        player.GetComponent<IHandleWorkouts>().ReadUserData();
    }

    #endregion
    public void switchBt(int counter)
    {
        
        //  myModalWindow.icon = "spriteVariable; // Change icon
        if (counter == 2)
        {
           /* if(EDC.GraphDataPoints.Count == 0)
            {
                myModalWindow.titleText = "SEE DATA"; // Change title
                myModalWindow.descriptionText = "No data found please do at least one rep to view Data."; // Change desc
                counter = 1;
            }
            else
            {*/
                if (_isIKon)
                {
                    myModalWindow.titleText = "SEE DATA"; // Change title
                    myModalWindow.descriptionText = "Viewing Data will stop Exercise, Unsave Reps will be lost"; // Change desc
                }
                else
                {
                    myModalWindow.CloseWindow();
                    answer(counter);
                    return;
                }
          //  }
         
           
           
        }
        else if (counter == 1)
        {
          /*  myModalWindow.titleText = "BACK TO EXERCISE"; // Change title
            myModalWindow.descriptionText = "You can go back to Exercise and start Exercise."; // Change desc*/

            myModalWindow.CloseWindow();
            answer(counter);
            return;

        }
        else if (counter == 0)
        {
            myModalWindow.titleText = "PREVIEW EXERCISE"; // Change title
            myModalWindow.descriptionText = "Preview will stop exercise, Unsave Reps will be lost.."; // Change desc
        }
        else if (counter == 3)
        {
            myModalWindow.titleText = "Main Menu"; // Change title
            myModalWindow.descriptionText = "Do you really want to go to Main Menu, Unsave Reps will be lost."; // Change desc
        }
        else if (counter == 4)
        {
            myModalWindow.titleText = "Imbalances"; // Change title
            myModalWindow.descriptionText = "Do you really want to go to Imbalances, Unsave Reps will be lost."; // Change desc
        }
        else if (counter == 7)
        {
            myModalWindow.titleText = "Exercise"; // Change title
            myModalWindow.descriptionText = "Do you want to add exerice click? ";
        }
        else if (counter == 8)
        {
            myModalWindow.titleText = "Tutorial"; // Change title
            myModalWindow.descriptionText = "You have already Watched the tutorials, do you want to go back and master your skill? ";
        }

        myModalWindow.onConfirm.RemoveAllListeners();
    //    myModalWindow.onCancel.RemoveAllListeners();
     //   myModalWindow.onCancel.AddListener(delegate { GameObject.FindObjectOfType<FitCapTest>().DisplayData = false; });
        myModalWindow.onConfirm.AddListener(delegate { answer(counter); });
        myModalWindow.UpdateUI(); // Update UI
       // myModalWindow.OpenWindow(); // Open window
        myModalWindow.AnimateWindow(); // Close/Open window automatically
       
    }

    public void answer(int counter)
    {
        Doneanimation();
        switch (counter)
        {
            case 0:
               
                readworkoutData();
                break;
            case 1:
                innerList(workout2, false);
                innerList(workout3, false);
                innerList(workout1, true);
               
            /*    main_Camera.transform.position = CameraPos1;
                main_Camera.transform.eulerAngles = CameraRot1;*/
                break;
            case 2:
                innerList(workout1, false);
                innerList(workout3, false);
                innerList(workout2, true);
                stopWorkout(currentWorkoutSO);

                Doneanimation();
                popupPanel.SetActive(false);
                WH.animator.speed = 1;
                //            FCT.OnButtonPress_DisconnectButton();
                CenterButton.GetComponent<Image>().color = Color.green;
                CenterButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "START";
                _isIKon = false;
                /*    main_Camera.transform.position = CameraPos2;
                    main_Camera.transform.eulerAngles = CameraRot2;*/
                break;
            case 3:
                Application.LoadLevel(1);
                break;
            case 4:
                innerList(workout1, false);
                innerList(workout2, false);
                innerList(workout3, true);
                break;
            case 5:
                innerList(workout1, false);
                innerList(workout2, false);
                innerList(workout3, false);
                VelocityPanel.SetActive(true);
               
                break;
            case 6:
                innerList(workout1, false);
                innerList(workout2, true);
                innerList(workout3, false);
                VelocityPanel.SetActive(false);
                break;
            case 7:
                sideMenuBt();
            //    GameObject.FindObjectOfType<FitCapTest>().DisplayData = false;
                break;
            case 8:
                Application.LoadLevel(5);
                break;
            default:
                print("Incorrect");
                break;
        }
    }

  
    void innerList(List<GameObject> temp , bool condition)
    {
        for(int i = 0; i < temp.Count; i++)
        {
            temp[i].SetActive(condition);
        }
    }


    public void loadworkoutData()
    {
     
        StartCoroutine(loadingWorkoutData());
      
    }

    IEnumerator loadingWorkoutData()
    {

        while (PlayerPrefs.GetString("workout", "").Length <= 0)
        {
           
            yield return null;
        }
       
        string listAnimation = PlayerPrefs.GetString("workout", "");
        string[] tempData = listAnimation.Split(","[0]);
        for (int i = 0; i < tempData.Length; i++)
        {
            int temp = int.Parse(tempData[i]);
            selectedAnimations.Add(nameofAnimations[temp]);
//            print(nameofAnimations[temp]);
        //    HS.CreateNewItem(nameofAnimations[temp]);

        }
      //  HS.label.text = selectedAnimations[0];
        
    }
    public void readworkoutData()
    {
        for (int y = 0; y < nameofAnimations.Count; y++)
        {
            WH.animator.SetBool(nameofAnimations[y], false);
        }
        for (int a = 0; a < selectedAnimations.Count; a++)
        {
            if (selectedAnimations[a] == CenterButtonExercisetxt.text)
            {
                WH.animator.SetBool(selectedAnimations[a], true);
            }
        }
        _startcounting = true;
    }

    public void Doneanimation()
    {
        for (int y = 0; y < nameofAnimations.Count; y++)
        {
            WH.animator.SetBool(nameofAnimations[y], false);
        }

        _startcounting = false;
    }

    public void updateExerciseTxt()
    {
       // work2AniTxt.text = currentAniTxt.text;
    }

    public void textString(string textH)
    {
        Clickedtext.text = textH;
    }

    public void sideMenuBt()
    {
        StartCoroutine(SideMoveCall(_isOpened));
    }

    public IEnumerator SideMoveCall(bool conditionn)
    {
        float waitingtime = testingSpeed/2;
        switch (conditionn)
        {
            case true:
                while (waitingtime > 0)
                {
                    currentSideImageIcon.sprite = closeSp;
                    sideMenuObject.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(sideMenuObject.GetComponent<RectTransform>().localPosition, OpeningPos, testingSpeed*2);
                    yield return null;
                    waitingtime -= 1;

                }
              


                break;
            case false:
                while (waitingtime > 0)
                {
                    currentSideImageIcon.sprite = openSp;
                    sideMenuObject.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(sideMenuObject.GetComponent<RectTransform>().localPosition,ClosingPos,testingSpeed*2);
                    yield return null;
                    waitingtime -= 1;

                }
                


                break;
        }

        _isOpened = !conditionn;

        if (listofSelectedObject.transform.childCount == 1)
        {

            for (int i = 0; i < selectedAnimations.Count; i++)
            {
                RectTransform clone = Instantiate(prefabofSelectedObject, Vector3.zero, Quaternion.identity);
                clone.SetParent(listofSelectedObject.transform);
                clone.GetChild(1).GetComponent<TextMeshProUGUI>().text = selectedAnimations[i];
                clone.gameObject.SetActive(true);
                clone.localScale = Vector3.one;
            }
        }
           

    }
}
