using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Coroutine coroutine;
    private AudioListener _listener;

    private void OnEnable()
    {
        EventManager.onCheckpointReached += MoveToCheckpoint;
    }
    private void OnDisable()
    {
        EventManager.onCheckpointReached -= MoveToCheckpoint;
    }
    public void MoveToCheckpoint(Checkpoint checkpoint)
    {
        if (coroutine != null)
            return;

        Vector3 targetPosition = checkpoint.GetCheckpointCameraPosition();
        if (targetPosition == transform.position)
            return;

        coroutine = StartCoroutine(MoveToPosition(targetPosition));
        EventManager.RaiseOnCameraStartedMoving();
    }

    private IEnumerator MoveToPosition(Vector3 position)
    {
        float f = 0;
        float duration = 1f;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(position.x, position.y, startPos.z);
        while (f < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, f / duration);
            f += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
        coroutine = null;
        EventManager.RaiseOnCameraStoppedMoving();
    }

    private void Awake()
    {
        _listener = GetComponent<AudioListener>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            float volume = AudioListener.volume;
            volume += 0.05f;
            volume = Mathf.Clamp01(volume);
            AudioListener.volume = volume;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            float volume = AudioListener.volume;
            volume -= 0.05f;
            volume = Mathf.Clamp01(volume);
            AudioListener.volume = volume;
        }
    }
}
