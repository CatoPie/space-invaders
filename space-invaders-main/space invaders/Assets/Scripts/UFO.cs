using UnityEngine;

public class UFO : MonoBehaviour
{
    //handles UFO's movement and health

    [SerializeField] private float speed = 70;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject ParticleEffect;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        GetComponent<Rigidbody2D>().velocity = transform.rotation * Vector3.left * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        var bullet = obj.GetComponent<Bullet>();

        if (bullet != null)
        {
            GenerateParticles(ParticleEffect, transform.position);
            gameManager.Points += 100;
            Destroy(obj);
            Destroy(gameObject);
        }
    }

    private void GenerateParticles(GameObject prefab, Vector3 position)
    {
        var particles = Instantiate(prefab, position, Quaternion.identity);
    }
}
