using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //handles points counter

    [SerializeField] private Text PointsCounter;

    private int points = 0;

    public int Points
    {
        get { return points; }
        set
        {
            points = Mathf.Max(0, value);

            if (OnPointsChanged != null)
                OnPointsChanged.Invoke(points);
        }
    }

    public event System.Action<int> OnPointsChanged;

    private void Awake()
    {
        Points = 0;

        FindObjectOfType<GameManager>().OnPointsChanged += Points =>
        {
            PointsCounter.text = Points.ToString();
        };
    }
}
