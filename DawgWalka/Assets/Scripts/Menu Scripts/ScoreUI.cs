using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = GameManager.Instance.score.ToString();
    }
}
