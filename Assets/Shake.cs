using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    Vector2 startingPos;
    [SerializeField] float amount = 0.5f;
    float delay = 0.5f;
    float timer = 0;

    void Awake()
    {
        startingPos.x = transform.position.x;
        startingPos.y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            transform.position = new Vector3(startingPos.x + Random.Range(-amount, amount), startingPos.y + Random.Range(-amount, amount), 0);
            timer = delay;
        }
        else
            timer -= Time.deltaTime;
        

    }
}
