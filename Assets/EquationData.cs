using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class EquationData : MonoBehaviour
{


	//public BarChartFeed bcf;
	public GraphChartFeed gcf;
	public ExerDatabaseCsv EDC;
	public TextMeshProUGUI velocityAverageTxt, ForceTxt, WorkTxt, PowerTxt;


	[Header(" Velocity_Handler")]
	public TextMeshProUGUI VelocityLossTxt;
	public TextMeshProUGUI L_VelocityTxt;
	public Image VelocityArrowImage;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	public void callafter(List<float> speeds,bool _isSaving)
	{
		print("we callafter now " + speeds.Count);
		float tempAverage = returnAverage(speeds);
		float tempForce = returnForce(speeds, 5);
		float tempWork = returnWork(speeds, tempForce);
		float tempPower = returnPower(tempWork, speeds.Count);
		
		if (tempAverage.ToString().Length > 4)
		{
			velocityAverageTxt.text = tempAverage.ToString().Substring(0, 5) + " m/s";
		}
        else
        {
			velocityAverageTxt.text = tempAverage.ToString() + " m/s";

		}
		if (tempPower.ToString().Length > 4)
		{
			PowerTxt.text = tempPower.ToString().Substring(0, 6) + " P";
		}
		else
		{
			PowerTxt.text = tempPower.ToString() + " P";

		}

		if (tempForce.ToString().Length > 4)
		{
			ForceTxt.text = tempForce.ToString().Substring(0, 6) + " N";
		}
		else
		{
			ForceTxt.text = tempForce.ToString() + " N";

		}
		if (tempWork.ToString().Length > 4)
		{
			WorkTxt.text = tempWork.ToString().Substring(0, 6) + " J";
		}
		else
		{
			WorkTxt.text = tempWork.ToString() + " J";

		}


		

		//bcf.addbarSingleValue(tempAverage);
		//	gcf.Singcall(tempAverage);
		if (_isSaving)
		{
			print("saving data ");
			EDC.addData(0, tempAverage.ToString());
			EDC.addData(1, tempForce.ToString());
			EDC.addData(2, tempWork.ToString());
			EDC.addData(3, tempPower.ToString());
			EDC.GraphDataPoints.Add(tempAverage);
			EDC.GraphDataPoints.Add(tempPower);
			EDC.GraphDataPoints.Add(tempWork);
			EDC.GraphDataPoints.Add(tempForce);
		}
		if (speeds.Count > 1)
		{
			float returnLoss = returnVelocityLoss(speeds.Count - 2, speeds.Count - 1);
			if (returnLoss < 0)
			{
				L_VelocityTxt.text = " Velocity Loss";
				VelocityArrowImage.color = Color.red;
				VelocityArrowImage.transform.localEulerAngles = new Vector3(0,0,180);
			}
			else if (returnLoss == 0)
            {
				L_VelocityTxt.text = " Velocity Neutral";
				VelocityArrowImage.color = Color.yellow;
				VelocityArrowImage.transform.localEulerAngles = new Vector3(0, 0, 0);
			}
			else if(returnLoss > 0)
            {
				L_VelocityTxt.text = " Velocity Gain";
				VelocityArrowImage.color = Color.green;
				VelocityArrowImage.transform.localEulerAngles = new Vector3(0, 0, 0);
			}
			
			VelocityLossTxt.text = returnLoss.ToString() + " m/s";
		}
		speeds.Clear();

	}

	float returnAverage(List<float> speeds)
	{
		float averageTotal = 0;


		for (int i = 0; i < speeds.Count; i++)
		{
			averageTotal += speeds[i];

		}
		// Avelocity = TotalVelocity/TotalCount
		return Mathf.Abs(averageTotal / speeds.Count);
	}
	float returnForce(List<float> speeds, float mass)
	{
		float Totalacceleration = 0;

		for (int i = 0; i < speeds.Count; i++)
		{
			Totalacceleration += speeds[i];

		}
		// Force = Acceleration*Mass
		return Totalacceleration * mass;
	}

	float returnWork(List<float> finalVelocity, float Force)
	{

		float Vf = 0;
		for (int i = 0; i < finalVelocity.Count; i++)
		{
			Vf += finalVelocity[i];
		}
		// Displacemnt = 1/2(vi+vf)*t
		float Displacement = (Vf / 2);
		// Work = Force * Discplacement

		return Force * Displacement;


	}
	float returnPower(float work, float time)
	{
		return work / time;
	}
	float returnVelocityLoss(float SetA, float SetB)
	{
		return SetA - SetB;
	}
}
