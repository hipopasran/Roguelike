using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapParts", menuName = "OwnGameData/MapParts", order = 1)]
public class MapParts : ScriptableObject
{
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject finish;
    [SerializeField] private GameObject player;

    public GameObject Floor     => floor;
    public GameObject Box       => box;
    public GameObject Wall      => wall;
    public GameObject Finish    => finish;
    public GameObject Player    => player;

}
