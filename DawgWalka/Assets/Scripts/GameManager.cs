using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { NullState, Intro, MainMenu, Game, Paused };

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public GameState gameState { get; private set; }

    //Other fields

    private GameManager()
    {
        //initialize other fields
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }
    }

    public void SetGameState(GameState state)
    {
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
            //...
        }
        if (state == GameState.Game)
        {
            //...
        }
        if (state == GameState.Paused)
        {
            //...
        }

    }

    private void Awake()
    {

        SceneManager.LoadScene("MainMenu");

    }

    private void Update()
    {
        //Do stuff
    }
}
