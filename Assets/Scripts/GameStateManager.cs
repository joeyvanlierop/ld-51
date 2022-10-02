using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
  MAIN_MENU,
  PLAYING,
  GAME_OVER
}

public class GameStateManager : MonoBehaviour
{
  public GameState gameState = GameState.MAIN_MENU;
  public GameObject playButton;
  public GameObject restartButton;
  public GameObject gameOver;
  public CharacterMovement characterMovement;

  public void StartGame()
  {
    gameState = GameState.PLAYING;
    playButton.gameObject.SetActive(false);
    characterMovement.autoWalk = false;
    characterMovement.SetSpeed(5, 5);
  }

  public void EndGame()
  {
    gameState = GameState.GAME_OVER;
    restartButton.SetActive(true);
    gameOver.SetActive(true);
  }

  public void RestartGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
