

using SFML.Graphics;

namespace Agar.io_sfml.Game.Scripts.GameObjects
{
    public class Ship : Cell
    {
        public Ship(int x, int y, int cellSize) : base(x, y, cellSize)
        {
            Shape.FillColor = Color.Blue;
        }

        public override bool OnClick()
        {
            IsRevealed = true;
            Shape.FillColor = Color.Red;
            return true;
        }
    }
}