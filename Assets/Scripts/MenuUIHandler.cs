using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{

    public Text highestScoreText;
    // Start is called before the first frame update

    void Awake()
    {
        ScoreManager.Instance.LoadScore();
        highestScoreText.text = "Highest Score: " + ScoreManager.Instance.playerName + " " + ScoreManager.Instance.playerScore;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        // MainManager.Instance.SaveColor();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    public void ResetScore()
    {
        ScoreManager.Instance.playerName = " ";
        ScoreManager.Instance.playerScore = 0;
        ScoreManager.Instance.SaveScore();
        SceneManager.LoadScene(0);



    }
}
