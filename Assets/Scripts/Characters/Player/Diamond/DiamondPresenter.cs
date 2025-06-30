using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiamondPresenter : MonoBehaviour
{
    [SerializeField] private IDiamond _diamond;
    [SerializeField] private TextMeshProUGUI _diamondText;

    void Start()
    {
        _diamond = GameObject.FindGameObjectWithTag("Player")?.GetComponent<IDiamond>();
        if (_diamond != null)
        {
            _diamond.OnDiamondChanged += OnDiamondChanged;
        }
        UpdateView();
    }

    // This method is called when the object is destroyed to unsubscribe from the diamond change event
    private void OnDestroy()
    {
        if (_diamond != null)
        {
            _diamond.OnDiamondChanged -= OnDiamondChanged;
        }
    }

    // This method is called to increase the diamond count by a specified amount
    public void IncreaseDiamond(int amount)
    {
        _diamond.IncreaseDiamond(amount);
    }

    // This method is called to decrease the diamond count by a specified amount
    public void DecreaseDiamond(int amount)
    {
        _diamond.DecreaseDiamond(amount);
    }

    // This method is called to restore the diamond count to zero (or a specific value if needed)
    public void RestoreDiamond()
    {
        _diamond.RestoreDiamond();
    }

    // This method updates the diamond display based on the current diamond count
    public void UpdateView()
    {
        if (_diamond == null)
            return;

        if (_diamondText != null)
        {
            _diamondText.text = _diamond.CurrentDiamond.ToString();
        }
    }

    // This method is called when the diamond count changes to update the view
    private void OnDiamondChanged()
    {
        UpdateView();
    }    
}
