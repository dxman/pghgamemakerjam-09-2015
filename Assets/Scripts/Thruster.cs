using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour
{
    private InputManager inputManager;
    private PlayerController playerController;
	private ParticleSystem thruster;


	void Start()
	{
		thruster = this.GetComponent<ParticleSystem>();

        GameObject gc = GameObject.FindGameObjectWithTag("GameController");
        if (gc != null)
        {
            inputManager = gc.GetComponent<InputManager>();
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

	void Update()
	{
        InputState inputState = inputManager.GetInputState();
        PlayerState playerState = playerController.GetPlayerState();
        if ((inputState.ThrusterInput) || (playerState.isBoosting))
		{
			thruster.startSpeed = 4;
			thruster.startSize = 2;
		}
		else
		{
			thruster.startSpeed = 2;
			thruster.startSize = 1;
			//thruster.enableEmission = false;
		}
	}
}