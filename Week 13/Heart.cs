using UnityEngine;

public class Heart : MonoBehaviour
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
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            
            if (player.lives < 3)
            {
                player.lives++;
                gameManager.ChangeLivesText(player.lives);
            }
            else
            {
                
                gameManager.AddOneScore();
            }

           
            gameManager.PlaySound(1);

            Destroy(gameObject);
        }
    }
}
