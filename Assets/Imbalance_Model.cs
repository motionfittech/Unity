using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Imbalance_Model : MonoBehaviour
{
    float smooth = 5.0f;
    float tiltAngle = 1;
    public float RightHandPre = 50 , LeftHandPre = 50;
    public TextMeshProUGUI RightHandPretxt, LeftHandPretxt;
    float horixontalZ = 0;
    float horixontalX = 0;
    void Update()
    {
        // Smoothly tilts a transform towards a target rotation.
      float   tiltAroundZ = horixontalZ * tiltAngle;
      float   tiltAroundX = horixontalX * tiltAngle;
        RightHandPretxt.text = RightHandPre.ToString() + "%";
        LeftHandPretxt.text = LeftHandPre.ToString() + "%";
        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(this.transform.eulerAngles.x, this.transform.eulerAngles.y,horixontalZ);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
    }

    public void RightHandDownBt()
    {
        if(RightHandPre >= 100)
        {
            RightHandPre = 100;
            LeftHandPre = 0;
            return;
        }

        horixontalZ += 0.5f;
        RightHandPre += 1;
        LeftHandPre -= 1;
    }
    public void LeftHandDownBt()
    {
        if (LeftHandPre >= 100)
        {
            LeftHandPre = 100;
            RightHandPre = 0;
            return;
        }

        horixontalZ -= 0.5f;
        RightHandPre -= 1;
        LeftHandPre += 1;
    }
}
