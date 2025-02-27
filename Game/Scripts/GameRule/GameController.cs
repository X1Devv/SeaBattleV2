using Agar.io_sfml.Engine.Interfaces;
using Agar.io_sfml.Engine.Managers;
using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.Config;
using Agar.io_sfml.Game.Scripts.GameObjects;
using Agar.io_sfml.Game.Scripts.Managers;
using Agar.io_sfml.Game.Scripts.UI;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

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
        private List<Cell> _cells;

        public GameController(RenderWindow window, GameConfig config, TextureManager textureManager)
        {
            _window = window;
            _config = config;
            _textureManager = textureManager;
            _fixedCamera = new View(new Vector2f(_config.MapWidth / 2, _config.MapHeight / 2), new Vector2f(_window.Size.X, _window.Size.Y));
            _gameStateManager = new GameStateManager(_window, _textureManager, _config);
            _playerUI = new PlayerUI(_window, _textureManager, _config, _gameStateManager.TogglePause);
            _cells = new List<Cell>();
            GameObjectManager.Instance.AddObject(new StartButton(_window, _textureManager, _config.FontPath, StartGame));
        }

        public void Update(RenderWindow window)
        {
            float deltaTime = 1f / 60f;
            if (_gameStateManager.IsInMenu() || _gameStateManager.IsPlaying())
            {
                GameObjectManager.Instance.UpdateObjects(deltaTime);
                HandleClick();
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
            InitializeBattlefield();
        }

        private void InitializeBattlefield()
        {
            _cells.Clear();
            ShipGenerator generator = new ShipGenerator(_config.GridWidth, _config.GridHeight);
            List<Ship> ships = generator.GenerateShips(_config.CellSize);

            for (int x = 0; x < _config.GridWidth; x++)
            {
                for (int y = 0; y < _config.GridHeight; y++)
                {
                    Ship ship = ships.Find(s => s.X == x && s.Y == y);
                    Cell cell = ship != null ? ship : new Miss(x, y, _config.CellSize);
                    _cells.Add(cell);
                    GameObjectManager.Instance.AddObject(cell);
                }
            }
        }

        private void HandleClick()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left) && _gameStateManager.IsPlaying())
            {
                Vector2i mousePos = Mouse.GetPosition(_window);
                Vector2f worldPos = _window.MapPixelToCoords(mousePos, _fixedCamera);

                Cell cell = _cells.Find(c => !c.IsRevealed && worldPos.X >= c.Position.X && worldPos.X < c.Position.X + _config.CellSize && worldPos.Y >= c.Position.Y && worldPos.Y < c.Position.Y + _config.CellSize);

                cell?.OnClick();
            }
        }
    }
}