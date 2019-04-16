﻿using System.Collections.Generic;
using UnityEngine;
using Resources.Scripts;

public class Tile : MonoBehaviour
{
    private float _length;
    private HexaPoint _hexaPoint;
    private CubePoint _cubePoint;

    private GameObject _redTile;

    public float Length
    {
        get { return _length; }
        set { _length = value; }
    }

    public HexaPoint HexaPoint
    {
        get { return _hexaPoint; }
        set { _hexaPoint = value; }
    }

    public CubePoint CubePoint
    {
        get { return _cubePoint; }
        set { _cubePoint = value; }
    }

    public GameObject RedTile
    {
        get { return _redTile; }
        set { _redTile = value; }
    }

    public void OnClick()
    {
        // why there is no on click
        Debug.Log("Row, Col : " + HexaPoint.Row + ", " + HexaPoint.Col);
        Debug.Log("X, Y, Z : " + CubePoint.X + ", " + CubePoint.Y + ", " + CubePoint.Z);
        
        TileManager.Instance.HighlightNeighbor(CubePoint);
        
    }
}