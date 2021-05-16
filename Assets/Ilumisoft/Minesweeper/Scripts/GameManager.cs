using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Ilumisoft.Minesweeper.UI;

namespace Ilumisoft.Minesweeper
{
    public class GameManager : MonoBehaviour, ITileClickListener
    {
        [SerializeField]
        GameObject levelCompleteUI = null;

        [SerializeField]
        GameObject gameOverUI = null;

        [SerializeField]
        Tile normalTilePrefab = null;

        [SerializeField]
        Tile bombTilePrefab = null;

        [SerializeField]
        TileGrid grid = null;

        [SerializeField]

        Timer timer = null;

        [SerializeField]
        int bombCount = 5;
        int bombValue;

        GameObject tileContainer = null;

        List<Tile> tiles = new List<Tile>();

        InputField inputFieldBomb;
        public bool bombFlag = false;
        public GameObject CanvasProperty;

        private void Awake()
        {
            tileContainer = new GameObject("Tile Container");
        }

        void Start()
        {
            if(SceneManager.GetActiveScene().name == "Game Custom"){
                inputFieldBomb = GameObject.Find("InputFieldBomb").GetComponent<InputField>();
                GameObject.Find("ButtonStart").GetComponent<Button>().interactable = false;
                return;
            } else {
                //custom意外ならcreateboardを実行
                CreateBoard();
            }
        }

        public void BombCustom(){
            if(inputFieldBomb.text == ""){
                GameObject.Find("ButtonStart").GetComponent<Button>().interactable = false;
                return;
            } else if(int.Parse(inputFieldBomb.text) > grid.Height * grid.Width){
                GameObject.Find("ButtonStart").GetComponent<Button>().interactable = false;
                return;
            } else {
                bombValue = int.Parse(inputFieldBomb.text);
                bombFlag = true;
            }

            if(bombFlag == true && grid.horizontalFlag == true && grid.verticalFlag == true){
                GameObject.Find("ButtonStart").GetComponent<Button>().interactable = true;
            }
        }

        public void CreateCustomBoard(){
            grid.tileCustom();
            CreateBoard();
            deleteMenu();
            timer.IsStarted = true;
        }

        public void CreateBoard()
        {
            AddBombsToGrid();
            AddNormalTilesToGrid();
            tileContainer.AddComponent<ScaleChange>();
            
            foreach (var tile in tiles)
            {
                if (tile.TryGetComponent<TileNumber>(out var tileNumber))
                {
                    tileNumber.SetNumberOfBombs(grid.GetNumberOfSurroundingBombs(tile));
                }
            }
        }

        private void AddBombsToGrid()
        {
            //customならボムは入力された値を参照する
            if(SceneManager.GetActiveScene().name == "Game Custom"){
                bombCount = bombValue;
            } else {
                bombCount = Mathf.Min(bombCount, grid.Width * grid.Height);
            }

            for (int i = 0; i < bombCount; i++)
            {
                int x = UnityEngine.Random.Range(0, grid.Width);
                int y = UnityEngine.Random.Range(0, grid.Height);
                if (grid.TryGetTile(x, y, out _) == false)
                {
                    AddTileToGrid(x, y, bombTilePrefab);
                }
                else
                {
                    i--;
                    continue;
                }
            }
        }

        private void AddNormalTilesToGrid()
        {
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    if (grid.TryGetTile(x, y, out _) == false)
                    {
                        AddTileToGrid(x, y, normalTilePrefab);
                    }
                }
            }
        }

        void AddTileToGrid(int x, int y, Tile prefab)
        {
            var position = grid.GetWorldPosition(x, y);

            var tile = Instantiate(prefab, position, Quaternion.identity);
            tile.transform.SetParent(tileContainer.transform);
            grid.SetTile(x, y, tile);

            tiles.Add(tile);
        }

        public void OnTileClick(Tile tile)
        {
            if (tile.State == TileState.Hidden)
            {
                TileRevealer tileRevealer = new TileRevealer(grid);

                tileRevealer.Reveal(tile);

                if (tile.CompareTag(Bomb.Tag))
                {
                    GameOver(won: false);
                }
                else if(HasRevealedAllSafeTiles())
                {
                    GameOver(won: true);
                }
            }
        }

        bool HasRevealedAllSafeTiles()
        {
            foreach (var tile in tiles)
            {
                if (tile.CompareTag(Bomb.Tag) == false && tile.State != TileState.Revealed)
                {
                    return false;
                }
            }

            return true;
        }

        void GameOver(bool won)
        {
            StopAllCoroutines();
            StartCoroutine(GameOverCoroutine(won));
        }

        IEnumerator GameOverCoroutine(bool won)
        {
            GameObject uiElement = won ? levelCompleteUI : gameOverUI;

            yield return new WaitForSecondsRealtime(1.0f);

            uiElement.SetActive(true);
        }

        public void deleteMenu(){
            CanvasProperty.SetActive(false);
        }
    }
}