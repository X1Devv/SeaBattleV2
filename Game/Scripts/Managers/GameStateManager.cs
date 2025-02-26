using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.Config;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.Managers
{
    public class GameStateManager
    {
        private enum GameState { Menu, Playing, Paused }
        private GameState _currentState;
        private readonly RenderWindow _window;
        private readonly Text _stateText;
        private readonly Clock _pauseClock;
        private float _lastPauseTime;
        private const float PauseCooldown = 0.2f;

        public GameStateManager(RenderWindow window, TextureManager textureManager, GameConfig config)
        {
            _window = window;
            _stateText = new Text("", textureManager.LoadFont(config.FontPath), 40)
            {
                FillColor = Color.Black,
                OutlineColor = Color.White,
                OutlineThickness = 2f,
                Position = new Vector2f(window.Size.X / 2 - 100, window.Size.Y / 2 - 20)
            };
            _currentState = GameState.Menu;
            _pauseClock = new Clock();
        }

        public void Update(float deltaTime)
        {
            _stateText.DisplayedString = _currentState == GameState.Paused ? "Paused" : "";
        }

        public void Render()
        {
            if (_currentState == GameState.Paused)
            {
                _window.SetView(_window.DefaultView);
                _window.Draw(_stateText);
            }
        }

        public void TogglePause()
        {
            float currentTime = _pauseClock.ElapsedTime.AsSeconds();
            if (currentTime - _lastPauseTime < PauseCooldown) return;
            _lastPauseTime = currentTime;
            _currentState = _currentState == GameState.Playing ? GameState.Paused :
                           _currentState == GameState.Paused ? GameState.Playing : _currentState;
        }

        public void StartGame()
        {
            _currentState = GameState.Playing;
        }

        public bool IsPaused() => _currentState == GameState.Paused;
        public bool IsPlaying() => _currentState == GameState.Playing;
        public bool IsInMenu() => _currentState == GameState.Menu;
    }
}