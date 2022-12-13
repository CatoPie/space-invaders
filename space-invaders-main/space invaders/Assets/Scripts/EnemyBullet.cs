using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //handles enemy bullets movement

    [SerializeField] private float speed = 200;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.rotation * Vector3.down * speed;
    }
}
