using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public struct FieldSize
{
    public FieldSize(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }
}

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelSettings  settings;
    [SerializeField] private MapParts       parts;

    private FieldSize field;
    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnInitGame     += SetupScene;
            GameManager.Instance.OnRestart      += Restart;
            GameManager.Instance.OnHome         += Home;
            GameManager.Instance.OnNextLevel    += NextLevel;
        }

    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnInitGame     -= SetupScene;
            GameManager.Instance.OnRestart      -= Restart;
            GameManager.Instance.OnHome         -= Home;
            GameManager.Instance.OnNextLevel    -= NextLevel;
        }
    }

    private void GenerateSize()
    {
        int xSize = Random.Range(settings.SizeMin, settings.SizeMax);
        int ySize = Random.Range(settings.SizeMin, settings.SizeMax);
        field = new FieldSize(xSize, ySize);

        Debug.Log("Generate field with: x - " + xSize + ", y - " + ySize);
    }
    private void LevelSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for(int x = -1; x < field.X+1; x++)
        {
            for(int y = -1; y < field.Y+1; y++)
            {
                GameObject toInstantiate = parts.Floor;

                if(x == -1 || x == field.X || y == -1 || y == field.Y)
                {
                    toInstantiate = parts.Wall;
                }

                var obj = Instantiate(toInstantiate, new Vector3(x, 0f, y), Quaternion.identity) as GameObject;
                obj.transform.SetParent(boardHolder);
            }
        }
    }

    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = 1; x < field.X - 1; x++)
        {
            for (int y = 1; y < field.Y - 1; y++)
            {
                gridPositions.Add(new Vector3(x, 0f, y)); ;
            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);

        Vector3 randomPosition = gridPositions[randomIndex];

        gridPositions.RemoveAt(randomIndex);

        return randomPosition;
    }


    void LayoutObjectAtRandom(GameObject something, int minimum, int maximum)
    {
        if(something == null)
        {
            Debug.LogError("Map part is null");
            return;
        }

        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            var obj  = Instantiate(something, randomPosition, Quaternion.identity);
            obj.transform.SetParent(boardHolder);
        }
    }

    private void SetupScene(int level)
    {
        if(boardHolder != null)
        {
            Destroy(boardHolder.gameObject);
        }

        GenerateSize();
        LevelSetup();
        InitialiseList();
        LayoutObjectAtRandom(parts.Box, 0, settings.MaxCountOfBox);
        LayoutObjectAtRandom(parts.Wall, 0, settings.MaxCountOfWall);
        LayoutObjectAtRandom(parts.Finish, 1, 1);
        LayoutObjectAtRandom(parts.Player, 1, 1);
    }

    private void Restart(int currentLevel)
    {
        Destroy(boardHolder.gameObject);
        SetupScene(currentLevel);
    }

    private void Home()
    {
        Destroy(boardHolder.gameObject);
    }

    private void NextLevel()
    {
        Destroy(boardHolder.gameObject);
    }
}
