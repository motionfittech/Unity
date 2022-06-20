using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ChartAndGraph;
using UnityEngine.UI;

public class NormalAnalysis : MonoBehaviour
{
    // Athlete 
    [SerializeField] private GameObject atheleteProfileBtn;
    [SerializeField] private GameObject scrollViewToWhichTheAthleteProfileButtonWillBeAdded;

    // Graph Types
    [SerializeField] private GameObject graphTypeBtn;
    [SerializeField] private GameObject scrollViewToWhichTheGraphTypeButtonWillBeAdded;

    // Barchart
    public BarChart barChart;

    // Piechart
    public PieChart pieChart;

    // Button List for Passing To Master Button Manager
    public List<GameObject> _normalAnalysisTimeFrameBtnList;

    // CSV Object
    private CSV cSV;

    private void Awake()
    {
        AddAthleteProfileButtonToScrollView(athleteName: "Kamran");
        AddGraphTypeButtonToScrollView("Yearly");
        AddGraphTypeButtonToScrollView("Monthly");
        AddGraphTypeButtonToScrollView("Weekly");
        AddGraphTypeButtonToScrollView("Daily");
        AddGraphTypeButtonToScrollView("Set Averages");
        AddGraphTypeButtonToScrollView("Per Repetition Of Movement");

    }

    private void Start()
    {
        cSV = FindObjectOfType<CSV>();
    }
    
    private void AddAthleteProfileButtonToScrollView(string athleteName)
    {
        GameObject tempAtheleteProfileBtn = Instantiate(atheleteProfileBtn);
        tempAtheleteProfileBtn.transform.SetParent(scrollViewToWhichTheAthleteProfileButtonWillBeAdded.transform, false);
        tempAtheleteProfileBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = athleteName;
    }

    private void AddGraphTypeButtonToScrollView(string timeLine)
    {
        GameObject tempGraphTypeBtn = Instantiate(graphTypeBtn);
        tempGraphTypeBtn.transform.SetParent(scrollViewToWhichTheGraphTypeButtonWillBeAdded.transform, false);
        tempGraphTypeBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = timeLine;
        tempGraphTypeBtn.name = timeLine;
        _normalAnalysisTimeFrameBtnList.Add(tempGraphTypeBtn);
    }

    public BarChart getBarChart()
    {
        barChart.gameObject.SetActive(false);
        pieChart.gameObject.SetActive(false);
        return barChart;
    }

    public PieChart getPieChart()
    {
        barChart.gameObject.SetActive(false);
        pieChart.gameObject.SetActive(false);
        return pieChart;
    }


}
