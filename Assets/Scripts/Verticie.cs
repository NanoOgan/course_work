using UnityEngine;
using System.Collections;
using Assets;

public class Verticie {
    public Node first { get; set; }
    public Node second { get; set; }

    public Vector3 MidPoint()
    {
        float x = ((float)first.x + (float)second.x) / 2;
        float y = ((float)first.y + (float)second.y) / 2;
        Vector3 middle = new Vector3(x, 0, y);
        return middle;
    }
}
