using Agar.io_sfml.Engine.Core;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.GameObjects
{
    public class GridCell : GameObject
    {
        private readonly RectangleShape _cellShape;

        public GridCell(int x, int y, int cellSize)
        {
            Position = new Vector2f(x * cellSize, y * cellSize);
            _cellShape = new RectangleShape(new Vector2f(cellSize, cellSize))
            {
                Position = Position,
                OutlineColor = Color.White,
                OutlineThickness = 1f,
                FillColor = Color.Transparent
            };
        }

        public override void Update(float deltaTime) { }

        public override void Render(RenderWindow window)
        {
            window.Draw(_cellShape);
        }
    }
}