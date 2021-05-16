using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Ilumisoft.Minesweeper
{
    public class TileGrid : MonoBehaviour
    {
        [SerializeField]
        int width = 5;

        [SerializeField]
        int height = 5;

        InputField inputFieldVertical;
        InputField inputFieldHorizontal;
        public bool verticalFlag = false;
        public bool horizontalFlag = false;

        [SerializeField]
        float cellSize = 1.0f;

        public int Width { get; set; }
        public int Height { get; set; }
        // public int Width { get => this.width; }
        // public int Height { get => this.height; }

        public Vector3 BottomLeftCorner => transform.position - new Vector3(Width - 1, Height - 1, 0) / 2 * CellSize;

        public float CellSize { get => this.cellSize; set => this.cellSize = value; }

        Tile[,] tiles;

        private void Awake()
        {
            if(SceneManager.GetActiveScene().name == "Game Custom"){
                inputFieldHorizontal = GameObject.Find("InputFieldHorizontal").GetComponent<InputField>();
                inputFieldVertical = GameObject.Find("InputFieldVertical").GetComponent<InputField>();
                return;
            } else {
                Width = this.width;
                Height = this.height;
                tiles = new Tile[width, height];
            }
        }

        public void tileCustom(){
                tiles = new Tile[width, height];
        }

        public void HorizontalCustom(){
            if(inputFieldHorizontal.text == ""){
                GameObject.Find("ButtonStart").GetComponent<Button>().interactable = false;
                return;
            } else {
                width = int.Parse(inputFieldHorizontal.text);
                Width = int.Parse(inputFieldHorizontal.text);
                horizontalFlag = true;
            }
        }

        public void VerticalCustom(){
            if(inputFieldVertical.text == ""){
                GameObject.Find("ButtonStart").GetComponent<Button>().interactable = false;
                return;
            } else {
                height = int.Parse(inputFieldVertical.text);
                Height = int.Parse(inputFieldVertical.text);
                verticalFlag = true;
            }
        }

        public bool IsValidTilePosition(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public void SetTile(int x, int y, Tile tile)
        {
            tiles[x, y] = tile;
        }

        public bool TryGetTile(Vector2Int gridPos, out Tile tile)
        {
            return TryGetTile(gridPos.x, gridPos.y, out tile);
        }

        public bool TryGetTile(int x, int y, out Tile tile)
        {
            tile = null;

            if (!IsValidTilePosition(x,y))
            {
                return false;
            }

            if (tiles[x, y] != null)
            {
                tile = tiles[x, y];
                return true;
            }

            return false;
        }

        public bool TryGetGridPosition(Tile tile, out Vector2Int gridPosition)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (tile == tiles[x, y])
                    {
                        gridPosition = new Vector2Int(x, y);

                        return true;
                    }

                }
            }

            gridPosition = default;
            return false;
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return BottomLeftCorner + new Vector3(x, y, 0) * CellSize;
        }

        private void OnDrawGizmos()
        {

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Vector3 position = BottomLeftCorner + new Vector3(x, y, 0);

                    Gizmos.DrawWireCube(position, Vector3.one * CellSize);

                }
            }
        }
    }
}