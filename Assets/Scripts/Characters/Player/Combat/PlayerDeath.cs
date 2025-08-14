using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    // This script handles the player's death by disabling the player GameObject.
    public void DeathHandle()
    {
        gameObject.SetActive(false);
        GameManager.Instance.SetGameState(GameState.GameOver);
    }    
}
