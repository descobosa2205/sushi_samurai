using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public partial class GameManager : MonoBehaviour
{
    private int missesAllowed = 3;
    private int misses;
    private int pointsScored;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private GameObject[] hearts;
    [SerializeField] private ParticleSystem[] heartParticles;
    [SerializeField] private AudioClip heartSound;

    [SerializeField] private AudioSource[] heartSpeakers;

    private void OnEnable()
    {
        Sushi.SushiPoint += ManagePoints;
        Sushi.SushiMiss += ManageMisses;
        Bomb.BombTriggered += GameOver;
    }

    private void OnDisable()
    {
        Sushi.SushiPoint -= ManagePoints;
        Sushi.SushiMiss -= ManageMisses;
        Bomb.BombTriggered -= GameOver;
    }

    private void Start()
    {
        misses = 0;
        pointsScored = 0;
        UpdateScoreText();
    }

    private void ManagePoints()
    {
        pointsScored += 1;
        UpdateScoreText();
    }

    private void ManageMisses()
    {
        heartParticles[2 - misses].transform.SetParent(null);
        heartSpeakers[2 - misses].transform.SetParent(null);
        heartParticles[2 - misses].Play();
        heartSpeakers[2 - misses].PlayOneShot(heartSound);
        Destroy(heartParticles[2-misses].gameObject, 1.0f);
        Destroy(heartSpeakers[2 - misses].gameObject, 1.0f);
        Destroy(hearts[2 - misses]);
        misses += 1;
        if (misses >= missesAllowed)
        {
            GameOver();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = pointsScored.ToString();
    }

    private void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void GameOver()
    {
        PlayerData.LastRunPoints = pointsScored;

        PlayerData.AddCoins(pointsScored);

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                heartParticles[i].transform.SetParent(null);
                heartParticles[i].Play();
                heartSpeakers[i].transform.SetParent(null);
                heartSpeakers[i].PlayOneShot(heartSound);
                Destroy(heartParticles[i].gameObject, 1.0f);
                Destroy(heartSpeakers[i].gameObject, 1.0f);
                Destroy(hearts[i]);
            }
        }

        Invoke("LoadGameOver", 1f);
    }
}