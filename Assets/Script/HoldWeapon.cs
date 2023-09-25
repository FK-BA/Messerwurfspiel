using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HoldWeapon : MonoBehaviour
{
    public GameObject KnifeOnPlayer;
    private SteamVR_Action_Boolean grabAction;

    private void Awake()
    {
        grabAction = SteamVR_Input.GetBooleanAction("GrabPinch");
    }

    void Start()
    {
        KnifeOnPlayer.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if boolean button is pressed
            if (grabAction.GetStateDown(SteamVR_Input_Sources.Any))
            {
                this.gameObject.SetActive(false);
                KnifeOnPlayer.SetActive(true);
            }
        }
    }
}


