using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    TextMesh text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMesh>();
        GameManager.OnGameOver += UpdateText;
    }

    // Update is called once per frame
    void UpdateText()
    {
        text.text = GameManager.Instance.score.ToString();
    }

    void OnDestroy() {
        GameManager.OnGameOver -= UpdateText;
    }
}
