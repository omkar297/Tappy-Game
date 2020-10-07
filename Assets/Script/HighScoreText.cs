using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighScoreText : MonoBehaviour
{
    Text highScore;
    private void OnEnable()
    {
        highScore = GetComponent<Text>();
        highScore.text = "HighScore: "+PlayerPrefs.GetInt("HighScore").ToString();
    }
}
//Design and Made by Rathod Studio