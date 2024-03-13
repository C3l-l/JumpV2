using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    //public Text highscoreText;

    int score = 0;
    //int highscore = 0;
    //int maxPlatform = 0;
    

    private void Awake(){
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        //highscore = PlayerPrefs.GetInt("highscore", 0);
        score = PlayerPrefs.GetInt("score",0);
        scoreText.text = "SCORE: " + score.ToString();
        //highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }



    public void AddPoint()
    {
        if (Time.timeScale == 1)
        {
            //maxPlatform += 1;
            score += 1;
            scoreText.text = "SCORE: " + score.ToString();
            //scoreText.text = "SCORE: " + maxPlatform.ToString();
            PlayerPrefs.SetInt("score", score);
            //PlayerPrefs.SetInt("score", maxPlatform);
        }
        else
        {
            score +=0;
            //maxPlatform += 0;
        }
    }
}