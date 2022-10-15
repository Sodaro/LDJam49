using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    [SerializeField] private float maxFallSpeed = 10f;
    [SerializeField] private float accelerationAmount = 4f;
    private float currentSpeed = 0;
    private bool isFalling = false;

    public void Activate()
    {
        isFalling = true;
    }

    void Update()
    {
        if (!isFalling)
            return;

        currentSpeed += accelerationAmount * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxFallSpeed);

        transform.position += transform.up * currentSpeed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathBox"))
            Destroy(gameObject);
    }
}
