using UnityEngine;

public class Bullet : MonoBehaviour
{
    //handles bullets movement

    [SerializeField] private float speed = 200;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.rotation * Vector3.up * speed;
    }
}
