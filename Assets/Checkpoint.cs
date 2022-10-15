using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private float _playerStartHealth = 30f;
    [SerializeField] Camera _camera;
    [SerializeField] Transform _playerStart;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    public float PlayerStartHealth => _playerStartHealth;

    public Matrix4x4 GetPlayerCheckpointWorldTransform()
    {
        return _playerStart.localToWorldMatrix;
    }

    public Vector3 GetCheckpointCameraPosition()
    {
        return _camera.transform.position;
    }
}
