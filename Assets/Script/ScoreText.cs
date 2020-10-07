using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    Text Score;
    private void OnEnable()
    {
        Score = GetComponent<Text>();
        Score.text = "Score: " + GameManager.Instance.Score;
    }
}
