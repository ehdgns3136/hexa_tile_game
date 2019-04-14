using UnityEngine;
using Resources.Scripts;

public class Tile : MonoBehaviour
{
    private Point _point;
    private float _length;

    public Point Point
    {
        get { return _point; }
        set { _point = value; }
    }

    public float Length
    {
        get { return _length; }
        set { _length = value; }
    }

    public void OnClick()
    {
        // why there is no on click
        Debug.Log("Row, Col : " + _point.Row + ", " + _point.Col);
        Debug.Log("X, Y, Z : " + _point.X + ", " + _point.Y + ", " + _point.Z);
    }
}
