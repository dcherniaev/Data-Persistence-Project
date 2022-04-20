using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public GameObject GameOverText;
    public GameObject newHighScore;
    public InputField mainInputField;
    public GameObject restartButton;
    public GameObject backToMenuButton;



    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;


    // Checks if there is anything entered into the input field.
    void LockInput(InputField input)
    {
        if (input.text.Length > 0)
        {
            Debug.Log("Text has been entered");
            ScoreManager.Instance.playerName = input.text;
        }
        else if (input.text.Length == 0)
        {
            Debug.Log("Main Input Empty");
        }
    }

    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        //Adds a listener that invokes the "LockInput" method when the player finishes editing the main input field.
        //Passes the main input field into the method when "LockInput" is invoked
        mainInputField.onEndEdit.AddListener(delegate { LockInput(mainInputField); });
        
        bestScoreText.text = "Highest Score: " + ScoreManager.Instance.playerName + " " + ScoreManager.Instance.playerScore;

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
       
        
    }



    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        if (m_Points > ScoreManager.Instance.playerScore)
        {
            newHighScore.SetActive(true);
            ScoreManager.Instance.playerScore = m_Points;
            ScoreManager.Instance.SaveScore();
        } else
        {
            restartButton.SetActive(true);
            backToMenuButton.SetActive(true);
        }

    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveAndRestartGame()
    {
        ScoreManager.Instance.SaveScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveAndBackToMenu()
    {
        ScoreManager.Instance.SaveScore();
        SceneManager.LoadScene(0);
    }

}
