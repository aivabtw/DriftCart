using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public Vector2 pointPosition;
    public int prevDirection;
    public int thisDirection;
    public int angle;
    public int corner = 0;


    public GameObject endPointPrefab;
    public GameObject startPointPrefab;
    public List<GameObject> corners = new List<GameObject>();
    public List<GameObject> straight = new List<GameObject>();

    public bool isStart = false;
    public bool isEnd = false;


    public void Initialize(List<GameObject> straightlink, List<GameObject> cornerlink)
    {
        angle = (prevDirection - 1) * 90;
        if (thisDirection == prevDirection + 1 || (thisDirection == 1 && prevDirection == 4))
        {
            corner = 1;
        }
        else if (thisDirection == prevDirection - 1 || (thisDirection == 4 && prevDirection == 1))
        {
            corner = 2;
        }
        corners = cornerlink;
        straight = straightlink;
        Spawn();
    }

    void Spawn()
    {
        if (isStart)
        {
            Instantiate(startPointPrefab, new Vector3(pointPosition.x * 20, 0, pointPosition.y * 20), Quaternion.Euler(0, angle, 0));
        }
        else if (isEnd)
        {
            Instantiate(endPointPrefab, new Vector3(pointPosition.x * 20, 0, pointPosition.y * 20), Quaternion.Euler(0, angle, 0));
        }
        else if (corner == 0)
        {
            Instantiate(straight[Random.Range(0, straight.Count)], new Vector3(pointPosition.x * 20, 0, pointPosition.y * 20), Quaternion.Euler(0, angle, 0));
        }
        else
        {
            GameObject createdObject = Instantiate(corners[Random.Range(0, corners.Count)], new Vector3(pointPosition.x * 20, 0, pointPosition.y * 20), Quaternion.Euler(0, angle, 0));
            // Mirror the scale on the X-axis
            if (corner == 2)
            {
                Vector3 mirroredScale = createdObject.transform.localScale;
                mirroredScale.x *= -1; // Negate the X scale
                createdObject.transform.localScale = mirroredScale;
            }
        }
    }
}