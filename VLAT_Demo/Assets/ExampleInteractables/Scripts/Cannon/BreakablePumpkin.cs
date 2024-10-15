using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakablePumpkin : MonoBehaviour
{

    #region VARIABLES


    [SerializeField] private Rigidbody[] breakableRbs;
    private bool broken = false;
    [SerializeField] private UnityEvent onBreak;


    #endregion


    #region BREAK


    // Breaks the pumpkin
    //--------------------------------------//
    public void BreakPumpkin()
    //--------------------------------------//
    {
        if (!broken)
        {
            broken = true;

            FindObjectOfType<CannonInteractable>().PumpkinBroken();

            foreach (Rigidbody rb in breakableRbs)
            {
                rb.isKinematic = false;
                rb.gameObject.GetComponent<Collider>().enabled = true;
            }

            onBreak?.Invoke();
        }

    } // END BreakPumpkin


    // OnTriggerEnter, break the pumpkin
    //--------------------------------------//
    private void OnCollisionEnter(Collision collision)
    //--------------------------------------//
    {
        if (collision.gameObject.CompareTag("Projectile"))
            BreakPumpkin();        

    }


    #endregion


} // END BreakablePumpkin.cs
