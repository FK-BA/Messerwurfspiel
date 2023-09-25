using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{

    public float m_Gravity = 30.0f;
    public float m_Sensitivity = 0.1f;
    public float m_MaxSpeed = 1.0f;




    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;

    private float m_Speed = 0.0f;

    private CharacterController m_CharacterController = null;
    private Transform m_CameraRig = null;
    private Transform m_Head = null;



    private void Awake()

    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    private void Start()

    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
    }


    void FixedUpdate()
    {
       // HandleCenter();
        CalculateMovement();
        CalculateOrientation();

    }

    private void HandleCenter()
    {
        Vector3 newCenter = Vector3.zero;
        newCenter.x = m_Head.localPosition.x;
        newCenter.z = m_Head.localPosition.z;
        m_CharacterController.center = newCenter;

    }

    private void CalculateMovement()

    {
        // ignore out moevement orientation
        Quaternion orientation = CalculateOrientation();
        Vector3 movement = Vector3.zero;

        // If not moving
        if (m_MoveValue.axis.magnitude == 0)
            m_Speed = 0;


        // Add, clamp
        m_Speed += m_MoveValue.axis.magnitude * m_Sensitivity;
        m_Speed = Mathf.Clamp(m_Speed, -m_MaxSpeed, m_MaxSpeed);

        //Orientation and Gravity
        movement += orientation * (m_Speed * Vector3.forward);
        movement.y -= m_Gravity * Time.deltaTime;

        //Apply
        m_CharacterController.Move(movement);


    }


    private Quaternion CalculateOrientation()
    {

        float rotation = Mathf.Atan2(m_MoveValue.axis.x, m_MoveValue.axis.y);
        rotation *= Mathf.Rad2Deg;
        Vector3 orientationEuler = new Vector3(0, m_Head.eulerAngles.y + rotation, 0);
        return Quaternion.Euler(orientationEuler);

    }




}

//Code-Scource:
//      Andrew. (2019, 08. Mai). [01] [Unity] Basic Touchpad Locomotion for SteamVR 2.0 [Video]. YouTube. https://www.youtube.com/watch?v=QREKO1sf8b8
//      Andrew. (2019, 16.Mai). [02] [Unity] Basic Touchpad Locomotion for SteamVR 2.0. [Video]. YouTube. https://www.youtube.com/watch?v=q5ZXMBtgIKM&t=649s
//      Andrew. (2019, 31.Juli). [03] [Unity] Basic Touchpad Locomotion for SteamVR 2.0 [Video]. YouTube. https://www.youtube.com/watch?v=MvuI6fspGMM&t=345s
//      Andrew. (2019, 23.Oktober). [04] [Unity] Basic Touchpad Locomotion for SteamVR 2.0 [Video]. YouTube. https://www.youtube.com/watch?v=8bq_V4MlUdI&t=22s