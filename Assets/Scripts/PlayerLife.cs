using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public GameObject lastCheckpoint;
    public Animator sceneTransitionAnimator;
    public float transitionDuration = .5f;

    public void Die(bool forceRestart = false)
    {
        GetComponent<PlayerMovement>().enabled = false;

        if (lastCheckpoint != null && !forceRestart)
        {
            transform.position = lastCheckpoint.transform.position;
            StartCoroutine(ReloadCheckpoint());
        }
        else
        {
            StartCoroutine(ReloadScene());
        }
    }

    public void SetCheckpoint(GameObject checkpoint)
    {
        lastCheckpoint = checkpoint;
    }

    IEnumerator ReloadScene()
    {
        sceneTransitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ReloadCheckpoint()
    {
        sceneTransitionAnimator.SetTrigger("Start");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(transitionDuration);

        sceneTransitionAnimator.SetTrigger("Restart");
        GetComponent<PlayerMovement>().enabled = true;
    }
}
 