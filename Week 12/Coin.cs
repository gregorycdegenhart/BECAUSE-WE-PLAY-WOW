using UnityEngine;

public class Coin : MonoBehaviour
{
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        FindObjectOfType<GameManager>().AddScore(1);
        Destroy(gameObject);
    }
}
