﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
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
