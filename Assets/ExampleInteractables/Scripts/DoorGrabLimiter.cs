using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class DoorGrabLimiter : MonoBehaviour
{

    // DoorGrabLimiter limits the distance a user can pull a door, as to prevent snapping off from hinge


    #region VARIABLES


    private XRGrabInteractable doorInteractable; // The XRGrabInteractable component on the door
    private float maxDistanceFromStart = .3f; // The maximum allowed distance before releasing the door

    private Vector3 startGrabPosition; // The initial position of the interactor when the door is grabbed
    private XRBaseInteractor currentInteractor; // The interactor that is currently grabbing the door
    private bool isGrabbing = false; // Flag to check if the door is being grabbed


    #endregion


    #region ENABLE / DISABLE


    // OnEnable, subscribe to grab events
    //--------------------------------------//
    private void OnEnable()
    //--------------------------------------//
    {
        doorInteractable = GetComponent<XRGrabInteractable>();

        doorInteractable.selectEntered.AddListener(OnGrab);
        doorInteractable.selectExited.AddListener(OnRelease);

    } // END OnEnable


    // OnDisable, unsubscribe from grab events
    //--------------------------------------//
    private void OnDisable()
    //--------------------------------------//
    {
        doorInteractable.selectEntered.RemoveListener(OnGrab);
        doorInteractable.selectExited.RemoveListener(OnRelease);
    
    } // END OnDisable


    #endregion


    #region UPDATE


    // Update, check distance if we are grabbing an object
    //--------------------------------------//
    private void Update()
    //--------------------------------------//
    {
        // Only check the distance if the door is currently being grabbed
        if (isGrabbing && currentInteractor != null)
        {
            // Get the current position of the interactor (controller)
            Vector3 currentPosition = currentInteractor.transform.position;

            // Calculate the distance between the starting grab position and the current position
            float distance = Vector3.Distance(startGrabPosition, currentPosition);

            // If the distance exceeds the maximum allowed value, release the door
            if (distance > maxDistanceFromStart)
            {
                ReleaseDoor();
            }
        }

    } // END Update


    #endregion


    #region GRAB / RELEASE


    // Called when the door is grabbed
    //--------------------------------------//
    private void OnGrab(SelectEnterEventArgs args)
    //--------------------------------------//
    {
        // Save the interactor (controller) that is grabbing the door
        currentInteractor = (XRBaseInteractor) args.interactorObject;

        // Save the initial position of the interactor when the door is grabbed
        startGrabPosition = currentInteractor.transform.position;

        // Set the grabbing flag to true
        isGrabbing = true;

    } // END OnGrab


    // Called when the door is released
    //--------------------------------------//
    private void OnRelease(SelectExitEventArgs args)
    //--------------------------------------//
    {
        // Reset the interactor and flag when the door is released
        currentInteractor = null;
        isGrabbing = false;

    } // END OnRelease


    // Manually release the door when the distance exceeds the limit
    //--------------------------------------//
    private void ReleaseDoor()
    //--------------------------------------//
    {
        if (currentInteractor != null)
        {
            // End the manual interaction and release the door
            currentInteractor.interactionManager.SelectExit((IXRSelectInteractor)currentInteractor, (IXRSelectInteractable)doorInteractable);

            // Reset the interactor and flag
            currentInteractor = null;
            isGrabbing = false;
        }

    } // END ReleaseDoor


    #endregion


} // END DoorGrabLimiter.cs
