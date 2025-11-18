using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int lives;

    private float speed;
    private int weaponType;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;
    public float minY = -3.5f;   
    public float maxY = 0f;      


    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;   

    private bool hasShield = false;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        lives = 3;
        speed = 5.0f;
        weaponType = 1;
        hasShield = false;

        if (shieldPrefab != null)
        {
            shieldPrefab.SetActive(false);
        }

        if (thrusterPrefab != null)
        {
            thrusterPrefab.SetActive(false);
        }

        gameManager.ChangeLivesText(lives);
    }

    void Update()
    {
        Movement();
        Shooting();
    }

    public void LoseALife()
    {
     
        if (hasShield)
        {
            hasShield = false;

            if (shieldPrefab != null)
            {
                shieldPrefab.SetActive(false);
            }

            
            gameManager.ManagePowerupText(0);
            gameManager.PlaySound(2);
            return;
        }

        
        lives--;
        gameManager.ChangeLivesText(lives);

        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.GameOver();
            Destroy(this.gameObject);
        }
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(3f);
        speed = 5f;

        if (thrusterPrefab != null)
        {
            thrusterPrefab.SetActive(false);
        }

        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }

    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(3f);
        weaponType = 1;
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        
        if (whatDidIHit.CompareTag("Powerup"))
        {
            Destroy(whatDidIHit.gameObject);

            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);

            switch (whichPowerup)
            {
                case 1:
                    
                    speed = 10f;
                    StartCoroutine(SpeedPowerDown());

                    if (thrusterPrefab != null)
                    {
                        thrusterPrefab.SetActive(true);
                    }

                    gameManager.ManagePowerupText(1);
                    break;

                case 2:
                    
                    weaponType = 2;
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(2);
                    break;

                case 3:
                    
                    weaponType = 3;
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(3);
                    break;

                case 4:
                    
                    if (!hasShield)
                    {
                        hasShield = true;

                        if (shieldPrefab != null)
                        {
                            shieldPrefab.SetActive(true);
                        }

                        gameManager.ManagePowerupText(4);
                    }
                    break;
            }
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (weaponType)
            {
                case 1:
                    Instantiate(bulletPrefab,
                        transform.position + new Vector3(0, 0.5f, 0),
                        Quaternion.identity);
                    break;

                case 2:
                    Instantiate(bulletPrefab,
                        transform.position + new Vector3(-0.5f, 0.5f, 0),
                        Quaternion.identity);
                    Instantiate(bulletPrefab,
                        transform.position + new Vector3(0.5f, 0.5f, 0),
                        Quaternion.identity);
                    break;

                case 3:
                    Instantiate(bulletPrefab,
                        transform.position + new Vector3(-0.5f, 0.5f, 0),
                        Quaternion.Euler(0, 0, 45));
                    Instantiate(bulletPrefab,
                        transform.position + new Vector3(0, 0.5f, 0),
                        Quaternion.identity);
                    Instantiate(bulletPrefab,
                        transform.position + new Vector3(0.5f, 0.5f, 0),
                        Quaternion.Euler(0, 0, -45));
                    break;
            }
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * Time.deltaTime * speed);

        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        
        if (transform.position.x <= -horizontalScreenSize || transform.position.x > horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        
        float minY = -verticalScreenSize * 0.8f;
        float maxY = 0f;

        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, 0);
    }
}
