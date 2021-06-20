using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Sideitem : MonoBehaviour
{
    Toggle currentToggle;
    WorkoutManager WM;
    private void Start()
    {
      currentToggle  = this.transform.GetChild(0).GetComponent<Toggle>();
        WM = GameObject.FindObjectOfType<WorkoutManager>();
    }
    // Start is called before the first frame update
    public void onValueChanged()
    {
        if (!currentToggle.isOn)
            return;

        if (!WM._Dowehaveanimation)
            WM._Dowehaveanimation = true;

        WM.CenterButtonExercisetxt.text = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        //print(this.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text +  " "+ currentToggle.isOn);
    }
}
