using SFML.Graphics;

namespace Agar.io_sfml.Game.Scripts.GameObjects
{
    public class Miss : Cell
    {
        public Miss(int x, int y, int cellSize) : base(x, y, cellSize)
        {
            Shape.FillColor = Color.Transparent;
        }

        public override bool OnClick()
        {
            IsRevealed = true;
            Shape.FillColor = Color.White;
            return false;
        }
    }
}