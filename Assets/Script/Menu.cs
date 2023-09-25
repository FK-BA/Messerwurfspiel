using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public TMP_InputField TMP_InputField;
    public float alpha;
    public GameObject Canvas;
    public CanvasGroup canvasGroup;

    void Start() 
    {
        canvasGroup = Canvas.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            TMP_InputField.interactable = false;
            TMP_InputField.gameObject.SetActive(false);
         
            return;
        }
    }
}