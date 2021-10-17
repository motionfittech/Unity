using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class ChangeSceneWithButton : MonoBehaviour
{
    public Image Fade;
    public GameObject WarningPanel,Login,Signup,DownLoginPanel,DownSignUpPanel;
    public TextMeshProUGUI infotxt;
   

    private void Start()
    {
        //LoadScene();
       // StartCoroutine(startFade(Signup, Login));
    }

    public void LoadScene()
    {
        Fade.enabled = true;
        Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, 1);
        Invoke("Restart",1);
    }
    private void Restart()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public IEnumerator startFade(GameObject close,GameObject open,GameObject Downclose,GameObject Downopen)
    {
        
        Fade.enabled = true;
        Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b,1);
        float temp = 1;
        close.SetActive(false);
        Downclose.SetActive(false);
        open.SetActive(true);
        Downopen.SetActive(true);
        while (Fade.color.a > 0)
        {

           temp -= 0.05f;
            Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, temp);
            yield return null;

        }
        Fade.enabled = false;
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
