using UnityEngine;

public class EnemyTwo : MonoBehaviour
{
    public float speed = 2f;
    public float horizontalAmplitude = 1.5f;
    public float horizontalFrequency = 2f;

    public GameObject explosionPrefab;   
    private GameManager gameManager;     

    private float startingX;

    void Start()
    {
        startingX = transform.position.x;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        float newX = startingX + Mathf.Sin(Time.time * horizontalFrequency) * horizontalAmplitude;
        float newY = transform.position.y - speed * Time.deltaTime;

        transform.position = new Vector3(newX, newY, 0f);
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Player")
        {
            whatDidIHit.GetComponent<PlayerController>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (whatDidIHit.tag == "Weapons")
        {
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.AddScore(5);
            Destroy(this.gameObject);
        }
    }
}
