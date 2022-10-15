using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerCheck : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Checkpoint checkpoint;
        if (checkpoint = collision.GetComponent<Checkpoint>())
        {
            EventManager.RaiseOnCheckpointReached(checkpoint);
            return;
        }
        Collectible collectible = collision.GetComponent<Collectible>();
        if (collectible != null)
        {
            player.Heal(collectible.healAmount);
            player.IncreaseScore(collectible.score);
            collectible.PlaySoundAndDestroy();
        }
        else if (collision.CompareTag("DeathBox"))
        {
            player.CollideAndDie();
        }
        else if (collision.CompareTag("Ending"))
        {
            EventManager.RaiseOnGameComplete();
        }
    }
}
