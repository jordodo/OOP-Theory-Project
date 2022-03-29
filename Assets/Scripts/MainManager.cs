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
    //Variables and object references
    [SerializeField] private Mage mage;
    [SerializeField] private Warrior warrior;
    [SerializeField] private Thief thief;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI timerText;
    private Vector3 spawnPos = new Vector3(-7.5f, 1, 0);
    private float gravityModifier;
    private float currentTime;
    private string currentClass;

    //ENCAPSULATION
    [SerializeField] public string currentCheckMarkName {get; private set;}
    [SerializeField] public GameObject currentCheckMark {get; private set;}
    public bool gameOver {get; private set;} = true;
    public static MainManager Instance {get; private set;}
    
    

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
    private void Start()
    {
        currentTime = 0;
        gravityModifier = 2;
        gameOver = true;
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!gameOver)
        {
            Timer();
        }
    }

    public void SetCurrentClass(GameObject button, GameObject checkMark)
    {
        currentClass = button.name;
        currentCheckMark = checkMark;
        currentCheckMarkName  = checkMark.name;
    }

    public void GameOver(string textToShow)
    {
            gameOver = true;
            gameOverScreen.SetActive(true);
            TextMeshProUGUI gameOverText = gameOverScreen.transform.Find("GameOver Text").GetComponent<TextMeshProUGUI>();

            gameOverText.text = textToShow;


            DestroyProjectiles();
    }

    private void StartGame()
    {
        if (currentClass != null)
        {
            SpawnCharacter(currentClass);
            gameOver = false;
            currentTime = 0;

            timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
            timerText.text = "Time: "  + currentTime;
            
            gameOverScreen = GameObject.Find("GameOver");
            gameOverScreen.SetActive(false); 
        }
        else
        {
            GameOver("No Class was selected, something went wrong");
        }
            
    }

    private void SpawnCharacter(string name)
    {
        if (name == "Mage")
        {
            Instantiate(mage.gameObject, spawnPos, mage.transform.rotation);
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

    private void Timer()
    {
        currentTime += Time.deltaTime;
        int timeToDisplay = Mathf.FloorToInt(currentTime);
        timerText.text = "Time: " + timeToDisplay;
    }

    private void DestroyProjectiles()
    {
        GameObject[] activeProjectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach(GameObject activeProjectile in activeProjectiles)
        GameObject.Destroy(activeProjectile);       
    }

    //Enables OnSceneLoad
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Called whenever the scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main" && currentClass != null)
        {
            StartGame();
        }

        if (scene.name == "Menu")
        {
            //instructionsText = GameObject.Find("Instructions").GetComponent<TextMeshProUGUI>();

            if (currentCheckMark == null && !string.IsNullOrEmpty(currentCheckMarkName))
            {
                currentCheckMark = GameObject.Find(currentCheckMarkName);
                currentCheckMark.GetComponent<Image>().enabled = true;
            }
        }
    }

    //Disables OnSceneLoad
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    } 

}
