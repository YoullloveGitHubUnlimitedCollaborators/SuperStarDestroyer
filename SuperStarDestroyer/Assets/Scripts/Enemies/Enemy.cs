using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private GameObject player;
    public GameObject bullet;
    public GameObject smallExplosion;
    public float shootingCooldownTime;


    public float movingSpeed = 1f;
    public float angle;
    public bool alive = true;
    public float health = 100;
    public float damage = 33.4f;
    public List<KeyValuePair<GameObject, bool>> targetPoints = new List<KeyValuePair<GameObject, bool>>();
    public GameObject targetPoint;
    public int pointId;

    // Use this for initialization
    void Start()
    {
        bool isSet = false;
        for (int i = 0; i < GameManager.instance.targetPoints.Count; i++)
        {
            if (!GameManager.instance.targetPoints[i].Value)
            {
                GameManager.instance.targetPoints[i] = new KeyValuePair<GameObject, bool>(GameManager.instance.targetPoints[i].Key, true);
                targetPoint = GameManager.instance.targetPoints[i].Key;
                isSet = true;
                break;
            }
        }
        if (isSet)
            StartCoroutine(Shooting());
        else Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        GoToPoint();
    }

    // Update is called once per frame
    void Update()

    {
        if (player == null)
        {
            player = GameManager.instance.player;
        }
        LookAtPlayer();
        if (IsDead())
        {
            GameManager.instance.targetPoints[pointId] = new KeyValuePair<GameObject, bool>(GameManager.instance.targetPoints[pointId].Key, false);
            alive = false;
            GameManager.instance.score++;
            Instantiate(smallExplosion, transform.position, Quaternion.Euler(0, 180, 0));
            Destroy(gameObject);
        }
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootingCooldownTime);
        }
    }

    void GoToPoint()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint.transform.position, movingSpeed);
    }


    void Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
    }

    void LookAtPlayer()
    {
        var dir = player.transform.position - transform.position;
        angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + -90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "playerBullet")
        {
            Hit(damage);
        }
    }

    void Hit(float hp)
    {
        health -= hp;
    }

    bool IsDead()
    {
        return health <= 0;
    }
}
