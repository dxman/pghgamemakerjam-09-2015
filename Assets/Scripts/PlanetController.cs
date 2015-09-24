using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour
{
    [SerializeField]
    private PlanetState planetState;

    private GameController gameController;

    private AudioSource myAudioSource;

    void Start()
    {
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

    void OnCollisionEnter(Collision collision)
    {
        GameState gameState = gameController.GetGameState();
        if (gameState.gamePhase == GamePhase.Playing)
        {
            if (collision.gameObject.tag == "Player")
            {
                myAudioSource.Play();
                DamagePlayer(collision.gameObject);
            }
        }
    }

    void Update()
    {
        if (planetState.isDamaging)
        {
            planetState.damageCooldown += Time.deltaTime;
            if (planetState.damageCooldown >= planetState.damageCounterMax)
            {
                planetState.isDamaging = false;
                planetState.damageCooldown = 0f;
            }
        }
    }

    public PlanetState GetPlanetState()
    {
        return planetState;
    }

    private void DamagePlayer(GameObject player)
    {
        if (!planetState.isDamaging)
        {
            planetState.isDamaging = true;
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.Damage(planetState.collideDamage);
        }
    }
}

[System.Serializable]
public struct PlanetState
{
    public int collideDamage;
    public bool isDamaging;
    public float damageCooldown;
    public float damageCounterMax;
}