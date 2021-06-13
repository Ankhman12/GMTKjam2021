using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpauseButton : MonoBehaviour
{
    public void UnPause() {
        GameManager.Instance.SetGameState(GameState.Game);
    }
}
