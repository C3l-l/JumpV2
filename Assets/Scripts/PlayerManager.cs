using System.Diagnostics;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    // display game over screen
    public static bool isGameOver;
    public static bool isLevelComplete;
    public GameObject gameOverScreen;
    public GameObject pauseMenuScreen;
    private Countdown myCountdown;

    public static int numberOfFruits;
    public TextMeshProUGUI fruitsText;

    //public PlayfabManager playfabManager;
    //public int maxPlatform = 0;

    private void Awake()
    {
        UnityEngine.Debug.Log("PlayerManager Awake called");
        //numberOfFruits = PlayerPrefs.GetInt("NumberOfFruits", 0);
        fruitsText = GameObject.Find("Fruits Text").GetComponent<TextMeshProUGUI>();
        fruitsText.SetText(numberOfFruits.ToString());
        isGameOver = false;
        isLevelComplete = false;
        //playfabManager = FindObjectOfType<PlayfabManager>();
        myCountdown = GetComponent<Countdown>();
        if (numberOfFruits < 0)
        {
            numberOfFruits = 0;
        }
        UnityEngine.Debug.Log("Fruits Text: " + fruitsText);
    }


    private void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
            //StartCoroutine(playfabManager.SendLeaderboardCoroutine(maxPlatform));
        }
    }

    // Update the number of fruits and update the UI text
    public void UpdateFruits(int amount)
    {
        numberOfFruits += amount;
        if (numberOfFruits < 0)
        {
            numberOfFruits = 0;
        }
        fruitsText.text = numberOfFruits.ToString();
        UnityEngine.Debug.Log("UpdateFruitsText called");

    }

    // replay level again
    public void ReplayLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    // pause button functions

    // pause game 
    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuScreen.SetActive(true);
    }

    // resume game button
    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuScreen.SetActive(false);
        myCountdown.enabled = true;
    }

    // go back to home button 
    public void GoToMenu()
    {
        SceneManager.LoadScene("Start");
    }

    //public void NextLevel()
    //{
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Time.timeScale = 1;
        //myCountdown.enabled = true;
        //playfabManager.SendLeaderboardCoroutine(maxPlatform);
    //}
}
