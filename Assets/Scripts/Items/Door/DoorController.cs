using UnityEngine;
using UnityEngine.Playables;

public class DoorController : MonoBehaviour
{
    [SerializeField] private bool _isFinishDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_isFinishDoor)
            {
                GameObject player = collision.gameObject;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                GameManager.Instance.SetGameState(GameState.Victory);
            }
        }
    }
}
