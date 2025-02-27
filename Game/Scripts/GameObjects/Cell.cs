using Agar.io_sfml.Engine.Core;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.GameObjects
{
    public abstract class Cell : GameObject
    {
        protected RectangleShape Shape;
        public bool IsRevealed { get; protected set; }
        public int X { get; }
        public int Y { get; }

        public Cell(int x, int y, int cellSize)
        {
            X = x;
            Y = y;
            Position = new Vector2f(x * cellSize, y * cellSize);
            Shape = new RectangleShape(new Vector2f(cellSize, cellSize))
            {
                Position = Position,
                OutlineColor = Color.White,
                OutlineThickness = 1f
            };
            IsRevealed = false;
        }

        public virtual bool OnClick()
        {
            IsRevealed = true;
            return false;
        }

        public override void Update(float deltaTime) { }

        public override void Render(RenderWindow window)
        {
            window.Draw(Shape);
        }
    }
}