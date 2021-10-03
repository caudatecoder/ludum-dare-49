using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorNextLevel : MonoBehaviour
{
    public Animator sceneTransitionAnimator;

    public void Interact(GameObject _)
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        sceneTransitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(0.5f); // TODO use a global param for this?

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
