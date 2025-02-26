using Agar.io_sfml.Engine.Core;
using Agar.io_sfml.Engine.Utils;
using Agar.io_sfml.Game.Scripts.Config;
using SFML.Graphics;
using SFML.Window;

namespace Agar.io_sfml.Game.Scripts.GameRule
{
    public class GameInit
    {
        public GameLoop CreateGameLoop()
        {
            var config = LoadConfig();
            var window = CreateWindow();
            var textureManager = new TextureManager();
            var gameController = new GameController(window, config, textureManager);

            return new GameLoop(gameController, window);
        }

        private GameConfig LoadConfig()
        {
            var configLoader = new ConfigLoader("Resources/Config/config.ini");
            return ConfigInitializer.Initialize(configLoader.Load());
        }

        private RenderWindow CreateWindow()
        {
            return new RenderWindow(new VideoMode(800, 600), "Sea Battle");
        }
    }
}