using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public string playerName;
    public int playerScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int playerScore;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.playerScore = playerScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            playerScore = data.playerScore;
        }
    }





}