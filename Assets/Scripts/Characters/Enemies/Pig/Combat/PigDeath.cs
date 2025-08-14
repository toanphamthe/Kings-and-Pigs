using UnityEngine;

public class PigDeath : MonoBehaviour
{
    // This script handles the enemy's death by disabling the player GameObject.
    public void DeathHandler()
    {
        gameObject.SetActive(false);    
    }
}
