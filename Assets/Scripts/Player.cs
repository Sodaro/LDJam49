using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.Light2D playerLight;
    private Shapes2D.Shape shape;

    private Color initialColor1;
    private Color initialColor2;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeedDegrees = 360f;

    [SerializeField] private float maxLightRadius = 3.5f;
    [SerializeField] private float maxLightIntensity = 5f;

    private Vector3 velocity;

    private bool canMove = true;

    private bool isDead = false;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 30f;
    SpriteRenderer sprite;


    bool isGameComplete = false;

    float timeBetweenDamage = 1f;
    float healthTimer = 0f;

    private int _score = 0;

    private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _shape;

    [SerializeField] private AudioSource _collisionSound;
    [SerializeField] private AudioSource _deathSound;

    private void Awake()
    {
        shape = GetComponentInChildren<Shapes2D.Shape>();
        initialColor1 = shape.settings.fillColor;
        initialColor2 = shape.settings.fillColor2;

        playerLight = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public int GetScore()
    {
        return _score;
    }
    private void Start()
    {
        _score = PlayerPrefs.GetInt("playerScore");
        EventManager.RaiseOnScoreUpdated(_score);
    }

    private void OnEnable()
    {
        EventManager.onGameComplete += SetCompleted;
        EventManager.onCameraStartedMoving += DisableMovement;
        EventManager.onCameraStoppedMoving += EnableMovement;
    }

    private void OnDisable()
    {
        EventManager.onGameComplete -= SetCompleted;
        EventManager.onCameraStartedMoving -= DisableMovement;
        EventManager.onCameraStoppedMoving -= EnableMovement;
    }

    void SetCompleted()
    {
        isGameComplete = true;
    }

    void DisableMovement()
    {
        canMove = false;
    }
    void EnableMovement()
    {
        canMove = true;
        PlayerPrefs.SetInt("playerScore", _score);
    }

    void Update()
    {
        if (isDead)
        {
            if (!_deathSound.isPlaying && !_collisionSound.isPlaying)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            else
                return;
        }

        if (!canMove)
            return;

        if (Input.GetKeyDown(KeyCode.B))
        {
            GameSetup.ResetProgress();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
            
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (isGameComplete)
            return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
            transform.rotation *= Quaternion.Euler(0, 0, rotationSpeedDegrees * -horizontal * Time.deltaTime);

        Vector3 moveDir = transform.up;

        if (vertical > 0)
            velocity = moveDir * maxSpeed;
        else
            velocity = moveDir * moveSpeed;


        velocity.y = Mathf.Clamp(velocity.y, -maxSpeed, maxSpeed);
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

        
        transform.position += velocity * Time.deltaTime;

        if (healthTimer <= 0)
        {
            healthTimer = timeBetweenDamage;
            currentHealth -= 10f;
            UpdateColor();
        }
        else
            healthTimer -= Time.deltaTime;

        if (currentHealth <= 0)
        {
            Die();
            _deathSound.Play();
        }
    }

    private void UpdateColor()
    {
        playerLight.pointLightOuterRadius = (currentHealth / maxHealth) * maxLightRadius;
        playerLight.intensity = (currentHealth / maxHealth) * maxLightIntensity;
    }

    public void CollideAndDie()
    {
        Die();
        _collisionSound.Play();
    }

    public void Die()
    {
        _shape.SetActive(false);
        var main = _particleSystem.main;
        main.startColor = shape.settings.fillColor;
        _particleSystem.Play();
        isDead = true;
    }

    public void SetHealth(float amount)
    {
        currentHealth = amount;
        UpdateColor();
    }

    public void IncreaseScore(int amount)
    {
        _score += amount;
        EventManager.RaiseOnScoreUpdated(_score);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateColor();
    }
}
