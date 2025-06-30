using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private bool _diamond;
    [SerializeField] private bool _heart;
    [SerializeField] private int _diamondAmount; // Amount of diamonds to give when picked up
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_heart)
            {
                PickupHeart();
            }
            if (_diamond)
            {
                PickupDiamond();
            }
        }
    }

    private void PickupDiamond()
    {
        if (_diamond)
        {
            DiamondPresenter diamondPresenter = GameObject.Find("Diamond Presenter")?.GetComponent<DiamondPresenter>();
            if (diamondPresenter != null)
            {
                diamondPresenter.IncreaseDiamond(_diamondAmount);
            }
            DestroyItem();
        }
    }
    private void PickupHeart()
    {
        if (_heart)
        {
            PlayerHealthPresenter playerHealthPresenter = GameObject.Find("Player Health Presenter")?.GetComponent<PlayerHealthPresenter>();
            if (playerHealthPresenter != null)
            {
                playerHealthPresenter.Heal(1);
            }
            DestroyItem();
        }
    }

    public void DestroyItem()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}