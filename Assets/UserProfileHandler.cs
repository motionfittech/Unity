using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UserProfileHandler : MonoBehaviour
{
    [Header("Horizontal Bar")]
    public Transform Main;
    public Vector2 target;
	public List<GameObject> buttonList;
	public List<string> upperTxt;
	public List<string> buttonTxt;
    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;

    public bool detectSwipeAfterRelease = false;

    public float SWIPE_THRESHOLD = 20;

	public bool _ismove = false,_istap = false;
	private float offsetright, offsetleft;
    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(Main.localPosition.x,Main.localPosition.y);
		offsetright = (233 * 3)+target.x;
		offsetleft = -(233 * 3)+target.x;

		for(int i = 0; i < buttonList.Count; i ++)
        {
			buttonList[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = buttonTxt[i];
			buttonList[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = upperTxt[i];

		}

    }

    // Update is called once per frame
    void Update()
    {
		Main.localPosition = Vector2.Lerp(Main.localPosition, target, 10 * Time.deltaTime);

		Main.localPosition = new Vector3(Mathf.Clamp(Main.localPosition.x,offsetleft,offsetright),Main.localPosition.y);
		if (!_ismove)
			return;

       

		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				fingerUpPos = touch.position;
				fingerDownPos = touch.position;
			}

			//Detects Swipe while finger is still moving on screen
			if (touch.phase == TouchPhase.Moved)
			{
				if (!detectSwipeAfterRelease)
				{
					
					fingerDownPos = touch.position;
					DetectSwipe();
				}
			}

			//Detects swipe after finger is released from screen
			if (touch.phase == TouchPhase.Ended)
			{
				
				fingerDownPos = touch.position;
				
				DetectSwipe();
			}
		}
	}
	void DetectSwipe()
	{

		if (VerticalMoveValue() > SWIPE_THRESHOLD && VerticalMoveValue() > HorizontalMoveValue())
		{
			Debug.Log("Vertical Swipe Detected!");
			if (fingerDownPos.y - fingerUpPos.y > 0)
			{
				OnSwipeUp();
			}
			else if (fingerDownPos.y - fingerUpPos.y < 0)
			{
				OnSwipeDown();
			}
			fingerUpPos = fingerDownPos;

		}
		else if (HorizontalMoveValue() > SWIPE_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue())
		{
			Debug.Log("Horizontal Swipe Detected!");
			if (fingerDownPos.x - fingerUpPos.x > 0)
			{
				OnSwipeRight();
			}
			else if (fingerDownPos.x - fingerUpPos.x < 0)
			{
				OnSwipeLeft();
			}
			fingerUpPos = fingerDownPos;

		}
		else
		{
			_istap = true;
			Debug.Log("No Swipe Detected!");
		}
	}

	float VerticalMoveValue()
	{
		return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
	}

	float HorizontalMoveValue()
	{
		return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
	}

	void OnSwipeUp()
	{
		//Do something when swiped up
	}

	void OnSwipeDown()
	{
		//Do something when swiped down
	}

	void OnSwipeLeft()
	{
		//Do something when swiped left
		target.x += 233;
		_ismove = false;
		_istap = false;
	}

	void OnSwipeRight()
	{
		//Do something when swiped right
		target.x -= 233;
		_ismove = false;
		_istap = false;
	}

	public void swiprbt(bool conditioner)
    {

		
		_ismove = conditioner;
	}
	public void callbt()
    {
        if (_istap)
        {
			print("called button fn");
        }
    }
}
