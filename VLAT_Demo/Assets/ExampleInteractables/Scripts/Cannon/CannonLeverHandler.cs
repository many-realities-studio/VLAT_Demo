using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CannonLeverHandler : MonoBehaviour
{

    // CannonLeverHandler handles the lever of the cannon interactable


    #region VARIABLES


    [SerializeField] private CannonInteractable cannonInteractable;
    private bool grabActive = false;
    [SerializeField] private Transform controlSphere;
    private Transform targetTransform;


    #endregion


    #region TRIGGER STAY


    // OnTriggerStay, notify cannon
    //--------------------------------------//
    public void Update()
    //--------------------------------------//
    {
        if (grabActive)
        {
            cannonInteractable.OnRotatingCannon(transform);
        }

    } // END OnTriggerStay


    // OnSelect
    //--------------------------------------//
    public void OnSelect()
    //--------------------------------------//
    {
        grabActive = true;

    } // END OnSelect


    // OnEndSelect
    //--------------------------------------//
    public void OnEndSelect()
    //--------------------------------------//
    {
        grabActive = false;
        ResetGrabPos();

    } // END OnEndSelect


    // Resets grab position to control sphere
    //--------------------------------------//
    private void ResetGrabPos()
    //--------------------------------------//
    {
        transform.position = controlSphere.position;

    } // END ResetGrabPos


    #endregion


} // END CannonLeverHandler.cs
