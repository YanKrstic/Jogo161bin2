using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [HideInInspector]
    public TargetSpawner.SpawnPoint spawnPoint;

    [HideInInspector]
    public bool moveHorizontal = false;

    [HideInInspector]
    public bool moveVertical = false;

    [HideInInspector]
    public float moveSpeed = 3f;

    [HideInInspector]
    public float moveRange = 5f;

    [HideInInspector]
    public int health = 1;

    [HideInInspector]
    public int pointsValue = 10;

    private Vector3 startPosition;
    private float directionX = 1f;
    private float directionY = 1f;
    private FPSAimController playerShooter;

    void Start()
    {
        startPosition = transform.position;
        playerShooter = FindObjectOfType<FPSAimController>();
    }

    void Update()
    {
        // Movimento
        Vector3 newPos = transform.position;

        if (moveHorizontal)
        {
            newPos.x += directionX * moveSpeed * Time.deltaTime;

            if (Mathf.Abs(newPos.x - startPosition.x) >= moveRange)
                directionX *= -1;
        }

        if (moveVertical)
        {
            newPos.y += directionY * moveSpeed * Time.deltaTime;

            if (Mathf.Abs(newPos.y - startPosition.y) >= moveRange)
                directionY *= -1;
        }

        transform.position = newPos;

        // Rota��o
        //transform.Rotate(Vector3.up, 180 * Time.deltaTime);
    }

    // Substitua o OnTriggerEnter inteiro por este bloco:
    void OnCollisionEnter(Collision collision)
    {
        // Note que em OnCollisionEnter, precisamos usar collision.gameObject para ver a Tag
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;

            if (health <= 0)
            {
                if (playerShooter != null)
                    playerShooter.AddScore(pointsValue);

                Destroy(collision.gameObject); // Destrói a bala
                Destroy(gameObject);           // Destrói o alvo
            }
            else
            {
                Destroy(collision.gameObject); // Destrói a bala se o alvo ainda tiver vida
            }
        }
    }
}