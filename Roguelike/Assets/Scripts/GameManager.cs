using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action<int> OnInitGame;
    public event Action<int> OnRestart;

    [SerializeField] private int currentLevel;

    private bool playMode;

    public void InitGame()
    {
        currentLevel++;
        OnInitGame?.Invoke(currentLevel);
        playMode = true;
    }

    private void Restart()
    {
        if (playMode)
        {
            Debug.Log("Restart");
            OnRestart?.Invoke(currentLevel);
        }
    }

    private void Home()
    {
        currentLevel = 0;
        playMode = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }
}
