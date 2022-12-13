using UnityEngine;

public class BunkerFragment : MonoBehaviour
{
    //handles collisions of bunker fragments

    [SerializeField] private GameObject ParticleEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        var bullet = obj.GetComponent<Bullet>();
        var enemyByllet = obj.GetComponent<EnemyBullet>();
        var enemy = obj.GetComponent<Enemy>();

        if (bullet != null)
        {
            GenerateParticles(ParticleEffect, transform.position);
            Destroy(obj);
            Destroy(gameObject);
        }

        if (enemyByllet != null)
        {
            GenerateParticles(ParticleEffect, transform.position);
            Destroy(obj);
            Destroy(gameObject);
        }

        if (enemy != null)
        {
            GenerateParticles(ParticleEffect, transform.position);
            Destroy(gameObject);
        }
    }

    private void GenerateParticles(GameObject prefab, Vector3 position)
    {
        var particles = Instantiate(prefab, position, Quaternion.identity);
    }
}
