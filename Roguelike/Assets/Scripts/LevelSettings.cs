using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "OwnGameData/LevelSettings", order = 1)]
public class LevelSettings : ScriptableObject
{
    [SerializeField] private int sizeMax;
    [SerializeField] private int sizeMin;
    [SerializeField] private int maxCountOfWall;
    [SerializeField] private int maxCountOfBox;

    public int SizeMax => sizeMax;
    public int SizeMin => sizeMin;
    public int MaxCountOfWall
    {
        get
        {
            if(maxCountOfWall >= sizeMax)
            {
                return 0;
            }
            else
            {
                return maxCountOfWall;
            }
        }
    }

    public int MaxCountOfBox
    {
        get
        {
            if (maxCountOfBox >= sizeMax)
            {
                return 0;
            }
            else
            {
                return maxCountOfBox;
            }
        }
    }
}
