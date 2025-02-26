using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.Config;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agar.io_sfml.Game.Scripts.UI
{
    public class PlayerUI
    {
        private readonly Sprite _pauseButton;
        private readonly Action _onPauseClick;
        private readonly RenderWindow _window;

        public PlayerUI(RenderWindow window, TextureManager textureManager, GameConfig config, Action onPauseClick)
        {
            _window = window;
            _onPauseClick = onPauseClick;
            _pauseButton = new Sprite(textureManager.LoadTexture(config.PauseButtonPath))
            {
                Scale = new Vector2f(0.1f, 0.1f),
                Position = new Vector2f(window.Size.X - 50, 10)
            };
        }

        public void Update()
        {
            Vector2i mousePosition = Mouse.GetPosition(_window);
            Vector2f screenMousePosition = _window.MapPixelToCoords(mousePosition, _window.DefaultView);
            if (Mouse.IsButtonPressed(Mouse.Button.Left) &&
                _pauseButton.GetGlobalBounds().Contains(screenMousePosition.X, screenMousePosition.Y))
            {
                _onPauseClick?.Invoke();
            }
        }

        public void Render()
        {
            _window.SetView(_window.DefaultView);
            _window.Draw(_pauseButton);
        }
    }
}