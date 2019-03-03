using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    public static int round = 1;
    public static int p1W = 0;
    public static int p2W = 0;
    public int levelToLoad;
    public GameObject hunter;
    public GameObject ghost;
    private Animator an;

    public Text p1S;
    public Text p2S;
    public float delay;

    public Text cdUI3;
    public Text cdUI4;
    public float startCD;
    public float endCD;

    public Text cdUI1;
    public Text cdUI2;
    public float countdown;

    private bool cdBool;

    // Start is called before the first frame update
    void Start()
    {
        cdUI1.text = "";
        cdUI2.text = "";
        cdUI3.text = ""; 
        cdUI4.text = "";
        p1S.text = "";
        p2S.text = "";
        an = gameObject.GetComponent<Animator>();
        cdBool = false;
        StartCoroutine(GameLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (cdBool) 
        {
            if (startCD >= 0)
            {
                StandBy();
            }
            else if (countdown >= 0)
            {
                CountDown();
            }
        }
        if (ghost.GetComponent<MeshRenderer>().enabled)
        {
            cdBool = false;
            cdUI1.text = "";
            cdUI2.text = "";
            StopCoroutine(RoundPlay());
            StartCoroutine(Win(1));
        }
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStart());
        yield return StartCoroutine(RoundPlay());
        StartCoroutine(Win(2));
    }

    private IEnumerator Win(int winner)
    {
        if (winner == 1)
        {
            p1W++;
            yield return StartCoroutine(RoundEnd(1));
        }
        else
        {
            p2W++;
            yield return StartCoroutine(RoundEnd(2));
        }

        if (round >= 1)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        else
        {
            an.SetTrigger("FadeOut");
        }
    }

    private IEnumerator RoundStart()
    {
        cdBool = true;
        yield return new WaitForSeconds(startCD);
        cdBool = false;
        cdUI1.text = "";
        cdUI2.text = "";
        cdUI3.text = "";
        cdUI4.text = "";
        StartCoroutine(PopUp("Game Start", p1S));
        StartCoroutine(PopUp("Game Start", p2S));
    }

    private IEnumerator RoundPlay()
    {
        yield return new WaitForSeconds(1);
        cdBool = true;
        yield return new WaitForSeconds(countdown);
        cdBool = false;
    }

    private IEnumerator RoundEnd(int winner)
    {
        cdUI1.text = "";
        cdUI2.text = "";
        if (winner == 1)
        {
            StartCoroutine(PopUp("You Win", p1S));
            StartCoroutine(PopUp("You Lose", p2S));
        }
        else
        {
            StartCoroutine(PopUp("You Win", p2S));
            StartCoroutine(PopUp("You Lose", p1S));
        }
        round--;
        yield return new WaitForSeconds(endCD);
    }

    void StandBy()
    {
        startCD -= Time.deltaTime;
        int tl = (int)(startCD + 1);
        cdUI3.text = tl.ToString();
        cdUI4.text = tl.ToString();
    }

    void CountDown()
    {
        countdown -= Time.deltaTime;
        int tl = (int)(countdown + 1);
        cdUI1.text = tl.ToString();
        cdUI2.text = tl.ToString();
    }

    IEnumerator PopUp(string state, Text text)
    {
        text.text = state;
        yield return new WaitForSeconds(delay);
        text.text = "";
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
