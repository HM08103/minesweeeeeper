using UnityEngine;
using UnityEngine.UI;

namespace Ilumisoft.Minesweeper
{
    [RequireComponent(typeof(Tile))]
    public class TileNumber : MonoBehaviour
    {
        [SerializeField]
        Text text = null;

        [SerializeField]
        public Font fontBold;

        public void SetNumberOfBombs(int count)
        {
            if (text != null)
            {
                this.text.text = count == 0 ? "" : count.ToString();
                this.text.font = fontBold;
            }
        }
    }
}