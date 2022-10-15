using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSetup : MonoBehaviour
{
    [SerializeField] GameObject _rawImage;
    [SerializeField] GameObject _playerPrefab;
    [SerializeField] GameObject _cameraPrefab;
    [SerializeField] List<Checkpoint> _checkpoints = new List<Checkpoint>();
    // Start is called before the first frame update

    [ContextMenu("Reset Progress")]
    public static void ResetProgress()
    {
        PlayerPrefs.SetInt("lastCheckPoint", 0);
        PlayerPrefs.SetInt("playerScore", 0);
        PlayerPrefs.SetInt("firstTime", 0);
    }



    void Start()
    {
        _rawImage.SetActive(true);
        EventManager.onCheckpointReached += SetActiveCheckpoint;
        int activeCheckPoint = PlayerPrefs.GetInt("lastCheckPoint");


        Matrix4x4 playerStart = _checkpoints[activeCheckPoint].GetPlayerCheckpointWorldTransform();

        Vector3 playerPos = playerStart.GetColumn(3);

        Quaternion playerRotation = playerStart.rotation;

        Vector3 cameraStart = _checkpoints[activeCheckPoint].GetCheckpointCameraPosition();

        Player player = Instantiate(_playerPrefab, playerPos, playerRotation).GetComponent<Player>();
        player.SetHealth(_checkpoints[activeCheckPoint].PlayerStartHealth);

        Instantiate(_cameraPrefab, cameraStart, Quaternion.identity);

    }

    private void SetActiveCheckpoint(Checkpoint checkpoint)
    {
        if (!_checkpoints.Contains(checkpoint))
            return;

        PlayerPrefs.SetInt("lastCheckPoint", _checkpoints.IndexOf(checkpoint));
    }
}
