using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action<int> OnInitGame;
    public event Action<int> OnRestart;
    public event Action      OnHome;
    public event Action      OnFinishLevel;
    public event Action      OnNextLevel;

    [SerializeField] private int currentLevel;

    private bool playMode;

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Init()
    {
        if (UIManager.Instance != null)
        {
            if(UIManager.Instance.StartButton != null) UIManager.Instance.StartButton.onClick.AddListener(InitGame);
            if (UIManager.Instance.NextLevelButton != null) UIManager.Instance.NextLevelButton.onClick.AddListener(NextLevel);
        }
    }

    private void OnDestroy()
    {
        if (UIManager.Instance != null)
        {
            if (UIManager.Instance.StartButton != null) UIManager.Instance.StartButton.onClick.RemoveListener(InitGame);
            if (UIManager.Instance.NextLevelButton != null) UIManager.Instance.NextLevelButton.onClick.RemoveListener(NextLevel);
        }
    }

    private void InitGame()
    {
        currentLevel++;
        OnInitGame?.Invoke(currentLevel);
        playMode = true;
    }

    private void NextLevel()
    {
        OnNextLevel?.Invoke();

        if(currentLevel >= 3)
        {
            Home();
        }
        else
        {
            InitGame();
        }
    }

    private void Restart()
    {
        OnRestart?.Invoke(currentLevel);
    }

    private void Home()
    {
        currentLevel = 0;
        playMode = false;

        OnHome?.Invoke();
    }

    private void Update()
    {
        if (!playMode)
            return;

        if(Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Home();
        }
    }

    public void Finish()
    {
        OnFinishLevel?.Invoke();
    }
}
