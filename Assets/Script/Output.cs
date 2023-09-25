using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime;
using System;
using UnityEngine.UIElements;
using System.Globalization;
using Valve.VR;
using Valve.VR.InteractionSystem;
using TMPro;

public class Output : MonoBehaviour
{
    public SteamVR_Action_Vector2 m_MoveValue = null;
    public GameObject Camera = null;
    public GameObject Controller = null;

    public TMP_InputField TMP_InputField;
    private String Name;
    private List<string[]> rowData = new List<string[]>();
    private SteamVR_Action_Boolean buttonPressed; // shoot button
    int Target_remaining;
    private int counter;

    private void Awake()
    {
        DateTime localDate = DateTime.Now;

        Name = System.DateTime.Now.ToString("HH-mm-ss");
        buttonPressed = SteamVR_Input.GetBooleanAction("GrabPinch");
    }

    void Start()
    {

        Target_remaining = 0;
        Targethealth[] Target = FindObjectsOfType<Targethealth>();
        Target_remaining = Target.Length;
        TMP_InputField.gameObject.SetActive(true);
        DateTime localDate = DateTime.Now;

        string[] rowDataTemp = new string[11];
        rowDataTemp[0] = "Timestamp";
        rowDataTemp[1] = "Target";
        rowDataTemp[2] = "X_Controller_Input";
        rowDataTemp[3] = "Y_Controller_Input";
        rowDataTemp[4] = "X_Head_Position";
        rowDataTemp[5] = "Y_Head_Position";
        rowDataTemp[6] = "Z_Head_Position";
        rowDataTemp[7] = "X_Head_Rotation";
        rowDataTemp[8] = "Y_Head_Rotation";
        rowDataTemp[9] = "Z_Head_Rotation";
        rowDataTemp[10] = "ShootButton";

        rowData.Add(rowDataTemp);

    }


    void Update()
    {
        if (TMP_InputField.interactable == false)
        { Save(); }
        if (Input.GetMouseButtonDown(1))
        {
            Application.Quit();
        }
       
        UpdateCrystalCount();
    }
    void UpdateCrystalCount()
    {
        if (TMP_InputField.interactable)
        {
            // If the input field is interactable, perform this block of code
            Targethealth[] Target = FindObjectsOfType<Targethealth>();
            Target_remaining = Target.Length;
        }
        else
        {
            // If the input field is not interactable, perform this block of code
            Targethealth[] Target = FindObjectsOfType<Targethealth>();
            if (Target.Length != Target_remaining)
            {
                counter++;
                Debug.Log(counter);
                Target_remaining = Target.Length;
                Debug.Log(Target.Length);
            }
        }
    }




    void Save()
    {
        string[] rowDataTemp = new string[11];
        rowDataTemp[0] = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fffff");//Time UCT+1
        rowDataTemp[1] = counter.ToString(); //Targets hit
        rowDataTemp[2] = m_MoveValue.axis.x.ToString();//Movement x Axis
        rowDataTemp[3] = m_MoveValue.axis.y.ToString();//Movement y Axis
        rowDataTemp[4] = Camera.transform.position.x.ToString();//x Position Head
        rowDataTemp[5] = Camera.transform.position.y.ToString();//y Position Head
        rowDataTemp[6] = Camera.transform.position.z.ToString();//z Position Head
        rowDataTemp[7] = Camera.transform.eulerAngles.x.ToString();//x Rotation
        rowDataTemp[8] = Camera.transform.eulerAngles.y.ToString();//y Rotation
        rowDataTemp[9] = Camera.transform.eulerAngles.z.ToString();//z RRotation 
        rowDataTemp[10] = buttonPressed.GetState(SteamVR_Input_Sources.Any) ? "pressed" : ""; //shoot button pressed

        rowData.Add(rowDataTemp);


        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = "/";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = GetPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();

    }


    string GetPath()
    {
        // Find the "Canvas" GameObject
        GameObject canvasObject = GameObject.Find("Canvas");

        // Get the ScoreManager component attached to it
        ScoreManager scoreManager = canvasObject.GetComponent<ScoreManager>();

        // Get the totalScore value
        int totalScore = scoreManager.totalScore;

#if UNITY_EDITOR
        return Application.dataPath + "/VR 2 Knife Training Game/" + DateTime.Now.ToLongDateString() + "_" + Name + "_" + TMP_InputField.text.ToString() + "_" + totalScore + ".csv";
#else
        return Application.dataPath + "/" + DateTime.Now.ToLongDateString() + "_" + Name + "_" + TMP_InputField.text.ToString() + "_" + totalScore + ".csv";
#endif
    }
}


