using UnityEngine;
using System.Collections;

public enum GamePhase { Playing, GameOver, Pause, Win }

[RequireComponent(typeof(InputManager))]
public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameState gameState;

    private InputManager myInputManager;
    private AudioSource myAudioSource;

    void Start()
    {
        myInputManager = GetComponent<InputManager>();
        myAudioSource = GetComponent<AudioSource>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.SetInputUpdater(myInputManager.GetInputState);
        }
        else
        {
            // ERROR!
        }
    }

    void Update()
    {
        InputState inputState = myInputManager.GetInputState();

        if (inputState.PauseInput)
        {
            if (gameState.isPauseReleased)
            {
                TogglePause();
            }
        }
        else
        {
            gameState.isPauseReleased = true;
        }
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void SetPhase(GamePhase gamePhase)
    {
        gameState.gamePhase = gamePhase;
    }

    public void AddScore(int score)
    {
        gameState.score += score;
    }

    private void TogglePause()
    {
        gameState.isPauseReleased = false;
        if (gameState.gamePhase == GamePhase.Playing)
        {
            Time.timeScale = 0f;
            gameState.gamePhase = GamePhase.Pause;
        }
        else if (gameState.gamePhase == GamePhase.Pause)
        {
            Time.timeScale = 1f;
            gameState.gamePhase = GamePhase.Playing;
        }
    }
}

[System.Serializable]
public struct GameState
{
    public GamePhase gamePhase;
    public int score;

    public bool isPauseReleased;
}
