using UnityEngine;
using System.Collections;

public delegate InputState InputUpdateHandler();

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputState inputState;

    void Update()
    {
        inputState.ThrusterInput = Input.GetButton("Fire1");
        inputState.BoosterInput = Input.GetButton("Fire2");
        inputState.BrakeInput = Input.GetButton("Fire3");
        inputState.PauseInput = Input.GetButton("Pause");
        inputState.TurnInput = Input.GetAxis("Horizontal");
    }

    public InputState GetInputState()
    {
        return inputState;
    }
}

[System.Serializable]
public struct InputState
{
    public bool ThrusterInput;
    public bool BoosterInput;
    public bool BrakeInput;
    public bool PauseInput;
    public float TurnInput;
}