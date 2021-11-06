using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ChangeSceneWithButton : MonoBehaviour
{
    public LoadingScreenBarSystem Fade;
    public GameObject WarningPanel,Login,Signup,DownLoginPanel,DownSignUpPanel;
    public TextMeshProUGUI infotxt;
     float timeloader = 0;
    public CanvasGroup Sidepanel;


    public static ChangeSceneWithButton Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        DOTween.Init();
    }

    private void Start()
    {
        //LoadScene();
        // StartCoroutine(startFade(Signup, Login));
       
      
    }

    public void LoadScene()
    {
        Fade.gameObject.SetActive(true);
      
        Invoke("Restart",1);
    }
    private void Restart()
    {
        Fade.gameObject.SetActive(true);
        Fade.startLoading();
        Invoke("loadScene",1);
    }
    public void loadScene()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public IEnumerator startFade(GameObject close,GameObject open,GameObject Downclose,GameObject Downopen)
    {

       // Fade.gameObject.SetActive(true);
       // Fade.startLoading();
       
        close.SetActive(false);
        Downclose.SetActive(false);
        open.SetActive(true);
        Downopen.SetActive(true);
      //  sideSwitch(1929);
        //while (timeloader > 0)
        //{

        //   timeloader -= 0.01f;

        yield return null;

      //  }
     //   GameObject.FindObjectOfType<LoadingScreenBarSystem>().startLoading();
        //  Fade.gameObject.SetActive(false);
    }

    public void signupButton()
    {
        StartCoroutine(startFade(Login, Signup, DownLoginPanel,DownSignUpPanel));
        sideSwitch(2000);

    }
    public void signupBackButton()
    {
        StartCoroutine(startFade(Signup, Login,DownSignUpPanel, DownLoginPanel));
        sideSwitch(2000);
    }

    public void popup(string info)
    {
        if (WarningPanel.active)
        {
            WarningPanel.SetActive(false);
        }
        else
        {
            infotxt.text = info;
            WarningPanel.SetActive(true);
        }
    }

    public void startGame()
    {
        Sidepanel.DOFade(1,2).SetEase(Ease.Flash);
    }


    public void sideSwitch(float valuex)
    {
        Sidepanel.GetComponent<RectTransform>().localPosition = new Vector3(valuex,0,0);
        Sidepanel.GetComponent<RectTransform>().DOLocalMove(Vector3.zero,0.7f).SetEase(Ease.Flash);
    }
   
}
