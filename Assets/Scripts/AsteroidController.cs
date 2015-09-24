using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour
{
    public Transform playerTransform;
    public Transform tran;

    public float speed;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        playerTransform = transform;
        journeyLength = 0.1f;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            playerTransform = other.transform;
            journeyLength = Vector3.Distance(transform.position, playerTransform.position);
        }
    }

    void OnTriggerExit(Collider other)
    {
        playerTransform = transform;
        journeyLength = Vector3.Distance(transform.position, playerTransform.position);
    }

    // Update is called once per frame
    void Update()
    {

        float distCovered = (Time.deltaTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, playerTransform.position, fracJourney);
    }
}
