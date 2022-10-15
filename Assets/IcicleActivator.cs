using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleActivator : MonoBehaviour
{
    [SerializeField] Icicle[] _icicles;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"ice fall!");
        if (collision.tag == "Vacuum")
        {
            foreach (var icicle in _icicles)
            {
                icicle?.Activate();
            }
        }
    }
}
