using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;

    void Start()
    {
        // spawn EnemyOne every 2 seconds
        InvokeRepeating("CreateEnemyOne", 1f, 2f);

        // spawn EnemyTwo every 3 seconds
        InvokeRepeating("CreateEnemyTwo", 3f, 3f);
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0), Quaternion.identity);
    }

    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0), Quaternion.identity);
    }
}
