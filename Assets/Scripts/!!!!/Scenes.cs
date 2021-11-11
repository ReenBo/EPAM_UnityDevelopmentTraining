namespace ET.Scenes
{
    public readonly struct SceneName
    {
        public const string MainMenu = "_MainMenu";
        public const string GameSession = "_GameSession";
        public const string Level_1 = "_Level_1";
        public const string Level_2 = "_Level_2";
        public const string Level_3 = "_Level_3";
    }

    public enum SceneIndex
    {
        _PreLevel,
        _MainMenu,
        _GameSession,
        _Level_1,
        _Level_2,
        _Level_3
    }
}
