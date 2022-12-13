using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemiesGrid : MonoBehaviour
{
    //spawns enemies in a grid and handles their movement

    [Header("Animations")]
    [SerializeField] private Animator animator;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip LaserSound;

    [Header("Grid Atributes")]
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 11;
    [SerializeField] private float SpaceBetweenInvadersHeight = 13;
    [SerializeField] private float SpaceBetweenInvadersWidth = 13;

    [Header("Enemies Atributes")]
    [SerializeField] private GameObject EnemyBullet;
    [SerializeField] private GameObject[] TypesOfEnemies;
    [SerializeField] private Transform parent;
    [SerializeField] private int randomEnemy;
    [SerializeField] private int enemyMovementDirection = 0;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float loweringTime = 2;
    [SerializeField] private float time1;
    [SerializeField] private float time2;
    [SerializeField] private new Rigidbody2D rigidbody;

    private Transform[] enemies;

    void Awake()
    {
        time2 = Random.Range(2, 5);

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 pos = new Vector3(x*SpaceBetweenInvadersWidth, y*SpaceBetweenInvadersHeight, 0);
                Instantiate(TypesOfEnemies[y], pos, Quaternion.identity, parent);
            }
        }

        parent.transform.position = new Vector3(-43f, 20f, 0);
    }

    void Update()
    {
        Transform[] enemies = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            enemies[i] = transform.GetChild(i);
        }

        if (time2 > 0)
            time2 -= Time.deltaTime;
        else if (time2 <= 0)
        {
            randomEnemy = Random.Range(0, enemies.Length);
            Instantiate(EnemyBullet, enemies[randomEnemy].transform.position, Quaternion.identity);

            audioSource.clip = LaserSound;
            audioSource.Play();

            time2 = Random.Range(2, 5);
        }

        if (enemies.Length == 0)
            StartCoroutine(CrossfadeVictory());

        switch (enemyMovementDirection)
            {
                case 0:
                    rigidbody.velocity = transform.rotation * Vector3.right * speed;
                    break;
                case 1:
                    Left();
                    break;
                case 2:
                    Right();
                    break;
            }
    }

    public void EnemyCollisionLeft()
    {
        time1 = loweringTime;
        enemyMovementDirection = 1;
    }

    public void EnemyCollisionRight()
    {
        time1 = loweringTime;
        enemyMovementDirection = 2;
    }

    void Right()
    {
        Stop();
        rigidbody.velocity = transform.rotation * Vector3.down * speed;
        if (time1 > 0)
            time1 -= Time.deltaTime;
        else if (time1 <= 0)
        {
            Stop();
            rigidbody.velocity = transform.rotation * Vector3.left * speed;
        }
    }

    void Left()
    {
        Stop();
        rigidbody.velocity = transform.rotation * Vector3.down * speed;
        if (time1 > 0)
            time1 -= Time.deltaTime;
        else if (time1 <= 0)
        {
            Stop();
            rigidbody.velocity = transform.rotation * Vector3.right * speed;
        }
    }

    void Stop()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = 0;
        rigidbody.Sleep();
    }

    public void GameOver()
    {
        StartCoroutine(CrossfadeGameOver());
    }

    IEnumerator CrossfadeVictory()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Victory");
    }

    IEnumerator CrossfadeGameOver()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }
}
