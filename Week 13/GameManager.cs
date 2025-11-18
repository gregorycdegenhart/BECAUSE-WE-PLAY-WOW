using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    
    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;     
    public GameObject enemyThreePrefab;   
    public GameObject cloudPrefab;
    public GameObject gameOverText;
    public GameObject restartText;
    public GameObject powerupPrefab;
    public GameObject audioPlayer;
    public GameObject coinPrefab;
    public GameObject heartPrefab;

    
    public AudioClip powerupSound;
    public AudioClip powerdownSound;

    
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerupText;

    
    public float horizontalScreenSize;
    public float verticalScreenSize;

    
    public int score;
    public int cloudMove;

    private bool gameOver;

    void Start()
    {
        
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;
        cloudMove = 1;
        gameOver = false;

       
        AddScore(0);

       
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();

        
        InvokeRepeating(nameof(CreateEnemy), 1f, 3f); 
        InvokeRepeating(nameof(CreateEnemyTwo), 2f, 4f); 
        InvokeRepeating(nameof(CreateEnemyThree), 3f, 5f); 

       
        StartCoroutine(SpawnPowerup());
        StartCoroutine(SpawnCoin());
        StartCoroutine(SpawnHeart());

        powerupText.text = "No powerups yet!";
    }

    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    
    void CreateEnemy()
    {
        Instantiate(
            enemyOnePrefab,
            new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f,
                        verticalScreenSize,
                        0),
            Quaternion.Euler(180, 0, 0)
        );
    }

    
    void CreateEnemyTwo()
    {
        Instantiate(
            enemyTwoPrefab,
            new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f,
                        verticalScreenSize,
                        0),
            Quaternion.identity
        );
    }

    
    void CreateEnemyThree()
    {
        Instantiate(
            enemyThreePrefab,
            new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f,
                        verticalScreenSize,
                        0),
            Quaternion.identity
        );
    }

    
    void CreatePowerup()
    {
        float minY = -verticalScreenSize * 0.8f; 
        float maxY = 0f;                         

        Instantiate(
            powerupPrefab,
            new Vector3(
                Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f),
                Random.Range(minY, maxY),
                0),
            Quaternion.identity
        );
    }

    
    void CreateCoin()
    {
        float minY = -verticalScreenSize * 0.8f;
        float maxY = 0f;

        Instantiate(
            coinPrefab,
            new Vector3(
                Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f),
                Random.Range(minY, maxY),
                0),
            Quaternion.identity
        );
    }

    
    void CreateHeart()
    {
        float minY = -verticalScreenSize * 0.8f;
        float maxY = 0f;

        Instantiate(
            heartPrefab,
            new Vector3(
                Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f),
                Random.Range(minY, maxY),
                0),
            Quaternion.identity
        );
    }

    
    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(
                cloudPrefab,
                new Vector3(
                    Random.Range(-horizontalScreenSize, horizontalScreenSize),
                    Random.Range(-verticalScreenSize, verticalScreenSize),
                    0),
                Quaternion.identity
            );
        }
    }

    
    public void ManagePowerupText(int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerupText.text = "Speed!";
                break;
            case 2:
                powerupText.text = "Double Weapon!";
                break;
            case 3:
                powerupText.text = "Triple Weapon!";
                break;
            case 4:
                powerupText.text = "Shield!";
                break;
            default:
                powerupText.text = "No powerups yet!";
                break;
        }
    }

    
    IEnumerator SpawnPowerup()
    {
        float spawnTime = Random.Range(3f, 5f);
        yield return new WaitForSeconds(spawnTime);
        CreatePowerup();
        StartCoroutine(SpawnPowerup());
    }

    IEnumerator SpawnCoin()
    {
        float spawnTime = Random.Range(3f, 5f);
        yield return new WaitForSeconds(spawnTime);
        CreateCoin();
        StartCoroutine(SpawnCoin());
    }

    IEnumerator SpawnHeart()
    {
        float spawnTime = Random.Range(4f, 7f);
        yield return new WaitForSeconds(spawnTime);
        CreateHeart();
        StartCoroutine(SpawnHeart());
    }

    
    public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerupSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerdownSound);
                break;
        }
    }

   
    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
        scoreText.text = "Score: " + score;
    }

    public void AddOneScore()
    {
        AddScore(1);
    }

    
    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }

    
    public void GameOver()
    {
        gameOverText.SetActive(true);
        restartText.SetActive(true);
        gameOver = true;
        CancelInvoke();
        cloudMove = 0;
    }
}
