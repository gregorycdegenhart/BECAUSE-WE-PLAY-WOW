using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : MonoBehaviour
{
    public float downSpeed = 2f;
    public float swaySpeed = 2f;
    public float swayAmount = 1f;

    private float startX;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        // move down
        transform.Translate(Vector3.down * downSpeed * Time.deltaTime);

        // left-right motion (zig-zag)
        Vector3 pos = transform.position;
        pos.x = startX + Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.position = pos;

        // destroy when off screen
        if (transform.position.y < -6.5f)
        {
            Destroy(gameObject);
        }
    }
}
