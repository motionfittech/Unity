using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class ChangeSceneWithButton : MonoBehaviour
{
    public LoadingScreenBarSystem Fade;
    public GameObject WarningPanel,Login,Signup,DownLoginPanel,DownSignUpPanel;
    public TextMeshProUGUI infotxt;
     float timeloader = 0;

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

        Fade.gameObject.SetActive(true);
        Fade.startLoading();
       
        close.SetActive(false);
        Downclose.SetActive(false);
        open.SetActive(true);
        Downopen.SetActive(true);
        while (timeloader > 0)
        {

           timeloader -= 0.01f;
          
            yield return null;

        }
      //  Fade.gameObject.SetActive(false);
    }

   public void signupButton()
    {
        StartCoroutine(startFade(Login, Signup, DownLoginPanel,DownSignUpPanel));
    }
    public void signupBackButton()
    {
        StartCoroutine(startFade(Signup, Login,DownSignUpPanel, DownLoginPanel));
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

}
