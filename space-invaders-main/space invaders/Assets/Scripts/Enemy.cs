using UnityEngine;

public class Enemy : MonoBehaviour
{
    //handles the enemies logic such as generating particles and collisions

    [SerializeField] private GameObject ParticleEffect;
    [SerializeField] private EnemiesGrid enemiesGrid;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        enemiesGrid = FindObjectOfType<EnemiesGrid>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        var bullet = obj.GetComponent<Bullet>();
        var ship = obj.GetComponent<Ship>();

        if (bullet != null)
        {
            GenerateParticles(ParticleEffect, transform.position);
            gameManager.Points += 10;
            Destroy(obj);
            Destroy(gameObject);
        }

        if (ship != null)
            enemiesGrid.GameOver();

        if (collision.gameObject.tag == "BoundaryBottom")
            enemiesGrid.GameOver();

        if (collision.gameObject.tag == "BoundaryLeft")
            enemiesGrid.EnemyCollisionLeft();

        if (collision.gameObject.tag == "BoundaryRight")
            enemiesGrid.EnemyCollisionRight();
    }

    private void GenerateParticles(GameObject prefab, Vector3 position)
    {
        var particles = Instantiate(prefab, position, Quaternion.identity);
    }
}
