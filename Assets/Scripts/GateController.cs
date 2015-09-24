using UnityEngine;
using System.Collections;

public delegate void GatePassedHandler(int index);

public class GateController : MonoBehaviour
{
    [SerializeField]
    private GateState gateState;

    public event GatePassedHandler GatePassed = delegate { };

    private GateCollectionController gateCollectionController;
    private SpriteRenderer arrowRenderer;

    void Start()
    {
        arrowRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public GateState GetGateState()
    {
        return gateState;
    }

    public void SetIndex(int index)
    {
        gateState.index = index;
    }

    public void EnableArrow(bool enabled)
    {
        arrowRenderer.enabled = enabled;
    }

    public void Passed(GameObject passedObject)
    {
        GatePassed.Invoke(gateState.index);
    }
}

[System.Serializable]
public struct GateState
{
    public int index;
}