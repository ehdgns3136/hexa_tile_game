using System;
using Resources.Scripts.InGame;
using UnityEngine;
using UnityEditor;

namespace Resources.Scripts.Utils
{
    public class MapEditor : MonoBehaviour
    {
        public Color editorDefaultColor;
        public Color editorSelectedColor;
        
        public Board board;

        public Color[] colors;
        public bool add;
        public bool remove;
        
        public HexTile.TileHeight selectedHeight;
        public int selectedColor;

        void Awake ()
        {
            editorDefaultColor =  new Color(0.767f, 0.767f, 0.767f);
            editorSelectedColor = Color.Lerp(Color.black, editorDefaultColor, 0.5f);
            
            add = false;
            remove = false;
            
            colors = new Color[]
            {
                new Color(0.784f, 0.874f, 0.317f), 
                new Color(0.501f, 0.654f, 0.486f),
                new Color(0.631f, 0.768f, 0.329f),
                new Color(0.447f, 0.898f, 0.721f),
                new Color(0.360f, 0.545f, 0.819f),
                new Color(0.368f, 0.443f, 0.690f),
                new Color(0.925f, 0.670f, 0.235f),
                new Color(0.956f, 0.780f, 0.247f),
                new Color(0.956f, 0.866f, 0.474f)
            };
            
            OnChangeColor(0);
            OnChangeHeight(HexTile.TileHeight.DEFAULT);
        }

        void Update () {
            if (Input.GetMouseButton(0)) {
                HandleInput();
            }
        }

        void HandleInput () {
            
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint( Input.mousePosition );
            RaycastHit2D hit = Physics2D.Raycast( worldPoint, Vector2.zero );
            
            if (hit.collider != null)
            {
                board.UpdateTile(hit.point, colors[selectedColor], selectedHeight, add, remove);
            }
        }

        public void OnChangeColor(int index)
        {
            selectedColor = index;
        }

        public void OnChangeHeight(HexTile.TileHeight height)
        {
            selectedHeight = height;
        }

        public void OnAdd()
        {
            if (add) add = false;
            else add = true;
            
            remove = false;
        }

        public void OnRemove()
        {
            add = false;

            if (remove) remove = false;
            else remove = true;
        }

        public void OnSave()
        {
            
        }

        public void OnLoad()
        {
            
        }

        public void OnInitialize()
        {
            board.InitializeTiles();
        }
    }
}