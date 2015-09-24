using UnityEngine;
using System.Collections;

public class CameraAnimator : MonoBehaviour
{
    private Animator myAnimator;

    private PlayerController playerController;
    private InputManager inputManager;

    void Start()
    {
        myAnimator = GetComponent<Animator>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            playerController = p.GetComponent<PlayerController>();
        }
        else
        {
            // ERROR!
        }

        GameObject gc = GameObject.FindGameObjectWithTag("GameController");
        if (gc != null)
        {
            inputManager = gc.GetComponent<InputManager>();
        }
        else
        {
            // ERROR!
        }
    }

    void Update()
    {
        PlayerState playerState = playerController.GetPlayerState();
        InputState inputState = inputManager.GetInputState();

        if (playerState.isBoosting)
        {
            myAnimator.CrossFade("CameraBoost", 0.5f);
        }
        else if (inputState.ThrusterInput)
        {
            myAnimator.CrossFade("CameraThrust", 0.5f);
        }
        else if (inputState.BrakeInput)
        {
            myAnimator.CrossFade("CameraBrake", 0.5f);
        }
        else
        {
            myAnimator.CrossFade("CameraIdle", 0.5f);
        }
    }
}
