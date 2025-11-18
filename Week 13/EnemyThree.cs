using UnityEngine;

public class EnemyThree : MonoBehaviour
{
    public float speedY = 3f;
    public float speedX = 2f;
    public float horizontalLimit = 10f;

    public GameObject explosionPrefab;      
    private GameManager gameManager;         

    private float dirX = 1f;

    void Start()
    {
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        
        dirX = Random.value < 0.5f ? -1f : 1f;
    }

    void Update()
    {
        
        transform.Translate(new Vector3(dirX * speedX, -speedY, 0f) * Time.deltaTime);

       
        if (transform.position.x < -horizontalLimit || transform.position.x > horizontalLimit)
        {
            dirX *= -1f;
        }

        
        if (transform.position.y < -12f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Player")
        {
            whatDidIHit.GetComponent<PlayerController>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (whatDidIHit.tag == "Weapons")
        {
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.AddScore(5);
            Destroy(gameObject);
        }
    }
}
