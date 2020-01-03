using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int score;
    public Text scoreText;
    public int highScore;
    public Text highScoreText;

    public GameObject gameOverPanel;
    public Text gameOverPanelScoreText;
    public Text gameOverPanelHighScoreText;

    public AudioClip[] sliceSounds;
    private AudioSource audioSource;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        GetHighScore();
    }

    private void GetHighScore()
    {
        highScore = PlayerPrefs.GetInt("Highscore", 0);
        highScoreText.text = "Best: " + highScore.ToString();
    }
    
    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();

        if(score >  highScore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            highScoreText.text = "Best: " + score.ToString();
        }
    }

    public void OnBombHit()
    {
        Time.timeScale = 0;

        GetHighScore();
        gameOverPanelScoreText.text = "Score: " + score.ToString();
        gameOverPanelHighScoreText.text = "Best: " + highScore.ToString();
        gameOverPanel.SetActive(true);

        Debug.Log("Bomb Hit");
    }

    public void RestartGame()
    {
        score = 0;
        scoreText.text = score.ToString();
        gameOverPanel.SetActive(false);

        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Destroy(g);
        }

        Time.timeScale = 1;
    }

    public void PlayRandomSliceSound()
    {
        AudioClip randomSound = sliceSounds[Random.Range(0, sliceSounds.Length)];
        audioSource.PlayOneShot(randomSound);
    }
}
