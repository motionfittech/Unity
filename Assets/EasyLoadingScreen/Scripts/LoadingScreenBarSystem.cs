using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreenBarSystem : MonoBehaviour {

    public GameObject bar;
    public Text loadingText;
    public bool backGroundImageAndLoop;
    public float LoopTime;
    public GameObject[] backgroundImages;
    [Range(0,1f)]public float vignetteEfectVolue; // Must be a value between 0 and 1
    AsyncOperation async;
    Image vignetteEfect;


    public void loadingScreen (int sceneNo)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(Loading(sceneNo));
    }

    // Used to try. Delete the comment lines (25 and 36)
    
    public void Update()
    {
        
            bar.transform.localScale += new Vector3(Random.Range(0.001f,0.003f),0,0)*5;

            if (loadingText != null)
                loadingText.text = "%" + (100 * bar.transform.localScale.x).ToString("####");
        

            if(bar.transform.localScale.x >= 1)
        {
          
            this.gameObject.SetActive(false);
            ChangeSceneWithButton.Instance.startGame();
           
        }
    }
    

    private void Start()
    {
        startLoading();
    }

    public void startLoading()
    {
        bar.transform.localScale = new Vector3(0,1,1);
        loadingText.text = "%" + "0";
        vignetteEfect = transform.Find("VignetteEfect").GetComponent<Image>();
        vignetteEfect.color = new Color(vignetteEfect.color.r, vignetteEfect.color.g, vignetteEfect.color.b, vignetteEfectVolue);

        if (backGroundImageAndLoop)
            StartCoroutine(transitionImage());
    }

    // The pictures change according to the time of
    IEnumerator transitionImage ()
    {
        for (int i = 0; i < backgroundImages.Length; i++)
        {
            yield return new WaitForSeconds(LoopTime);
            for (int j = 0; j < backgroundImages.Length; j++)
                backgroundImages[j].SetActive(false);
            backgroundImages[i].SetActive(true);           
        }
    }

    // Activate the scene 
    IEnumerator Loading (int sceneNo)
    {
        async = SceneManager.LoadSceneAsync(sceneNo);
        async.allowSceneActivation = false;

        // Continue until the installation is completed
        while (async.isDone == false)
        {
            bar.transform.localScale = new Vector3(async.progress,0.9f,1);

            if (loadingText != null)
                loadingText.text = "%" + (100 * bar.transform.localScale.x).ToString("####");

            if (async.progress == 0.9f)
            {
                bar.transform.localScale = new Vector3(1, 0.9f, 1);
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }

}
