using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper.Core;
using Xunit;

namespace Minesweeper.Tests
{
    [TestClass]
    public class BoardTests
    {
        [Fact]
        public void Board_InitTest()
        {
            var board = new Board(new Minesweeper(), 50, 50, 10);
            board.Height.Should().Be(50);
            board.Width.Should().Be(50);
            board.NumMines.Should().Be(10);
        }

        [Fact]
        public void Board_FilledTest()
        {
            var board = new Board(new Minesweeper(), 9, 9, 5);
            board.SetupBoard();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Xunit.Assert.NotNull(board.Cells[i,j]); 
                }
            }
        }

        [Fact]
        public void Board_CellLocationTest()
        {
            var board = new Board(new Minesweeper(), 9, 9, 5);
            board.SetupBoard();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board.Cells[i, j].XLoc.Should().Be(i);
                    board.Cells[i, j].YLoc.Should().Be(j);
                }
            }
        }

        [Fact]
        public void Board_CellClickTest()
        {
            var board = new Board(new Minesweeper(), 9, 9, 5);
            board.SetupBoard();
            var cell = board.Cells[0, 5];
            var cell2 = board.Cells[4, 4];
            cell.OnClick();
            cell2.OnClick();
            cell.CellState.Should().Be(CellState.Opened);
            cell2.CellState.Should().Be(CellState.Opened);
        }

        [Fact]
        public void Board_MineCountTest()
        {
            var board = new Board(new Minesweeper(), 9, 9, 9);
            board.SetupBoard();
            var rnd = new Random();
            var height = rnd.Next(board.Height);
            var width = rnd.Next(board.Width);
            var cell = board.Cells[height,width];
            cell.OnClick();
            board.InitValues();

            var mineCount = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board.Cells[i, j].CellType == CellType.Mine)
                        mineCount++;
                }
            }

            mineCount.Should().Be(9);
        }
    }
}
