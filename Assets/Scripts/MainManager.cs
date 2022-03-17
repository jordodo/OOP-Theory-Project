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
    public static MainManager Instance;
    public string currentName {get; private set;}
    [SerializeField] private float gravityModifier;

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
}
