using UnityEngine;
using System;
using System.Collections.Generic;

public class Hamilton : MonoBehaviour
{
    public int gridWidth = 8;
    public int gridHeight = 8;
    private int[,] grid;
    private List<Vector2Int> path;
    private readonly (int dx, int dy)[] directions = { (-1, 0), (1, 0), (0, -1), (0, 1) };

    List<int> pointdirections = new List<int>();

    public List<GameObject> corners = new List<GameObject>();
    public List<GameObject> straight = new List<GameObject>();
    public GameObject startPointPrefab;
    public GameObject endPointPrefab;

    public void GenerateMap()
    {
        grid = new int[gridWidth, gridHeight]; // 0: empty, 1: visited
        path = new List<Vector2Int>();
        System.Random rand = new System.Random();

        while (true)
        {
            int startX = rand.Next(0, gridWidth);
            int startY = rand.Next(0, gridHeight);
            if (FindHamiltonianPath(startX, startY, 1))
            {
                GetDirections();
                GameObject startpoint= new GameObject("Point");
                Point startPoint = startpoint.AddComponent<Point>();
                startPoint.pointPosition = path[0];
                startPoint.prevDirection = pointdirections[0];
                startPoint.thisDirection = pointdirections[0];
                startPoint.isStart = true;
                startPoint.startPointPrefab = startPointPrefab;
                startPoint.Initialize(straight, corners);
                for (int i = 0; i < path.Count-2; i++)
                {
                    GameObject thispoint = new GameObject("Point");
                    Point thisPoint = thispoint.AddComponent<Point>();
                    thisPoint.pointPosition = path[i+1];
                    thisPoint.prevDirection = pointdirections[i];
                    thisPoint.thisDirection = pointdirections[i+1];
                    thisPoint.Initialize(straight, corners);
                }
                GameObject endpoint = new GameObject("Point");
                Point endPoint = endpoint.AddComponent<Point>();
                endPoint.pointPosition = path[path.Count-1];
                endPoint.prevDirection = pointdirections[pointdirections.Count - 1];
                endPoint.thisDirection = pointdirections[pointdirections.Count - 1];
                endPoint.isEnd = true;
                endPoint.endPointPrefab = endPointPrefab;
                endPoint.Initialize(straight, corners);
                break;
            }
            else
            {
                ResetGrid(); 
            }
        }
        
    }

    // Check if a move is within bounds and not visited
    bool IsSafe(int x, int y)
    {
        return x >= 0 && x < gridWidth && y >= 0 && y < gridHeight && grid[x, y] == 0;
    }

    // Count the number of valid moves from a given cell
    int CountValidMoves(int x, int y)
    {
        int count = 0;
        foreach (var (dx, dy) in directions)
        {
            int nx = x + dx, ny = y + dy;
            if (IsSafe(nx, ny))
            {
                count++;
            }
        }
        return count;
    }

    // Get the sorted directions based on Warnsdorff's rule with randomness
    List<Vector2Int> GetSortedDirections(int x, int y)
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();

        foreach (var (dx, dy) in directions)
        {
            int nx = x + dx, ny = y + dy;
            if (IsSafe(nx, ny))
            {
                validMoves.Add(new Vector2Int(nx, ny));
            }
        }

        // Shuffle the valid moves to add randomness
        System.Random rand = new System.Random();
        for (int i = 0; i < validMoves.Count; i++)
        {
            int j = rand.Next(i, validMoves.Count);
            var temp = validMoves[i];
            validMoves[i] = validMoves[j];
            validMoves[j] = temp;
        }

        // Sort the valid moves by the number of valid moves from the next cell (Warnsdorff’s heuristic)
        validMoves.Sort((a, b) => CountValidMoves(a.x, a.y).CompareTo(CountValidMoves(b.x, b.y)));

        return validMoves;
    }

    // Backtracking function to find the Hamiltonian path
    bool FindHamiltonianPath(int x, int y, int step)
    {
        grid[x, y] = step;
        path.Add(new Vector2Int(x, y));

        if (step == gridWidth * gridHeight) // If all cells are visited
        {
            return true;
        }

        // Get the possible moves sorted by Warnsdorff's rule with randomness
        var sortedMoves = GetSortedDirections(x, y);

        // Try all possible directions based on Warnsdorff’s rule with randomness
        foreach (var move in sortedMoves)
        {
            if (FindHamiltonianPath(move.x, move.y, step + 1))
            {
                return true;
            }
        }

        // Backtrack if no solution found
        grid[x, y] = 0;
        path.RemoveAt(path.Count - 1);
        return false;
    }

    // Reset the grid for a new attempt
    void ResetGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                grid[x, y] = 0;
            }
        }
        path.Clear();
    }

    void GetDirections()
    {
        for (int i = 0; i < gridHeight*gridWidth - 1; i++)
        {
            if (path[i + 1].x == path[i].x)
            {
                if (path[i + 1].y > path[i].y)
                {
                    pointdirections.Add(1);
                }
                else
                {
                    pointdirections.Add(3);
                }
            }
            else
            {
                if (path[i + 1].x > path[i].x)
                {
                    pointdirections.Add(2);
                }
                else
                {
                    pointdirections.Add(4);
                }
            }
        }
        pointdirections.Add(pointdirections[pointdirections.Count - 1]);
    }
}
