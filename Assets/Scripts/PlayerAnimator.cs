using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour
{
    private Animator myAnimator;

    private InputManager inputManager;

    void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();

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
        InputState inputState = inputManager.GetInputState();
        myAnimator.SetFloat("TurnInput", inputState.TurnInput);
    }
}
