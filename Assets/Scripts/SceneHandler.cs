using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneHandler : MonoBehaviour
{
    private GameObject currentButton;
    private Transform currentCheckmark;
    [SerializeField] private TextMeshProUGUI instructionsText;


    private void ChooseClass()
    {
        currentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        currentCheckmark = currentButton.transform.Find(currentButton.name + " Check");

        MainManager.Instance.SetCurrentClass(currentButton, currentCheckmark.gameObject);

    }

    private void ToGame()
    {
        if (MainManager.Instance.currentCheckMark != null && !string.IsNullOrEmpty(MainManager.Instance.currentCheckMarkName))
        {
           SceneManager.LoadScene(1);  
        }
        else
        {
            instructionsText.text = "Please select a Class first";
        }    
    }

    private void ToMenu()
    {
        SceneManager.LoadScene(0);    
    }

    private void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
