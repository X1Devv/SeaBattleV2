using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Engine.Managers;
using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.Config;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Game.Scripts.Managers;
using Agar.io_sfml.Game.Scripts.UI;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_sfml.Game.Scripts.GameRule
{
    public class GameController : IGameController
    {
        private readonly RenderWindow _window;
        private readonly GameConfig _config;
        private readonly TextureManager _textureManager;
        private readonly View _fixedCamera;
        private readonly GameStateManager _gameStateManager;
        private readonly PlayerUI _playerUI;

        public GameController(RenderWindow window, GameConfig config, TextureManager textureManager)
        {
            _window = window;
            _config = config;
            _textureManager = textureManager;
            _fixedCamera = new View(new Vector2f(_config.MapWidth / 2, _config.MapHeight / 2), new Vector2f(_window.Size.X, _window.Size.Y));
            _gameStateManager = new GameStateManager(_window, _textureManager, _config);
            _playerUI = new PlayerUI(_window, _textureManager, _config, _gameStateManager.TogglePause);
            GameObjectManager.Instance.AddObject(new StartButton(_window, _textureManager, _config.FontPath, StartGame));
        }

        public void Update(RenderWindow window)
        {
            float deltaTime = 1f / 60f;
            if (_gameStateManager.IsInMenu() || _gameStateManager.IsPlaying())
            {
                GameObjectManager.Instance.UpdateObjects(deltaTime);
            }
            _playerUI.Update();
            _gameStateManager.Update(deltaTime);
        }

        public void Render(RenderWindow window)
        {
            window.Clear(Color.Black);
            if (_gameStateManager.IsInMenu())
                GameObjectManager.Instance.RenderObjects(window);
            else
            {
                window.SetView(_fixedCamera);
                GameObjectManager.Instance.RenderObjects(window);
                window.SetView(window.DefaultView);
                _playerUI.Render();
                _gameStateManager.Render();
            }
        }

        private void StartGame()
        {
            GameObjectManager.Instance.GetAllObjects().Clear();
            _gameStateManager.StartGame();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int x = 0; x < _config.GridWidth; x++)
            {
                for (int y = 0; y < _config.GridHeight; y++)
                {
                    GameObjectManager.Instance.AddObject(new GridCell(x, y, _config.CellSize));
                }
            }
        }
    }
}