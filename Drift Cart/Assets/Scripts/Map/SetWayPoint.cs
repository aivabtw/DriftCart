using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWayPoint : MonoBehaviour
{
    [SerializeField]
    List<Transform> wayPoints = new List<Transform>();
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("/GameManager").GetComponent<GameManager>();
        foreach (Transform wp in wayPoints) {
            gameManager.BotCheckPoints.Add(wp);
        }
    }
}
