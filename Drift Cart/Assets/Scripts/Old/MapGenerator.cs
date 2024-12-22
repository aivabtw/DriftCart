using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    List<GameObject> WallFridges1 = new List<GameObject>();
    List<GameObject> WallFridges2 = new List<GameObject>();
    List<GameObject> WallFridges3 = new List<GameObject>();
    List<GameObject> StandardObject = new List<GameObject>();
    List<GameObject> AdditionalObject = new List<GameObject>();
    List<float> ChanceWallFridges1 = new List<float>();
    List<float> ChanceWallFridges2 = new List<float>();
    List<float> ChanceWallFridges3 = new List<float>();
    List<float> ChanceStandardObject = new List<float>();
    List<float> ChanceAdditionalObject = new List<float>();
    public List<Cell> WallCells = new List<Cell>();
    public List<Cell> MidCells = new List<Cell>();
    bool started = false;

    public GameObject GetRandom(List<GameObject> prefabs, List<float> chances)
    {
        List<float> cumulativeChances = new List<float>();
        float cumulativeSum = 0f;
        foreach (var chance in chances)
        {
            cumulativeSum += chance;
            cumulativeChances.Add(cumulativeSum);
        }
        cumulativeSum = cumulativeChances[cumulativeChances.Count - 1];
        for (int i = 0; i < cumulativeChances.Count; i++)
        {
            cumulativeChances[i] /= cumulativeSum;
        }
        float randomValue = UnityEngine.Random.value;
        for (int i = 0; i < cumulativeChances.Count; i++)
        {
            if (randomValue < cumulativeChances[i])
            {
                return prefabs[i];
            }
        }
        return prefabs[prefabs.Count - 1];
    }

    public void GenerateMap()
    {
        foreach (Cell cell in WallCells)
        {
            cell.Iniciate();
            if (cell.isAngled)
            {
                if (started)
                {
                    if (UnityEngine.Random.value > 0.2f)
                    {
                        Spawn(cell, GetRandom(WallFridges2, ChanceWallFridges2), -90f);
                    }
                    else
                    {
                        Spawn(cell, GetRandom(WallFridges3, ChanceWallFridges3));
                    }
                }
                else
                {
                    if (UnityEngine.Random.value > 0.8f)
                    {
                        Spawn(cell, GetRandom(WallFridges2, ChanceWallFridges2));
                        started = true;
                    }
                    else
                    {
                        Spawn(cell, GetRandom(StandardObject, ChanceStandardObject));
                        float addTurn = 0f;
                        foreach(bool wall in cell.walls)
                        {
                            if (wall)
                            {
                                addTurn += 90f;
                                Spawn(cell, GetRandom(AdditionalObject, ChanceAdditionalObject));
                            }
                        }
                    }
                }
            }
            else
            {
                if (started)
                {
                    if (UnityEngine.Random.value > 0.8f)
                    {
                        cell.mirrored = true;
                        Spawn(cell, GetRandom(WallFridges1, ChanceWallFridges1));
                        started = false;
                    }
                    else
                    {
                        Spawn(cell, GetRandom(WallFridges2, ChanceWallFridges2));
                    }
                }
                else
                {
                    if (UnityEngine.Random.value > 0.8f)
                    {
                        Spawn(cell, GetRandom(WallFridges1, ChanceWallFridges1));
                    }
                    else
                    {
                        Spawn(cell, GetRandom(StandardObject, ChanceStandardObject));
                        Spawn(cell, GetRandom(AdditionalObject, ChanceAdditionalObject));

                    }
                }
            }
        }
        foreach (Cell cell in MidCells)
        {
            cell.Iniciate();
            Spawn(cell, GetRandom(StandardObject, ChanceStandardObject));
        }
    }

    public void Spawn(Cell cell, GameObject prefab, float CustomAngle=0f) 
    {
        GameObject newObject = Instantiate(prefab, cell.transform.position, Quaternion.Euler(0, cell.angle+CustomAngle, 0));
        newObject.transform.localScale = new Vector3(newObject.transform.localScale.x, -newObject.transform.localScale.y, newObject.transform.localScale.z);
    }
}
