namespace Agar.io_sfml.Game.Scripts.Config
{
    public static class ConfigInitializer
    {
        public static GameConfig Initialize(Dictionary<string, Dictionary<string, string>> configData)
        {
            var config = new GameConfig();

            config.GridWidth = GetValue(configData, "Gameplay", "GridWidth", 10);
            config.GridHeight = GetValue(configData, "Gameplay", "GridHeight", 10);
            config.CellSize = GetValue(configData, "Gameplay", "CellSize", 50);
            config.MapWidth = GetValue(configData, "Map", "Width", 500f);
            config.MapHeight = GetValue(configData, "Map", "Height", 500f);
            config.PauseButtonPath = GetValue(configData, "UI", "PauseButtonPath", "Resources/Textures/AbilityButton/PauseButton.png");
            config.FontPath = GetValue(configData, "UI", "FontPath", "Resources/Fonts/ShareTechMonoRegular.ttf");

            return config;
        }

        private static T GetValue<T>(Dictionary<string, Dictionary<string, string>> configData, string section, string key, T defaultValue)
        {
            if (configData.TryGetValue(section, out var sectionData) && sectionData.TryGetValue(key, out var value))
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            return defaultValue;
        }
    }
}