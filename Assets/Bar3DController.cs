using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bar3DController : MonoBehaviour
{

   public Transform obj;
    public float currentXvalue;
    public bool _isRightDown = false;
    public bool _isLeftDown = false;
    float tempvalueX;
    public Slider CurrentSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRightDown)
        {
            tempvalueX += 0.05f;
            obj.position += new Vector3(currentXvalue, 0, 0);
            CurrentSlider.value = tempvalueX;
        }
        else if (_isLeftDown)
        {
            tempvalueX -= 0.05f;
            obj.position += new Vector3(currentXvalue, 0, 0);
            CurrentSlider.value = tempvalueX;
        }

        tempvalueX = Mathf.Clamp(tempvalueX,0,13f);
        obj.position = new Vector3(tempvalueX,0,0);
       
    }

    public void btDown()
    {
        _isRightDown = true;
    }
    public void btUp()
    {
        _isRightDown = false;
    }
    public void btDownleft()
    {
        _isLeftDown = true;
    }
    public void btUpleft()
    {
        _isLeftDown = false;
    }
}
