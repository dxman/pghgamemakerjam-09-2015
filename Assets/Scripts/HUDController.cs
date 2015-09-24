using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private Text lifeText;
    private Text boosterText;
    private Text phaseText;

    private Image winImage;

    private GameController gameController;
    private PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        GameObject lifeGui = GameObject.Find("LifeText");
        if (lifeGui != null)
        {
            lifeText = lifeGui.GetComponent<Text>();
        }
        else
        {
            // ERROR!
        }

        GameObject boosterGui = GameObject.Find("BoosterText");
        if (boosterGui != null)
        {
            boosterText = boosterGui.GetComponent<Text>();
        }
        else
        {
            // ERROR!
        }

        GameObject phaseGui = GameObject.Find("PhaseText");
        if (phaseGui != null)
        {
            phaseText = phaseGui.GetComponent<Text>();
        }
        else
        {
            // ERROR!
        }

        GameObject wi = GameObject.Find("WinImage");
        if (wi != null)
        {
            winImage = wi.GetComponent<Image>();
        }
        else
        {
            // ERROR!
        }

        GameObject gc = GameObject.FindGameObjectWithTag("GameController");
        if (gc != null)
        {
            gameController = gc.GetComponent<GameController>();
        }
        else
        {
            // ERROR!
        }

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            playerController = p.GetComponent<PlayerController>();
        }
        else
        {
            // ERROR!
        }
    }

    void OnGUI()
    {
        GameState gameState = gameController.GetGameState();
        PlayerState playerState = playerController.GetPlayerState();

        lifeText.text = "Life: " + playerState.life + " / " + playerState.maxLife;
        boosterText.text = "Boosters: " + playerState.boosts + " / " + playerState.maxBoosts;

        switch (gameState.gamePhase)
        {
            case GamePhase.Win:
                //phaseText.text = "You Win!";
                winImage.enabled = true;
                break;

            case GamePhase.GameOver:
                phaseText.text = "Game Over!";
                break;

            case GamePhase.Pause:
                phaseText.text = "PAUSE";
                break;

            default:
                phaseText.text = "";
                break;
        }
    }
}
