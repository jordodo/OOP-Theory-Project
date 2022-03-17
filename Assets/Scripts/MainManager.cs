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


public class MainManager : MonoBehaviour
{
    [SerializeField] private Mage mage;
    [SerializeField] private Warrior warrior;
    [SerializeField] private Thief thief;
    Vector3 spawnPos = new Vector3(-10, 1, 0);
    public static MainManager Instance;
    public string currentName {get; private set;}
    public string currentClass;
    [SerializeField] private float gravityModifier;
    [SerializeField] public string currentCheckMarkName;
    [SerializeField] public GameObject currentCheckMark;
    [SerializeField] public GameObject testMark;
    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }



        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        gravityModifier = 2;
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCharacter(string name)
    {
        if (name == "Mage")
        {
            Instantiate(mage, spawnPos, mage.transform.rotation);
        }

        if (name == "Warrior")
        {
            print("You picked Warrior");
            Instantiate(warrior, spawnPos, warrior.transform.rotation);
        }

        if (name == "Thief")
        {
            Instantiate(thief, spawnPos, thief.transform.rotation);
        }

    }

    //Enables OnSceneLoad
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Called whenever the scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //print("A scene loaded");
        if (scene.name == "Main" && currentClass != null)
        {
            //print("Main Scene loaded");
            SpawnCharacter(currentClass);
        }

        if (scene.name == "Menu")
        {
            if (currentCheckMark == null && !string.IsNullOrEmpty(currentCheckMarkName))
            {
                //print("Finding Checkmark: " + currentCheckMarkName);
                currentCheckMark = GameObject.Find(currentCheckMarkName);
                //print(currentCheckMark);
                currentCheckMark.GetComponent<Image>().enabled = true;
            }
        }
    }

    //Disables OnSceneLoad
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    } 

}
