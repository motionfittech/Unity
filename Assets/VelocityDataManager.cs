using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class VelocityDataManager : MonoBehaviour
{
   public UnityEvent BarCallBack,GraphCallBack;
    
    public List<Toggle> TG = new List<Toggle>();

    private void Start()
    {
        TG[0].onValueChanged.AddListener(delegate {
            Tg0call(TG[0]);
        });
        TG[1].onValueChanged.AddListener(delegate {
            Tg1call(TG[1]);
        });

    }

    private void Update()
    {
        
    }

    public void Tg0call(Toggle temp)
    {

        if (temp.isOn)
        {
            
            BarCallBack.Invoke();
        }
    }
    public void Tg1call(Toggle temp)
    {

        if (temp.isOn)
        {

            GraphCallBack.Invoke();
        }
    }
}
