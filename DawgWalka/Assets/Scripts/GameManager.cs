using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { NullState, Intro, MainMenu, Game, Paused, GameOver };

public delegate void Action();
public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public GameState gameState = GameState.MainMenu;

    //Other fields
    public static event Action OnPause;
    public static event Action OnUnpause;
    public static event Action OnGameOver;

    public void SetGameState(GameState state)
    {
        if(gameState == GameState.Paused && state != GameState.Paused) {
            OnUnpause?.Invoke();
        } 
        this.gameState = state;
        if (state == GameState.NullState) 
        { 
            //...
        }
        if (state == GameState.Intro)
        {
            //...
        }
        if (state == GameState.MainMenu)
        {
            
        }
        if (state == GameState.Game)
        {
            //...
        }
        if (state == GameState.Paused)
        {
            OnPause?.Invoke();
        }
        if (state == GameState.GameOver)
        {
            OnGameOver?.Invoke();
        }
    }

    private void Awake()
    {
        if(Instance != null) {
            Debug.Log("Destroying");
            GameObject.Destroy(this);
        } else {
            Instance = this;
        }
        DontDestroyOnLoad(this);

    }

    private void Start() {
        Debug.Log(gameState);
        if(SceneManager.GetActiveScene().name == "MainMenu") {
            SetGameState(GameState.MainMenu);
        } else if(SceneManager.GetActiveScene().name == "ProgrammingScene") {
            SetGameState(GameState.Game);
        }
        Debug.Log(gameState);
    }

    private void Update()
    {
        //Do stuff
        
    }
}
