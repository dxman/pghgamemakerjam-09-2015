using UnityEngine;
using System.Collections;

public class PlanetGravity : MonoBehaviour
{
    [SerializeField]
    private float gravityStrength;

    private Transform planetTransform;

    void Start()
    {
        planetTransform = this.GetComponentInParent<Transform>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 gravityForce = (other.transform.position - planetTransform.position).normalized * gravityStrength * Time.deltaTime;

            Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();
            targetRigidbody.AddForce(gravityForce);
        }
    }
}
