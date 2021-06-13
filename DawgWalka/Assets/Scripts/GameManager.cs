using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { NullState, Intro, MainMenu, Game, Paused, GameOver };

public delegate void Action();
public delegate void ActionRef<T>(ref T item);
public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public GameState gameState = GameState.MainMenu;

    public int score = 0;
    public int scorePerTrick = 25;

    //Other fields
    public static event Action OnPause;
    public static event Action OnUnpause;
    public static event Action OnGameOver;

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
        if(SceneManager.GetActiveScene().name == "MainMenu") {
            SetGameState(GameState.MainMenu);
        } else if(SceneManager.GetActiveScene().name == "ProgrammingScene" || SceneManager.GetActiveScene().name == "WillTestingScene") {
            SetGameState(GameState.Game);
        }
        PlayerMovement.OnTrick += OnTrick;
    }

    private void Update()
    {
        //Do stuff
        
    }
    
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

    //this is here in case we need to reset other stuff
    public void Reset() {
        score = 0;
    }

    public void OnTrick() {
        score += scorePerTrick;
    }
}
