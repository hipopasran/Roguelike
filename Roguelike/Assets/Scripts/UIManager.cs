using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject homeMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private Button     startButton;
    [SerializeField] private Button     nextLevelButton;

    public Button StartButton => startButton;
    public Button NextLevelButton => nextLevelButton;


    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnInitGame     += InGame;
            GameManager.Instance.OnHome         += Home;
            GameManager.Instance.OnFinishLevel  += Win;
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnInitGame     -= InGame;
            GameManager.Instance.OnHome         -= Home;
            GameManager.Instance.OnFinishLevel  -= Win;
        }
    }

    private void InGame(int currentLevel)
    {
        homeMenu.SetActive(false);
        winMenu.SetActive(false);
    }

    private void Home()
    {
        homeMenu.SetActive(true);
        winMenu.SetActive(false);
    }

    private void Win()
    {
        winMenu.SetActive(true);
    }
}
