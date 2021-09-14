using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LowPolySideMenu : MonoBehaviour
{


    public Toggle menuToggle;
    public AnimationCurve menuFadeInOutCurve;
    public RectTransform mainMenuRectTransform;
    public RectTransform SidePrefabclone,ParentHead;
    public UIDataBase UID;
    int totalcount = 0;
    // Start is called before the first frame update
    void Start()
    {
      
        menuToggle.onValueChanged.AddListener((x) => StartCoroutine(SlideAway(menuFadeInOutCurve, mainMenuRectTransform, 1, x)));

        totalcount = UID.character.Count;
        sidePrefab();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeSideMenu()
    {
        StartCoroutine(SlideAway(menuFadeInOutCurve, mainMenuRectTransform, 1, false));
    }

    IEnumerator SlideAway(AnimationCurve curve, RectTransform objectToMove, float duration, bool isForward)
    {
        float elapsedTime = 0;
        var endPos = isForward ? new Vector2(objectToMove.anchoredPosition.x + objectToMove.rect.width, objectToMove.anchoredPosition.y) :
            new Vector2(objectToMove.anchoredPosition.x - objectToMove.rect.width, objectToMove.anchoredPosition.y);

        while (elapsedTime < duration)
        {
            objectToMove.anchoredPosition = Vector2.LerpUnclamped(objectToMove.anchoredPosition, endPos, curve.Evaluate((elapsedTime / duration)));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectToMove.anchoredPosition = endPos;
    }

    public void sidePrefab()
    {
        for (int i = 0; i < totalcount; i++)
        {
            RectTransform clone = Instantiate(SidePrefabclone, Vector3.zero, Quaternion.identity);
            clone.SetParent(ParentHead);
            clone.gameObject.SetActive(true);
            clone.gameObject.name = i.ToString();
            clone.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = i.ToString();
           
        }


    }

   
}
