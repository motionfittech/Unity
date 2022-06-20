
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MasterButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject _atheleteProfileCreatonForm;
    [SerializeField] private GameObject _atheleteProfileCreatorBtn;
    [SerializeField] private GameObject _closeFormBtn;

    // Normal Analysis Buttons
    [SerializeField] private GameObject _nextDataPoint;
    [SerializeField] private GameObject _previousDataPoint;
    [SerializeField] private GameObject _nextExcercise;
    [SerializeField] private GameObject _previousExcercise;
    [SerializeField] private Button _closeAnalysisBtn;
    
    // Normal Analysis Data Point Text Mesh
    [SerializeField] private TextMeshProUGUI _dataPointTM;
    //private List<string> _dataPointsList = new List<string>();//{ "Force", "Power","Velocity", "Excercise Form", "Fatigue", "Muscle Imabalances" };
    private int currentDataPointIndex = 0;

    // Normal analysis object
    private NormalAnalysis normalAnalysis;

    // Analysis canvas
    public GameObject analysisPanel;

    // Normal analysis List of excercises
    [SerializeField] private TextMeshProUGUI _excerciseNameTM;
    public ApiResponse exerciseList = null;
    private Exercise currentExercise = null;
    private int currentExcerciseIndex = 0;
    private string dimensionButtonClicked;
   
    private void Start()
    {
        exerciseList = GetComponent<Api>().getExercises();

        _atheleteProfileCreatorBtn.GetComponent<Button>().onClick.AddListener(OpenAtheleteProfileCreationForm);
        _closeFormBtn.GetComponent<Button>().onClick.AddListener(CloseAtheleteProfileCreationForm);

        // Normal Analysis Listeners
        _nextDataPoint.GetComponent<Button>().onClick.AddListener(LoadNextDataPoint);
        _previousDataPoint.GetComponent<Button>().onClick.AddListener(LoadPreviousDataPoint);

        _nextExcercise.GetComponent<Button>().onClick.AddListener(LoadNextExcercise);
        _previousExcercise.GetComponent<Button>().onClick.AddListener(LoadPreviousExcercise);

        // Close analysis
        _closeAnalysisBtn.onClick.AddListener(() => CloseAnalysis());

        // Normal analysis object reference
        normalAnalysis = FindObjectOfType<NormalAnalysis>();

        // Setting default values
        currentExercise = exerciseList.getCurrent();
        _excerciseNameTM.text = currentExercise.name;
        _dataPointTM.text = currentExercise.getCurrent().name;
        InitGraphButtons();
    }

    private void InitGraphButtons()
    {
        for (int i = 0; i < normalAnalysis._normalAnalysisTimeFrameBtnList.Count; i++)
        {
            Button button = normalAnalysis._normalAnalysisTimeFrameBtnList[i].GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => currentExercise.getCurrent().Render(normalAnalysis, button.name));
            button.onClick.AddListener(() => dimensionButtonClicked = button.name);
            if (button.name == dimensionButtonClicked) // render on load
            {
                button.onClick.Invoke();
            }
        }
    }

    private void CloseAnalysis()
    {
        analysisPanel.SetActive(false);
    }

    private void LoadPreviousExcercise()
    {
        currentExercise = exerciseList.getPrevious();
        _excerciseNameTM.text = currentExercise.name;
        _dataPointTM.text = currentExercise.getCurrent().name;
        InitGraphButtons();
    }

    private void LoadNextExcercise()
    {
        currentExercise = exerciseList.getNext();
        _excerciseNameTM.text = currentExercise.name;
        _dataPointTM.text = currentExercise.getCurrent().name;
        InitGraphButtons();
    }

    private void DeActivateBarChart()
    {

        normalAnalysis.barChart.gameObject.SetActive(false);
        normalAnalysis.pieChart.gameObject.SetActive(true);
    }

    private void ActivateBarChart()
    {

        normalAnalysis.barChart.gameObject.SetActive(true);
        normalAnalysis.pieChart.gameObject.SetActive(false);
    }

    private void LoadPreviousDataPoint()
    {
        _dataPointTM.text = currentExercise.getPrevious().name;
        InitGraphButtons();
    }

    private void LoadNextDataPoint()
    {
        _dataPointTM.text = currentExercise.getNext().name;
        InitGraphButtons();
    }
    

    private void CloseAtheleteProfileCreationForm()
    {
        _atheleteProfileCreatonForm.SetActive(false);
    }

    private void OpenAtheleteProfileCreationForm()
    {
        if (_atheleteProfileCreatonForm.activeSelf)
        {
            _atheleteProfileCreatonForm.SetActive(false);
        }
        else
        {
            _atheleteProfileCreatonForm.SetActive(true);
        }
    }
}
