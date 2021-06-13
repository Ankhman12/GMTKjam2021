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
        GameManager.Instance.SetGameState(GameState.Game);
        SceneManager.LoadScene("ProgrammingScene", LoadSceneMode.Single);
        AudioManager.i.StartGame();
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
