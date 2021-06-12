using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.i.StartJingle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        AudioManager.i.StartGame();
        SceneManager.LoadScene("ProgrammingScene", LoadSceneMode.Single);
    }

    public void Quit() {
        AudioManager.i.StartJingle();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
