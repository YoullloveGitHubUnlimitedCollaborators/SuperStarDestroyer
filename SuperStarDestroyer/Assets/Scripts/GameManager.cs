using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager instance;
    public GameObject player;
    public GameObject hugeExplosion;
    public List<KeyValuePair<GameObject, bool>> targetPoints = new List<KeyValuePair<GameObject, bool>>();
    public int score;
    public bool isGamePaused;


    [Header("UI")]
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public Text scoreText;

    // Use this for initialization
    void Start()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        score = 0;
        GetEnemyPoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGamePaused)
        {
            CheckPlayerHealth();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            SetPause(!isGamePaused);
    }

    void CheckPlayerHealth()
    {
        if (player.GetComponent<PlayerController>().IsAlive)
            if (player.GetComponent<PlayerController>().health <= 0)
            {
                Instantiate(hugeExplosion, player.transform.position, Quaternion.Euler(0, 180, 0));
                gameOverPanel.SetActive(true);
                scoreText.text = "Score: " + score.ToString();
                player.GetComponent<PlayerController>().IsAlive = false;
                player.GetComponent<SpriteRenderer>().enabled = false;
            }
    }

    void GetEnemyPoints()
    {
        GameObject[] enemyPointsArray = GameObject.FindGameObjectsWithTag("enemyPoint");
        foreach (GameObject go in enemyPointsArray)
        {
            targetPoints.Add(new KeyValuePair<GameObject, bool>(go, false));
        }
    }

    public void SetPause(bool state)
    {
        if (state)
        {
            Time.timeScale = 0;
            isGamePaused = true;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            isGamePaused = false;
            pausePanel.SetActive(false);
        }
    }
}
