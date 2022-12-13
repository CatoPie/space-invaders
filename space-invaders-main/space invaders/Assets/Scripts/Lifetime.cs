using UnityEngine;

public class Lifetime : MonoBehaviour
{
    //destroyes projectiles after cartain amount of time have passed

    [SerializeField] private float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}