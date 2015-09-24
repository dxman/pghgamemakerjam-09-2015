using UnityEngine;
using System.Collections;

public class GateCollision : MonoBehaviour
{
    public float passAngleDepth;
    public float testAngle;

    private GateController myGateController;

    void Start()
    {
        myGateController = GetComponentInParent<GateController>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();
            float angle = Vector3.Angle(transform.right, targetRigidbody.velocity);
            testAngle = angle;
            if (angle <= passAngleDepth)
            {
                myGateController.Passed(other.gameObject);
            }
        }
    }
}
