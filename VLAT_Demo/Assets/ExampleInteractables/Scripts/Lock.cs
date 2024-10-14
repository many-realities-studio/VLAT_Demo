using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lock : MonoBehaviour
{


    #region VARIABLES


    [SerializeField] private Transform linkedKey;
    [SerializeField] private TMP_Text doorDisplayText;
    [SerializeField] private HingeJoint leftDoorHinge, rightDoorHinge;
    [SerializeField] private ParticleSystem keyUnlockParticles;

    private float leftHingeMin, leftHingeMax, rightHingeMin, rightHingeMax;
    private bool currentlyLocked = true;


    #endregion


    #region MONOBEHAVIOUR


    // Start
    //--------------------------------------//
    private void Start()
    //--------------------------------------//
    {
        doorDisplayText.text = "The castle gate is locked!\nThere should be a key nearby... Find it and unlock the gate!";
        LockDoors();

    } // END Start


    // Update
    //--------------------------------------//
    private void Update()
    //--------------------------------------//
    {
        if (currentlyLocked)
            if ((transform.position - linkedKey.position).magnitude < 0.5f)
                Unlock();

    } // END Update


    #endregion


    #region LOCK / UNLOCK


    // Tries to manually unlock the door via VLAT controls
    //--------------------------------------//
    public void TryUnlock()
    //--------------------------------------//
    {
        if (currentlyLocked)
        {
            GrabTracker grabTracker = FindObjectOfType<GrabTracker>();

            if (grabTracker.grabbedObject != null && grabTracker.grabbedObject.transform == linkedKey)
                Unlock();
        }

    } // END TryUnlock


    // Locks the doors by setting their limits to their current value
    //--------------------------------------//
    private void LockDoors()
    //--------------------------------------//
    {
        // Save old limits
        leftHingeMin = leftDoorHinge.limits.min;
        leftHingeMax = leftDoorHinge.limits.max;
        rightHingeMin = rightDoorHinge.limits.min;
        rightHingeMax = rightDoorHinge.limits.max;

        // Set left door limits
        JointLimits limits = leftDoorHinge.limits;
        limits.min = leftDoorHinge.angle;
        limits.max = leftDoorHinge.angle;
        leftDoorHinge.limits = limits;

        // Set right door limits
        limits = rightDoorHinge.limits;
        limits.min = rightDoorHinge.angle;
        limits.max = rightDoorHinge.angle;
        rightDoorHinge.limits = limits;

    } // END LockDoors


    // Unlocks door by resetting door limits to normal
    //--------------------------------------//
    public void Unlock()
    //--------------------------------------//
    {
        currentlyLocked = false;

        doorDisplayText.text = "The castle gate is unlocked!\nOpen the door and head upstairs to the ramparts!";

        // Set left door limits
        JointLimits limits = leftDoorHinge.limits;
        limits.min = leftHingeMin;
        limits.max = leftHingeMax;
        leftDoorHinge.limits = limits;

        // Set right door limits
        limits = rightDoorHinge.limits;
        limits.min = rightHingeMin;
        limits.max = rightHingeMax;
        rightDoorHinge.limits = limits;

        // Destroy lock and key, and spawn some particles
        transform.LeanScale(Vector3.zero, .5f).setEaseInExpo();
        linkedKey.LeanScale(Vector3.zero, .5f).setEaseInExpo();
        GameObject.Destroy(GameObject.Instantiate(keyUnlockParticles, transform.position, Quaternion.identity), 2f);
        GameObject.Destroy(GameObject.Instantiate(keyUnlockParticles, linkedKey.position, Quaternion.identity), 2f);
        GameObject.Destroy(linkedKey.gameObject, 2f);

    } // END Unlock


    #endregion


} // END LockedDoor.cs
