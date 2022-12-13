using UnityEngine;

public class UFOspawner : MonoBehaviour
{
    //responsible for spawning UFOs

    [SerializeField] private GameObject ufo;
    [SerializeField] private GameManager gameManager;

    private float time;

    void Start()
    {
        time = Random.Range(8, 10);
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("UFO") != null)
        {
            if (time > 0)
                time -= Time.deltaTime;
            if (time <= 0)
            {
                Instantiate(ufo, transform.position, Quaternion.identity);
                time = Random.Range(10, 15);
            }
        }
    }
}
