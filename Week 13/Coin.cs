using UnityEngine;

public class Coin : MonoBehaviour
{
    public float lifetime = 3f;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.AddOneScore();     
            gameManager.PlaySound(1);       
            Destroy(gameObject);
        }
    }
}
