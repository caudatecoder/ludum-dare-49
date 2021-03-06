using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using System;
using System.Collections;

public class Destabilizer : MonoBehaviour
{
    public Light2D globalLight;
    public AudioSource alarmSound;

    public Text systemFailureTimer;
    public DialogCanvas dialogCanvas;

    public float systemFailureMinutes = 2;
    public PlayerLife player;

    public AudioSource backgroundMusicBefore;
    public AudioSource backgroundMusicAfter;

    private bool isDone = false;
    private DateTime endTime;

    const string timerFormat = @"mm'm'ss\.fff's'";

    public void ResetFailureTimer()
    {
        endTime = DateTime.Now.AddMinutes(systemFailureMinutes);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDone)
            return;

        if (collision.gameObject.tag == "Player")
        {

            alarmSound.Play();
            globalLight.intensity = 0.3f;
            globalLight.color = new Color(1, 0.89f, 0.89f);

            StartCoroutine(DestabilizationProcess(collision.gameObject));
        }
    }

    IEnumerator DestabilizationProcess(GameObject player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        backgroundMusicBefore.Stop();

        dialogCanvas.ShowNewDialog("[SYSTEM FAILURE] [MAIN_REACTOR_DESTRUCION_IMMINENT]\nUnstable energy level. Some systems might be inoperative");

        yield return new WaitForSeconds(4);

        isDone = true;
        endTime = DateTime.Now.AddMinutes(systemFailureMinutes);

        backgroundMusicAfter.Play();
        player.GetComponent<PlayerMovement>().enabled = true;
        player.SendMessage("Destabilize");

    }

    private void FixedUpdate()
    {
        if (isDone)
        {
            TimeSpan timeDiff = endTime.Subtract(DateTime.Now);
            systemFailureTimer.text = timeDiff.ToString(timerFormat);

            if(timeDiff.TotalSeconds < 1f)
            {
                player.Die(true);
            }
        }
    }
}
