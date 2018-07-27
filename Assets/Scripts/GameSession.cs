using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    [SerializeField] int playerLives = 3;
    [SerializeField] int coinPickupScore = 100;
    public int score;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Debug.Log("Destroyed additional GameSession");
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Prevented Destroying Game Session");
            DontDestroyOnLoad(this.gameObject);
        }       
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives <= 0)
        {
            ResetGameSession();
        }
        else
        {
            TakeLife();
        }
    }

    void TakeLife()
    {
        playerLives -= 1;
        livesText.text = playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ResetGameSession()
    {       
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }

    public void AddToScore(int points)
    {
        score += points;
    }

    public void PickedUpCoin()
    {
        AddToScore(coinPickupScore);
        scoreText.text = score.ToString();
    }
}
