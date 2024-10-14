using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class TransitionToSurvey : MonoBehaviour
{

    // TransitionToSurvey handles transitioning from the cannon to the survey area


    #region VARIABLES


    private CanvasGroup fadeCanvas;
    [SerializeField] private GameObject xrOrigin;
    private SurveyManager surveyManager;
    [SerializeField] private SurveyInfo surveyInfo;


    #endregion


    #region TRANSITION


    // Begins transition to survey process
    //--------------------------------------//
    public void BeginTransition()
    //--------------------------------------//
    {
        StartCoroutine(StartSurveyProcess());

    } // END BeginTransition


    // Starts the survey process
    //--------------------------------------//
    private IEnumerator StartSurveyProcess()
    //--------------------------------------//
    {
        surveyManager = FindObjectOfType<SurveyManager>();
        fadeCanvas = GetComponent<CanvasGroup>();

        yield return new WaitForSeconds(4f);

        fadeCanvas.LeanAlpha(1f, .25f);

        yield return new WaitForSeconds(.5f);

        xrOrigin.transform.position = new Vector3(71.5f, 22f, 20f);
        xrOrigin.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        yield return new WaitForSeconds(.5f);

        fadeCanvas.LeanAlpha(0f, .25f);

        yield return new WaitForSeconds(.5f);

        surveyManager.BeginSurvey(surveyInfo);

    } // END StartSurveyProcess


    #endregion


} // END TransitionToSurvey.cs
