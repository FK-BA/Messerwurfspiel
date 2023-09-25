using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;

public class Throwing : MonoBehaviour
{
    [Header("References")]
    public Transform attackPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public SteamVR_Input_Sources handType = SteamVR_Input_Sources.RightHand; 
    public SteamVR_Action_Boolean throwAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "GrabGrip"); 
    public float throwForce;
    public float throwUpwardForce;
    public SteamVR_Behaviour_Pose pose; 

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
    }

    private void Update()
    {

        if (throwAction.GetStateDown(handType) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, pose.transform.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        Vector3 forceDirection = pose.transform.forward;
        Vector3 forceToAdd = forceDirection * throwForce + pose.transform.up * throwUpwardForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        Invoke(nameof(ResetThrow), throwCooldown);
    }
    private void ResetThrow()
    {
        readyToThrow = true;
    }
}

//Code-Scource: Dave / Game Development. (2022, 29. Januar). THROWING Grenades, Knives and Other Objects - Unity Tutorial [Video]. YouTube. https://www.youtube.com/watch?v=F20Sr5FlUlE&t=382s