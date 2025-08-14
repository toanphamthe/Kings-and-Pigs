using UnityEngine;

public class PigThrowBoxDeath : MonoBehaviour
{
    public void DeathHandler()
    {
        gameObject.SetActive(false); // Deactivate the game object when dead
    }
}
