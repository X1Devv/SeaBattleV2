using Agar.io_sfml.Engine.Core;
using Agar.io_sfml.Engine.Utils;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agar.io_sfml.Game.Scripts.UI
{
    public class StartButton : GameObject
    {
        private readonly RectangleShape _buttonShape;
        private readonly Text _startText;
        private readonly Action _onClick;
        private readonly RenderWindow _window;

        public StartButton(RenderWindow window, TextureManager textureManager, string fontPath, Action onClick)
        {
            _window = window;
            _onClick = onClick;
            _startText = new Text("Start", textureManager.LoadFont(fontPath), 40)
            {
                FillColor = Color.White,
                Position = new Vector2f(window.Size.X / 2 - 50, window.Size.Y / 2 - 20)
            };
            FloatRect textBounds = _startText.GetGlobalBounds();
            _buttonShape = new RectangleShape(new Vector2f(textBounds.Width + 20, textBounds.Height + 10))
            {
                Position = new Vector2f(textBounds.Left - 10, textBounds.Top - 5),
                FillColor = new Color(0, 128, 0, 200),
                OutlineColor = Color.Black,
                OutlineThickness = 2f
            };
            Position = _buttonShape.Position;
        }

        public override void Update(float deltaTime)
        {
            Vector2i mousePos = Mouse.GetPosition(_window);
            Vector2f mappedPos = _window.MapPixelToCoords(mousePos, _window.DefaultView);
            if (Mouse.IsButtonPressed(Mouse.Button.Left) &&
                _buttonShape.GetGlobalBounds().Contains(mappedPos.X, mappedPos.Y))
            {
                _onClick?.Invoke();
            }
        }

        public override void Render(RenderWindow window)
        {
            window.SetView(window.DefaultView);
            window.Draw(_buttonShape);
            window.Draw(_startText);
        }
    }
}