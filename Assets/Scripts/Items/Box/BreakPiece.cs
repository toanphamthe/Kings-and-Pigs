using UnityEngine;

public class BreakPiece : MonoBehaviour
{
    public float forceMin;
    public float forceMax;
    public float lifetime;

    private void Start()
    {
        // Apply a random force to the piece when it is created
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        float randomForce = Random.Range(forceMin, forceMax);
        GetComponent<Rigidbody2D>().AddForce(randomDir * randomForce, ForceMode2D.Impulse);

        Destroy(gameObject, lifetime);
    }
}
