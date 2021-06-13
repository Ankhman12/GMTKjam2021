using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealUI : MonoBehaviour
{
    public Canvas PauseCanvas;

    public Canvas GameOverCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnPause += Pause;
        GameManager.OnUnpause += Unpause;
        GameManager.OnGameOver += GameOver; 
    }

    void OnDestroy() {
        GameManager.OnPause -= Pause;
        GameManager.OnUnpause -= Unpause;
        GameManager.OnGameOver -= GameOver; 
    }

    public void Pause() {
        PauseCanvas.enabled = true;
    }

    public void Unpause() {
        PauseCanvas.enabled = false;
    }

    public void GameOver() {
        GameOverCanvas.enabled = true;
    }
}
