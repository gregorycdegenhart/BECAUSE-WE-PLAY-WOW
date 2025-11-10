using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject cloudPrefab;

    
    public GameObject coinPrefab;

    
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;

    
    public float horizontalScreenSize;
    public float verticalScreenSize;

   
    public int score;

    
    [Header("Coin Spawn Settings")]
    public Vector2 coinSpawnEvery = new Vector2(4f, 6f); 
    public int maxCoinsOnScreen = 3;                     
    public float edgePadding = 1.5f;                     
    public float minDistanceFromPlayer = 1.75f;          

    void Start()
    {
        
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;

        score = 0;

        
        if (playerPrefab != null && GameObject.FindGameObjectWithTag("Player") == null)
            Instantiate(playerPrefab, transform.position, Quaternion.identity);

        CreateSky();

        
        InvokeRepeating(nameof(CreateEnemy), 1f, 3f);

        
        if (coinPrefab != null)
            StartCoroutine(CoinSpawnLoop());

        UpdateScoreUI();
        if (livesText != null) livesText.text = "Lives: 3";
    }

    void Update() { }

    void CreateEnemy()
    {
        float x = Random.Range(-horizontalScreenSize * 0.9f, horizontalScreenSize * 0.9f);
        Vector3 pos = new Vector3(x, verticalScreenSize, 0f);
        Instantiate(enemyOnePrefab, pos, Quaternion.Euler(180, 0, 0));
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            float x = Random.Range(-horizontalScreenSize, horizontalScreenSize);
            float y = Random.Range(-verticalScreenSize, verticalScreenSize);
            Instantiate(cloudPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        }
    }

    IEnumerator CoinSpawnLoop()
    {
        while (true)
        {
            float wait = Random.Range(coinSpawnEvery.x, coinSpawnEvery.y);
            yield return new WaitForSeconds(wait);

            if (CountCoins() >= maxCoinsOnScreen)
                continue;

            SpawnCoin();
        }
    }

    void SpawnCoin()
    {
        if (coinPrefab == null) return;

        
        Vector2 coinHalf = GetCoinHalfSize();

        float minX = -horizontalScreenSize + edgePadding + coinHalf.x;
        float maxX = horizontalScreenSize - edgePadding - coinHalf.x;
        float minY = -verticalScreenSize + edgePadding + coinHalf.y;
        float maxY = verticalScreenSize - edgePadding - coinHalf.y;

        Vector3 spawnPos = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            0f
        );

        
        Transform player = FindPlayerTransform();
        int tries = 6;
        while (player != null && tries-- > 0 &&
               Vector3.Distance(spawnPos, player.position) < minDistanceFromPlayer)
        {
            spawnPos.x = Random.Range(minX, maxX);
            spawnPos.y = Random.Range(minY, maxY);
        }

        Instantiate(coinPrefab, spawnPos, Quaternion.identity);
    }

    int CountCoins()
    {
        
        return FindObjectsOfType<Coin>(false).Length;
    }

    Vector2 GetCoinHalfSize()
    {
        float hx = 0.25f, hy = 0.25f; 
        if (coinPrefab != null)
        {
            var sr = coinPrefab.GetComponentInChildren<SpriteRenderer>();
            if (sr != null && sr.sprite != null)
            {
                Vector2 ext = sr.sprite.bounds.extents;
                Vector3 sc = sr.transform.localScale;
                hx = ext.x * sc.x;
                hy = ext.y * sc.y;
            }
        }
        return new Vector2(hx, hy);
    }

    Transform FindPlayerTransform()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        return p != null ? p.transform : null;
    }

    
    public void AddScore(int earnedScore)
    {
        score += earnedScore;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
    }

    public void ChangeLivesText(int currentLives)
    {
        if (livesText != null) livesText.text = "Lives: " + currentLives;
    }
}
