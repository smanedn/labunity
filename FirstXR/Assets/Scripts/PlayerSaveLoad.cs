using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveLoad : MonoBehaviour
{
    private string saveFileName = "playerSave.json";

    [SerializeField] private string sceneToLoadAfterSave;

    [System.Serializable]
    private class SaveData
    {
        public float[] playerPosition;
        public string currentScene;
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        Vector3 pos = transform.position;
        data.playerPosition = new float[] { pos.x, pos.y, pos.z };
        data.currentScene = SceneManager.GetActiveScene().name;

        string json = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        File.WriteAllText(path, json);

        if (!string.IsNullOrEmpty(sceneToLoadAfterSave))
        {
            SceneManager.LoadScene(sceneToLoadAfterSave);
        }
    }

    public void LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (data.playerPosition != null && data.playerPosition.Length == 3)
            {
                transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
            }

            if (!string.IsNullOrEmpty(data.currentScene) && SceneManager.GetActiveScene().name != data.currentScene)
            {
                SceneManager.LoadScene(data.currentScene);
            }

        }
    }
}
