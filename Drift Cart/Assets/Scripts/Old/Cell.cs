using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Cell LeftCell;
    public Cell RightCell;
    public Cell TopCell;
    public Cell BottomCell;
    public bool isAngled;
    public float angle;
    public bool mirrored;
    public List<bool> walls= new List<bool>();

    public void Iniciate()
    {
        if (!LeftCell)
        {
            walls.Add(true);
        }
        else
        {
            walls.Add(false);
        }
        if (!RightCell)
        {
            walls.Add(true);
        }
        else
        {
            walls.Add(false);
        }
        if (!TopCell)
        {
            walls.Add(true);
        }
        else
        {
            walls.Add(false);
        }
        if (!BottomCell)
        {
            walls.Add(true);
        }
        else
        {
            walls.Add(false);
        }

        if (walls[0])
        {
            if (walls[2]) isAngled=true;
            else if (walls[3]) isAngled = true;
        }
        else if (walls[1])
        {
            if (walls[2]) isAngled = true;
            else if (walls[3]) isAngled = true;
        }


        // 2-Top 3-Bootom 1-Right 0-Left
        if (walls[2]) angle= 0;
        else if (walls[1]) angle = 90;
        else if (walls[3]) angle = 180;
        else if (walls[0]) angle = 270;
    }
}
