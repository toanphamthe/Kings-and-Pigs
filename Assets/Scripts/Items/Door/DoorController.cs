using UnityEngine;
using UnityEngine.Playables;

public class DoorController : MonoBehaviour
{
    [SerializeField] private bool _isFinishDoor;
    [SerializeField] private PlayableDirector _playableDirector;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_isFinishDoor)
            {
                if (_playableDirector != null)
                {
                    _playableDirector.Play();
                    _playableDirector.stopped += OnCutSceneFinished;
                }
                else
                {
                    OnCutSceneFinished(null);
                }
            }
        }
    }

    private void OnCutSceneFinished(PlayableDirector director)
    {
        GameManager.Instance.SetGameState(GameState.Victory);
    }
}
