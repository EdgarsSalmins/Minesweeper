using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper.Core
{
    public class Board
    {
        public Minesweeper Minesweeper { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int NumMines { get; set; }
        public Cell[,] Cells { get; set; }
        private bool _firstClick { get; set; }

        public Board(Minesweeper minesweeper, int width, int height, int mines)
        {
            this.Minesweeper = minesweeper;
            this.Width = width;
            this.Height = height;
            this.NumMines = mines;
            this.Cells = new Cell[width, height];
            this._firstClick = false;
        }

        public void SetupBoard()
        {
            
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    var c = new Cell
                    {
                        CellState = CellState.Closed,
                        CellType = CellType.Regular,
                        CellSize = 50,
                        Board = this,
                        XLoc = i,
                        YLoc = j
                    };

                    c.SetupDesign();
                    c.MouseDown += Cell_MouseClick;

                    this.Cells[i, j] = c;
                    this.Minesweeper.Controls.Add(c);        
                }
            }
            
        }

        public void InitValues()
        {
            var placedMines = 0;
            var random = new Random();
            while (placedMines < NumMines)
            {
                var height = random.Next(Height);
                var width = random.Next(Width);
                if (Cells[height, width].CellState == CellState.Closed &&
                    Cells[height, width].CellType != CellType.Mine)
                {
                    Cells[height, width].CellType = CellType.Mine;
                    placedMines++;
                    Cells[height, width].Text = "*";
                }
            }
        }

        private void Cell_MouseClick(object sender, MouseEventArgs e)
        {
            var cell = (Cell) sender;

            if (cell.CellState == CellState.Opened)
                return;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    cell.OnClick();
                    if (!_firstClick)
                    {
                        InitValues();
                        _firstClick = true;
                    }
                    break;

                case MouseButtons.Right:
                    cell.OnFlag();
                    break;

                default:
                    return;
            }

        }
    }
}
