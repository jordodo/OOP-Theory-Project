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
    //[SerializeField] private MainManager mainManager;
    [SerializeField] private GameObject currentButton;
    [SerializeField] private Transform currentCheckmark;

    // Start is called before the first frame update
    void Start()
    {
        //mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseClass()
    {
        currentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        currentCheckmark = currentButton.transform.Find(currentButton.name + " Check");

        MainManager.Instance.currentClass = currentButton.name;
        MainManager.Instance.currentCheckMark = currentCheckmark.gameObject;
        MainManager.Instance.currentCheckMarkName  = currentCheckmark.gameObject.name;
        print(MainManager.Instance.currentClass);

    }

    public void ToGame()
    {
        if (MainManager.Instance.currentCheckMark != null && !string.IsNullOrEmpty(MainManager.Instance.currentCheckMarkName))
        {
           SceneManager.LoadScene(1);  
        }
        else
        {
            MainManager.Instance.instructionsText.text = "Please select a Class first";
        }    
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);    
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
