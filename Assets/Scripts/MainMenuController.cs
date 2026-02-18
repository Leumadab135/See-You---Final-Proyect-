using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Button _startGameButton;
    [SerializeField] Button _exitGameButton;

    // Start is called before the first frame update
    void Start()
    {
        _startGameButton.onClick.AddListener(StartGame);
        _exitGameButton.onClick.AddListener(ExitGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("InGame");
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}