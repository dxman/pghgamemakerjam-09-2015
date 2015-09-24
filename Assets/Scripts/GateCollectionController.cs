using UnityEngine;
using System.Collections;

public class GateCollectionController : MonoBehaviour
{
    [SerializeField]
    private GateCollectionState gateCollectionState;

    private GateController[] gateControllers;
    private AudioSource myAudioSource;

    private GameController gameController;

    void Start()
    {
        gateControllers = GetComponentsInChildren<GateController>();

        for (int i = 0; i < gateControllers.Length; i++)
        {
            gateControllers[i].SetIndex(i);
            gateControllers[0].EnableArrow(false);
            gateControllers[i].GatePassed += OnGatePassed;
        }

        gateCollectionState.currentGateIndex = 0;
        gateCollectionState.totalGates = gateControllers.Length;
        gateControllers[0].EnableArrow(true);

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

    void Update()
    {
        if (gateCollectionState.currentGateIndex >= gateCollectionState.totalGates)
        {
            gameController.SetPhase(GamePhase.Win);
        }
    }

    public GateCollectionState GetGateCollectionState()
    {
        return gateCollectionState;
    }

    private void OnGatePassed(int index)
    {
        if (index == gateCollectionState.currentGateIndex)
        {
            myAudioSource.Play();

            gateCollectionState.currentGateIndex++;
            gameController.AddScore(1);

            gateControllers[index].EnableArrow(false);
            if (gateCollectionState.currentGateIndex < gateControllers.Length)
            {
                gateControllers[gateCollectionState.currentGateIndex].EnableArrow(true);
            }
        }
    }
}

[System.Serializable]
public struct GateCollectionState
{
    public int currentGateIndex;
    public int totalGates;
}