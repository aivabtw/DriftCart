using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStart : MonoBehaviour
{
    [SerializeField]
    List<Transform> startPoints = new List<Transform>();
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("/GameManager").GetComponent<GameManager>();
        foreach (Transform sp in startPoints)
        {
            gameManager.StartingPositions.Add(sp);
        }
    }
}
