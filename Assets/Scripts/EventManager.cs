using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnCheckpointReached(Checkpoint checkpoint);
    public static event OnCheckpointReached onCheckpointReached;
    public static void RaiseOnCheckpointReached(Checkpoint checkpoint)
    {
        if (onCheckpointReached != null)
        {
            onCheckpointReached(checkpoint);
        }
    }

    public delegate void OnCameraStartedMoving();
    public static event OnCameraStartedMoving onCameraStartedMoving;
    public static void RaiseOnCameraStartedMoving()
    {
        if (onCameraStartedMoving != null)
        {
            onCameraStartedMoving();
        }
    }

    public delegate void OnCameraStoppedMoving();
    public static event OnCameraStoppedMoving onCameraStoppedMoving;
    public static void RaiseOnCameraStoppedMoving()
    {
        if (onCameraStoppedMoving != null)
        {
            onCameraStoppedMoving();
        }
    }

    public delegate void OnScoreUpdated(int amount);
    public static event OnScoreUpdated onScoreUpdated;
    public static void RaiseOnScoreUpdated(int amount)
    {
        if (onScoreUpdated != null)
        {
            onScoreUpdated(amount);
        }
    }

    public delegate void OnGameComplete();
    public static event OnGameComplete onGameComplete;
    public static void RaiseOnGameComplete()
    {
        if (onGameComplete != null)
        {
            onGameComplete();
        }
    }


}
