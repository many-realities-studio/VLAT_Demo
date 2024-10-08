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
    [SerializeField] private XRBaseInputInteractor rayInteractorRight, rayInteractorLeft;
    private Transform targetTransform;


    #endregion


    #region TRIGGER STAY


    // OnTriggerStay, notify cannon
    //--------------------------------------//
    public void Update()
    //--------------------------------------//
    {
        if (grabActive)
            cannonInteractable.OnRotatingCannon(targetTransform);

    } // END OnTriggerStay


    // OnSelect
    //--------------------------------------//
    public void OnSelect()
    //--------------------------------------//
    {
        if (rayInteractorLeft.interactablesSelected != null && rayInteractorLeft.interactablesSelected.Count > 0 && rayInteractorLeft.interactablesSelected[0] != null && rayInteractorLeft.interactablesSelected[0].transform.CompareTag("Button"))
        {
            targetTransform = rayInteractorLeft.transform;
            grabActive = true;
        }
        else if (rayInteractorRight.interactablesSelected != null && rayInteractorRight.interactablesSelected.Count > 0 && rayInteractorRight.interactablesSelected[0] != null && rayInteractorRight.interactablesSelected[0].transform.CompareTag("Button"))
        {
            targetTransform = rayInteractorRight.transform;
            grabActive = true;
        }

    } // END OnSelect


    // OnEndSelect
    //--------------------------------------//
    public void OnEndSelect()
    //--------------------------------------//
    {
        grabActive = false;

    } // END OnEndSelect


    #endregion


} // END CannonLeverHandler.cs
