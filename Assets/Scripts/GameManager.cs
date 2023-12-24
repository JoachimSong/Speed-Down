using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    static GameManager instance;
    public Text timeScore;
    public GameObject gameOverPanel;
    public GameObject gamePausePanel;
    public bool gamePause;
    private float speedUp;
    private void Start()
    {
        gamePause = false;
        speedUp = 25f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameOverPanel.activeSelf)
            {
                gamePause = !gamePause;
                GamePause(gamePause);
            }
        }
        timeScore.text = Time.timeSinceLevelLoad.ToString("00");
    }
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
    }

    public static void GameOver(bool dead)
    {
        if (dead)
        {
            instance.gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public static void GamePause(bool pause)
    {
        if (pause)
        {
            instance.gamePausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            instance.gamePausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void ReturnGame()
    {
        gamePause = !gamePause;
        GamePause(gamePause);
    }

    public static float VelocityRate()
    {
        return 1 + (Time.timeSinceLevelLoad / instance.speedUp);
    }
}
