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
    private Vector3 spawnPos = new Vector3(-7.5f, 1, 0);
    public static MainManager Instance;
    public string currentClass;
    public bool gameOver {get; private set;} = true;
    [SerializeField] private float gravityModifier;
    [SerializeField] public string currentCheckMarkName;
    [SerializeField] public GameObject currentCheckMark;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] public TextMeshProUGUI instructionsText;
    private float currentTime;
    

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
        currentTime = 0;
        gravityModifier = 2;
        gameOver = true;
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            Timer();
        }
    }

    public void GameOver(bool playerWin)
    {
            gameOver = true;
            gameOverScreen.SetActive(true);
            TextMeshProUGUI gameOverText = gameOverScreen.transform.Find("GameOver Text").GetComponent<TextMeshProUGUI>();
            if (playerWin)
            {
                gameOverText.text = "Game Over: You Win!";
            }
            else
            {
                gameOverText.text = "Game Over: You Died";
            }

            DestroyProjectiles();
    }

    private void StartGame()
    {
            SpawnCharacter(currentClass);
            gameOver = false;
            currentTime = 0;

            timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
            timerText.text = "Time: "  + currentTime;
            
            gameOverScreen = GameObject.Find("GameOver");
            gameOverScreen.SetActive(false);
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

    public void Timer()
    {
        print("in Timer");
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
            StartGame();
        }

        if (scene.name == "Menu")
        {
            instructionsText = GameObject.Find("Instructions").GetComponent<TextMeshProUGUI>();

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
