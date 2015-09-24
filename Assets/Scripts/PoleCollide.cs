using UnityEngine;
using System.Collections;

public class PoleCollide : MonoBehaviour
{
    private AudioSource myAudioSource;

    // Use this for initialization
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        myAudioSource.Play();
    }
}
