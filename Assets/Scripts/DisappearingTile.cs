using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class DisappearingTile : MonoBehaviour
{
    DissolveEffect dissolveEffect;
    Coroutine coroutine;

    SpriteRenderer spriteRenderer;
    SpriteShapeRenderer spriteShapeRenderer;
    Collider2D _collider;

    Material[] materials;

    private ParticleSystem _destructParticles;
    [SerializeField] private float _fadeDuration = 2f;

    private void Awake()
    {
        //_destructParticles = GetComponentInChildren<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteShapeRenderer = GetComponent<SpriteShapeRenderer>();
        if (spriteRenderer == null)
            materials = spriteShapeRenderer.materials;
        else
            materials = spriteRenderer.materials;

        foreach (var material in materials)
            material.SetFloat("_Fade", 1);
        _collider = GetComponent<Collider2D>();
    }

    IEnumerator FadeAndDisable()
    {
        float f = 0;
        //Color startColor = spriteRenderer.color;
        //Color endColor = new Color((float)137/255, (float)52 /255, (float)37 /255, 1);
        while (f < _fadeDuration)
        {
            float progress = Mathf.Lerp(1, 0, f / _fadeDuration);
            foreach (var material in materials)
                material.SetFloat("_Fade", progress);
            //spriteRenderer.color = Color.Lerp(startColor, endColor, f / _fadeDuration);
            f += Time.deltaTime;
            yield return null;
        }
        //spriteRenderer.color = endColor;
        _collider.enabled = false;
        //_destructParticles.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //coroutine = StartCoroutine(FadeAndDisable());
            //dissolveEffect.TriggerDissolve(collision.ClosestPoint(collision.transform.position));
            //spriteRenderer.color = Color.clear;
            //_collider.enabled = false;
            //_destructParticles.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (coroutine != null)
            return;


        if (collision.gameObject.tag == "Player")
        {
            coroutine = StartCoroutine(FadeAndDisable());
            //dissolveEffect.TriggerDissolve(collision.);
            //spriteRenderer.color = Color.clear;
            //_collider.enabled = false;
            //_destructParticles.Play();
        }
    }
}
