using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper.Core;
using Xunit;

namespace Minesweeper.Tests
{
    [TestClass]
    public class CellTests
    {
        [Fact]
        public void Cell_clickedTest()
        {
            var cell = new Cell();
            cell.OnClick();
            cell.CellState.Should().Be(CellState.Opened);
        }
    }
}
