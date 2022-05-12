using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CVE2_EffectChanger : MonoBehaviour
{
    public Text text;
    private int currIndx = 0;

    void Start()
    {
        foreach (Transform c in transform)
            c.gameObject.SetActive(false);
        transform.GetChild(currIndx).gameObject.SetActive(true);
        text.text = (currIndx + 1) + "/" + transform.childCount + " effects\r\n(visit the rest scenes to see more effects/uses of effects)";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currIndx > 0)
                SetNewActive(currIndx - 1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currIndx < transform.childCount - 1)
                SetNewActive(currIndx + 1);
        }
    }

    void SetNewActive(int activeIndx)
    {
        transform.GetChild(currIndx).gameObject.SetActive(false);
        transform.GetChild(activeIndx).gameObject.SetActive(true);
        currIndx = activeIndx;
        text.text = (currIndx + 1) + "/" + transform.childCount + " effects\r\n(visit the rest scenes to see more effects/uses of effects)";
    }
}
