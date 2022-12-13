using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //handles changing scenes

    [SerializeField] private Animator animator;

    private string SceneName;

    public void changeScene(string sceneName)
    {
        SceneName = sceneName;
        StartCoroutine(Crossfade());
    }

    IEnumerator Crossfade()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneName);
    }
}
