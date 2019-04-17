using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryLineController : MonoBehaviour
{
    public Text storyline1;
    public Text storyline2;
    public Text storyline3;
    public Text storyline4;
    public Image background;
    public float duration;
    public Image startButton;
    public GameObject startPage;
    public GameObject HunterTutPage;
    public GameObject GhostTutPage;

    private GameStateController gsc;
    private bool shown;
    private float timer = 0f;
    private bool storylineBegin;
    private bool firstime = true;
  
    void Awake()
    {
        gsc = GetComponent<GameStateController>();
        if (gsc.GetRoundNum() == 1)
        {
            shown = false;
            gsc.gamestart = false;
            background.enabled = true;
            storyline1.enabled = true;
            storyline2.enabled = true;
            storyline3.enabled = true;
            storyline4.enabled = true;
            storylineBegin = false;
        }
        else
        {
            startPage.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gsc.GetRoundNum() == 1 && !HunterTutPage.activeSelf)
        {
            if (Input.GetButtonDown("HunterStart") || Input.GetButtonDown("Start"))
            {
                storylineBegin = true;
                startPage.SetActive(false);
            }
            if (!storylineBegin)
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0)
            {
                startButton.enabled = !startButton.enabled;
                timer += 0.5f;
            }

            if (!shown)
            {
                if (storylineBegin)
                {
                    StartCoroutine(StoryIn());
                }

            }
            else
            {
                StartCoroutine(StoryOut());
            }
        }
    }

    IEnumerator StoryIn()
    {
        yield return new WaitForSeconds(2);

        if (storyline1.color.a < 1)
        {
            storyline1.color = new Color(storyline1.color.r, storyline1.color.b, storyline1.color.g, storyline1.color.a + Time.deltaTime / duration);
        }
        else if (storyline2.color.a < 1)
        {
            storyline2.color = new Color(storyline2.color.r, storyline2.color.b, storyline2.color.g, storyline2.color.a + Time.deltaTime / duration);
        }
        else if (storyline3.color.a < 1)
        {
            storyline3.color = new Color(storyline3.color.r, storyline3.color.b, storyline3.color.g, storyline3.color.a + Time.deltaTime / duration);
        }
        else if (storyline4.color.a < 1)
        {
            storyline4.color = new Color(storyline4.color.r, storyline4.color.b, storyline4.color.g, storyline4.color.a + Time.deltaTime / duration);
        }
        else
        {
            shown = true;
        }
        yield return null;
    }

    IEnumerator StoryOut()
    {
        yield return new WaitForSeconds(duration);

        if (storyline1.color.a > 0)
        {
            storyline1.color = new Color(storyline1.color.r, storyline1.color.b, storyline1.color.g, storyline1.color.a - Time.deltaTime / duration * 2);
        }
        if (storyline2.color.a > 0)
        {
            storyline2.color = new Color(storyline2.color.r, storyline2.color.b, storyline2.color.g, storyline2.color.a - Time.deltaTime / duration * 2);
        }
        if (storyline3.color.a > 0)
        {
            storyline3.color = new Color(storyline3.color.r, storyline3.color.b, storyline3.color.g, storyline3.color.a - Time.deltaTime / duration * 2);
        }
        if (storyline4.color.a > 0)
        {
            storyline4.color = new Color(storyline4.color.r, storyline4.color.b, storyline4.color.g, storyline4.color.a - Time.deltaTime / duration * 2);
        }
        else
        {
            if (firstime)
            {
                background.enabled = false;
                HunterTutPage.SetActive(true);
                GhostTutPage.SetActive(true);
                firstime = false;
            }
        }
    }
}
