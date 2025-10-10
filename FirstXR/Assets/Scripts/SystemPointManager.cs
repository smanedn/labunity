using System.IO;
using UnityEngine;
using TMPro;

public class SystemPointManager : MonoBehaviour
{
    public int totalCoinsNeeded = 5;
    public int currentCoins = 0;
    public float points = 100f;
    public float pointDecreaseRate = 1f;

    public TextMeshProUGUI textPunti;

    private bool gameActive = true;

    private string saveFileName = "pointsSave.json";

    void Start()
    {
        LoadGame();
        UpdateUI();
        StartCoroutine(PointDecreaseOverTime());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("moneta"))
        {
            currentCoins++;
            Destroy(other.gameObject);

            if (currentCoins >= totalCoinsNeeded)
            {
                gameActive = false;
            }

            UpdateUI();
            SaveGame();
        }
    }

    System.Collections.IEnumerator PointDecreaseOverTime()
    {
        while (gameActive)
        {
            yield return new WaitForSeconds(1f);

            if (currentCoins < totalCoinsNeeded)
            {
                points -= pointDecreaseRate;

                if (points < 0) points = 0;

                UpdateUI();
                SaveGame();
            }
        }
    }

    void UpdateUI()
    {
        if (textPunti != null)
        {
            textPunti.text = $"Punti: {points:F0}";
        }
    }

    [System.Serializable]
    private class SaveData
    {
        public int currentCoins;
        public float points;
    }

    public void SaveGame()
    {
        SaveData data = new SaveData
        {
            currentCoins = currentCoins,
            points = points
        };

        string json = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        File.WriteAllText(path, json);

    }

    public void LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            currentCoins = data.currentCoins;
            points = data.points;

        }
        else
        {
            currentCoins = 0;
            points = 100f;
        }
    }
}
