using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public string gameSceneName = "BasicScene";
    public string menuSceneName = "menu";

    public InputActionReference startGameAction;

    [SerializeField] private Button continueButton;

    private string playerSaveFile = "playerSave.json";
    private string pointsSaveFile = "pointsSave.json";

    void Start()
    {
        CheckForSavedGame();
    }

    void OnEnable()
    {
        if (startGameAction != null)
        {
            startGameAction.action.performed += OnStartGamePerformed;
            startGameAction.action.Enable();
        }
    }

    void OnDisable()
    {
        if (startGameAction != null)
        {
            startGameAction.action.performed -= OnStartGamePerformed;
            startGameAction.action.Disable();
        }
    }

    public void StartGame()
    {
        DeleteSaveFiles();
        LoadGameScene();
    }

    public void ContinueGame()
    {
        LoadGameScene();
    }

    public void ReturnMenu()
    {
        LoadMenuScene();
    }

    private void OnStartGamePerformed(InputAction.CallbackContext context)
    {
        StartGame();
    }

    private void LoadGameScene()
    {
        if (!string.IsNullOrEmpty(gameSceneName))
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }

    private void LoadMenuScene()
    {
        if (!string.IsNullOrEmpty(menuSceneName))
        {
            SceneManager.LoadScene(menuSceneName);
        }
    }

    private void CheckForSavedGame()
    {
        string playerPath = Path.Combine(Application.persistentDataPath, playerSaveFile);
        string pointsPath = Path.Combine(Application.persistentDataPath, pointsSaveFile);

        bool saveExists = File.Exists(playerPath) || File.Exists(pointsPath);

        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(saveExists);
        }
    }

    private void DeleteSaveFiles()
    {
        string playerPath = Path.Combine(Application.persistentDataPath, playerSaveFile);
        string pointsPath = Path.Combine(Application.persistentDataPath, pointsSaveFile);

        if (File.Exists(playerPath))
        {
            File.Delete(playerPath);
        }

        if (File.Exists(pointsPath))
        {
            File.Delete(pointsPath);
        }
    }
}
