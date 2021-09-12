using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class ChangeSceneWithButton : MonoBehaviour
{
    public Image Fade;
    public GameObject WarningPanel,Login,Signup;
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

    public IEnumerator startFade(GameObject close,GameObject open)
    {
        
        Fade.enabled = true;
        Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b,1);
        float temp = 1;
        close.SetActive(false);
        open.SetActive(true);
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
        StartCoroutine(startFade(Login, Signup));
    }
    public void signupBackButton()
    {
        StartCoroutine(startFade(Signup, Login));
    }


}
