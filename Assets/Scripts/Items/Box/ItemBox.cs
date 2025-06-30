using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] private GameObject[] itemDrops;
    [SerializeField] private GameObject breakEffectPrefab;
    [SerializeField] private int maxDrops;
    [SerializeField] private float forceMin;
    [SerializeField] private float forceMax;

    public void TakeDamage()
    {
        if (breakEffectPrefab != null)
            Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);

        foreach (var item in itemDrops)
        {
            int dropAmount = Random.Range(0, maxDrops);

            for (int i = 0; i < dropAmount; i++)
            {
                GameObject itemDrop = Instantiate(item, transform.position, Quaternion.identity);

                Rigidbody2D rb = itemDrop.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 randomDir = Random.insideUnitCircle.normalized;
                    float randomForce = Random.Range(forceMin, forceMax);
                    rb.AddForce(randomDir * randomForce, ForceMode2D.Impulse);
                }
            }
        }

        Destroy(gameObject);
    }
}
