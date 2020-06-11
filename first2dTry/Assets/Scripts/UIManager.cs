using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{

    public int totalScore;

    public Text scoreText;
    public GameObject gameOver;
    public GameObject gameStop;
    public GameObject dialog;
    public AudioMixer audioMixer;

    public static UIManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreText() {
        this.scoreText.text = totalScore.ToString();
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }
    public void ResumeGame()
    {
        gameStop.SetActive(false);
        Time.timeScale = 1f;
        BGMManager.instance.ContinueBGM();
    }
    public void PauseGame()
    {
        gameStop.SetActive(true);
        Time.timeScale = 0f;
        BGMManager.instance.PauseBGM();

    }
    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowDialog()
    {
        dialog.SetActive(true);
    }
    public  void CloseDialog()
    {
        dialog.SetActive(false);
    }
    
}
