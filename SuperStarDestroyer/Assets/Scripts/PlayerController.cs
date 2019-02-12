using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject defaultBullet;
    public GameObject hitEffect;
    public float health;
    public float playerSpeed;
    public float shootingCooldownTime;
    public bool lockShooting;
    public bool IsAlive { get; set; }

    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        IsAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGamePaused)
            if (IsAlive)
            {
                Movement();
                Shooting();
            }
    }

    void Movement()
    {
        float x = HyperInput.instance.GetAxis("Horizontal");
        float y = HyperInput.instance.GetAxis("Vertical");

        rb.velocity = new Vector2(x * playerSpeed * Time.deltaTime, y * playerSpeed * Time.deltaTime);
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !lockShooting)
        {
            Instantiate(defaultBullet, transform.position, Quaternion.identity);
            StartCoroutine(ShootCooldown());
        }
    }

    IEnumerator ShootCooldown()
    {
        lockShooting = true;
        yield return new WaitForSeconds(shootingCooldownTime);
        lockShooting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemyBullet")
        {
            health -= 33.4f;
            Instantiate(hitEffect, collision.transform.position, Quaternion.Euler(0, 180, 0));
            Destroy(collision.gameObject);
        }
    }
}
