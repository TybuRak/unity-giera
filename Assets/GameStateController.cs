using System.Runtime.CompilerServices;

public enum GameState
{
    Active,
    GameOver
}
    
public class GameStateController
{
    public delegate void GameStateAction(GameState gameState);
    public static event GameStateAction GameStateChanged;
    public static GameState GameState { get; private set; } = GameState.Active;

    public static void SetGameState(GameState state)
    {
        GameState = state;
        GameStateChanged?.Invoke(state);
    }
}