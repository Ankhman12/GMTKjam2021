using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        SceneManager.LoadScene("ProgrammingScene", LoadSceneMode.Single);
        GameManager.Instance.SetGameState(GameState.Game);
        ZombieHorde.distance = 1;
        AudioManager.i.StartGame();
    }

    public void MainMenu() {
        GameManager.Instance.SetGameState(GameState.MainMenu);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        AudioManager.i.StartJingle();
    }

    public void Quit() {
        AudioManager.i.StopJingle();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
