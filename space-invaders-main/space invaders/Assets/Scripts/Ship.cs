using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    //handles ships movement shooting and lives

    [SerializeField] private float speed;
    [SerializeField] private float ShootingDuration = 0.25f;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip LaserSound;
    [SerializeField] private GameObject[] lives = new GameObject[3];

    private float LastShootTime = 0f;
    private float objectWidth;
    private float objectHeight;
    private Vector2 screenBounds;

    private int LivesAmount = 3;
    private bool movingLeft = false;
    private bool movingRight = false;
    private bool shooting = false;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<MeshRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<MeshRenderer>().bounds.size.y / 2;
    }

    void Update()
    {
        switch (LivesAmount)
        {
            case -1:
                SceneManager.LoadScene("GameOver");
                break;
            case 0:
                lives[0].SetActive(false);
                break;
            case 1:
                lives[1].SetActive(false);
                break;
            case 2:
                lives[2].SetActive(false);
                break;
        }

        if (movingLeft == true)
            transform.position += Vector3.left * speed * Time.deltaTime;

        if (movingRight == true)
            transform.position += Vector3.right * speed * Time.deltaTime;

        if (shooting == true)
        {
            if (!CanShoot())
                return;

            Shoot();
            LastShootTime = Time.timeSinceLevelLoad;
        }

        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWidth, screenBounds.x * -1 - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight);
        transform.position = viewPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        var bullet = obj.GetComponent<EnemyBullet>();

        if (bullet == null)
            return;

        Destroy(obj);
        LivesAmount -= 1;
    }

    private void Shoot()
    {
        var bullet = Instantiate(Bullet, transform.position + Vector3.up * 1f, Quaternion.identity);
        audioSource.clip = LaserSound;
        audioSource.Play();
    }

    private bool CanShoot()
    {
        return (Time.timeSinceLevelLoad - LastShootTime >= ShootingDuration);
    }

    public void ShootingButtonDown()
    {
        shooting = true;
    }

    public void ShootingButtonUp()
    {
        shooting = false;
    }

    public void PointerDownLeft()
    {
        movingLeft = true;
    }

    public void PointerUpLeft()
    {
        movingLeft = false;
    }

    public void PointerDownRight()
    {
        movingRight = true;
    }

    public void PointerUpRight()
    {
        movingRight = false;
    }
}
