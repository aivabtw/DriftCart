using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private GameManager gameManager;
    private int position = 0;

    private void Start()
    {
        gameManager = GameObject.Find("/GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType().ToString().Equals("UnityEngine.BoxCollider"))
        {
            if (other.CompareTag("Bot"))
            {
                position += 1;
                other.gameObject.SetActive(false);
            }
            else
            {
                position += 1;
                other.gameObject.GetComponent<Controller>().enabled = false;
                gameManager.finishPosition = position;
                gameManager.FinishRace();
            }
        }
    }
}
