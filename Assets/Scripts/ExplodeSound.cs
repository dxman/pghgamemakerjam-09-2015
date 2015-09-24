using UnityEngine;
using System.Collections;

public class ExplodeSound : MonoBehaviour
{
    private GameController gameController;
    private AudioSource myAudioSource;

    private bool alreadyExploded = false;
    // Use this for initialization
    void Start()
    {
        GameObject gc = GameObject.FindGameObjectWithTag("GameController");
        if (gc != null)
        {
            gameController = gc.GetComponent<GameController>();
        }
        else
        {
            // ERROR!
        }

        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!alreadyExploded)
        {
            GameState gameState = gameController.GetGameState();
            if (gameState.gamePhase == GamePhase.GameOver)
            {
                myAudioSource.Play();
                alreadyExploded = true;
            }
        }
        
    }
}
