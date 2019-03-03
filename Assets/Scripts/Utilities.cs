public class Utilities
{
    public const string menuScene = "Start Menu";
    public const string gameScene = "Seb";
}

public enum GuideState
{
    Walking,
    Talk,
    Doubt,
    Alert,
}

public enum GuardState
{
    Waiting,
    Alert,
}

public enum GameState
{
    StartMenu,
    Playing,
    Pause,
    GameOver
}