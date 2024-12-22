using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameManager : MonoBehaviour
{
    public Hamilton MapGenerator;
    public GameObject player;
    public GameObject bot_prefab;
    public List<Transform> BotCheckPoints = new List<Transform>();
    public List<Transform> StartingPositions = new List<Transform>();
    private List<GameObject> bots = new List<GameObject>();

    public int finishPosition;
    public GameObject RaceResult;

    public GameObject CountDown;

    public void Start()
    {
        QualitySettings.vSyncCount = 1;
        StartCoroutine(SetRace());
    }

    public IEnumerator SetRace()
    {
        MapGenerator.GenerateMap();
        yield return new WaitUntil(()=>StartingPositions.Count == 4);
        SetRacers();
        yield return new WaitUntil(()=>bots.Count == 3);;
        CountDown.SetActive(true);
        yield return new WaitForSeconds(4.1f);
        TurnEnginesOn();
    }

    public void TurnEnginesOn()
    {
        foreach(GameObject bot in bots)
        {
            bot.GetComponent<BotController>().canGo = true;
        }
        player.GetComponent<Controller>().canGo = true;
    }

    public void SetRacers()
    {
        foreach (Transform StartPos in StartingPositions)
        {
            if (bots.Count == StartingPositions.Count - 1)
            {
                player.transform.position = StartPos.position;
                player.transform.rotation = StartPos.rotation;
            }
            else
            {
                GameObject bot = Instantiate(bot_prefab);
                bot.transform.position = StartPos.transform.position;
                bot.transform.rotation = StartPos.transform.rotation;
                bot.tag = "Bot";
                bots.Add(bot);
                bot.GetComponent<BotController>().waypoints = BotCheckPoints;
            }
        }
    }

    public void FinishRace()
    {
        RaceResult.SetActive(true);
    }
}
