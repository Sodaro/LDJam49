using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectible : MonoBehaviour
{
    public float healAmount = 10f;
    public int score = 50;

    [SerializeField] private float moveSpeed = 2f;
    private AudioSource pickupSound;
    private Renderer _renderer;
    private Collider2D collider2D;
    private UnityEngine.Rendering.Universal.Light2D light2D;

    private Transform target;

    private void Awake()
    {
        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        collider2D = GetComponent<Collider2D>();
        _renderer = GetComponent<Renderer>();
        pickupSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    public void PlaySoundAndDestroy()
    {
        collider2D.enabled = false;
        _renderer.enabled = false;
        pickupSound.pitch = Random.Range(0.7f, 1.2f);
        pickupSound.Play();
        light2D.enabled = false;
        Destroy(gameObject, pickupSound.clip.length);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Vacuum")
        {
            target = collision.transform.parent;
        }
    }
}
