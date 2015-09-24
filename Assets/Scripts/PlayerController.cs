using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public AudioClip idleSound;
    public AudioClip accelerationSound;

    [SerializeField]
    private PlayerState playerState;

    private InputUpdateHandler inputUpdate;
    private GameController gameController;

    private Rigidbody myRigidbody;
    private ParticleSystem smokeParticleSystem;
    private ParticleSystem fireParticleSystem;
    private ParticleSystem thrusterParticleSystem;

    private AudioSource[] audio;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            if (particleSystem.name == "Smoke")
            {
                smokeParticleSystem = particleSystem;
            }
            else if (particleSystem.name == "Fire")
            {
                fireParticleSystem = particleSystem;
            }
            else if (particleSystem.name == "Thruster")
            {
                thrusterParticleSystem = particleSystem;
            }
        }
       
        GameObject gc = GameObject.FindGameObjectWithTag("GameController");
        if (gc != null)
        {
            gameController = gc.GetComponent<GameController>();
        }
        else
        {
            // ERROR!
        }

        audio = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GameState gameState = gameController.GetGameState();
        if (gameState.gamePhase == GamePhase.Playing)
        {
            InputState inputState = inputUpdate.Invoke();

            if (!playerState.isBoosting)
            {
                if (inputState.BoosterInput)
                {
                    if (playerState.isBoostReleased)
                    {
                        BeginBoost();
                    }
                }
                else
                {
                    playerState.isBoostReleased = true;
                    if (inputState.ThrusterInput)
                    {
                        Thrust();
                    }
                    else if (inputState.BrakeInput)
                    {
                        Brake();
                    }
                }
            }
            else
            {
                ContinueBoost();
            }

            if (inputState.TurnInput != 0f)
            {
                Rotate(inputState.TurnInput);
            }

            ////////////////////////////////////////////////////////////////////////

            

            if (inputState.ThrusterInput)
            {
                if (audio[0].isPlaying)
                {
                    audio[0].Pause();
                }
                audio[0].clip = accelerationSound;
                audio[0].volume = 0.5f;
            }
            else
            {
                if (audio[0].isPlaying)
                {
                    audio[0].Pause();
                }
                audio[0].clip = idleSound;
                audio[0].volume = 1.0f;
            }

            audio[0].Play();
        }

        

        
    }

    public PlayerState GetPlayerState()
    {
        return playerState;
    }

    public void SetInputUpdater(InputUpdateHandler inputUpdateHandler)
    {
        inputUpdate = inputUpdateHandler;
    }

    void BeginBoost()
    {
        if (playerState.boosts > 0)
        {
            playerState.boosts--;
            playerState.isBoosting = true;
            playerState.isBoostReleased = false;

            audio[1].Play();
        }
    }

    void ContinueBoost()
    {
        myRigidbody.AddForce(transform.right * playerState.boostThrust * Time.deltaTime);
        playerState.boostCooldown += Time.deltaTime;
        if (playerState.boostCooldown >= playerState.boostDuration)
        {
            playerState.isBoosting = false;
            playerState.boostCooldown = 0f;
        }
    }

    void Thrust()
    {
        myRigidbody.AddForce(transform.right * playerState.normalThrust * Time.deltaTime);
    }

    void Brake()
    {
        myRigidbody.AddForce(transform.right * playerState.brakeThrust * -1 * Time.deltaTime);
    }

    void Rotate(float rotateDirection)
    {
        transform.Rotate(Vector3.up, playerState.rotateSpeed * rotateDirection * Time.deltaTime);
    }

    public void Damage(int amount)
    {
        playerState.life -= amount;
        if (playerState.life <= 0)
        {
            thrusterParticleSystem.Stop();
            smokeParticleSystem.Play();
            fireParticleSystem.Play();
            gameController.SetPhase(GamePhase.GameOver);
        }
    }
}

[System.Serializable]
public struct PlayerState
{
    public int life;
    public int maxLife;
    public int boosts;
    public int maxBoosts;

    public bool isBoosting;
    public bool isBoostReleased;
    public float boostCooldown;

    public int score;

    public float normalThrust;
    public float rotateSpeed;
    public float boostThrust;
    public float boostDuration;
    public float brakeThrust;
}