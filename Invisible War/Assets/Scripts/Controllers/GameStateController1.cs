﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateController1 : MonoBehaviour
{
    public static int round = 3;
    private static int roundNum = 1;
    public int levelToLoad;
    public GameObject hunter;
    public GameObject ghost;
    public Image crosshair;
    private Animator an;
    private bool roundOver;
    public static bool caught = false;

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

    private ScoreController scoreController;
    private PlayerStatus player1;
    private PlayerStatus player2;

    void Start()
    {
        caught = false;
        roundOver = false;
        cdUI1.enabled = false;
        cdUI2.enabled = false;
        cdUI3.enabled = false;
        cdUI4.enabled = false;
        p1S.enabled = false;
        p2S.enabled = false;
        an = gameObject.GetComponent<Animator>();
        scoreController = gameObject.GetComponent<ScoreController>();
        player1 = hunter.GetComponent<PlayerStatus>();
        player2 = ghost.GetComponent<PlayerStatus>();
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
        if (caught && !roundOver)
        {
            StopAllCoroutines();
            PlayerStatus ps1 = hunter.GetComponent<PlayerStatus>();
            if (ps1.invincible)
            {
                StartCoroutine(Win(1));
            }
            else
            {
                StartCoroutine(Win(2));
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (scoreController.goToNext)
            {
                an.SetTrigger("FadeOut");
            }
        }

        // ghost gets 20 if hunter is freezed
        if (player1.getfreezed)
        {
            scoreController.FreezePoint();
            player1.getfreezed = false;
        }
        //hunter gets 20 if ghost is hit
        if (player2.gethit)
        {
            scoreController.HitPoint();
            player2.gethit = false;
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
        roundOver = true;
        if (winner == 1)
        {
            cdBool = false;
            cdUI1.enabled = false;
            cdUI2.enabled = false;
            scoreController.Win(1);
            yield return StartCoroutine(RoundEnd(1));
        }
        else
        {
            scoreController.Win(2);
            yield return StartCoroutine(RoundEnd(2));
        }

        if (round >= 1)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        else
        {
            scoreController.displayScoreBoard();
        }
    }

    private IEnumerator RoundStart()
    {
        cdUI3.enabled = true;
        cdUI4.enabled = true;
        DisablePlayers();
        cdBool = true;
        yield return new WaitForSeconds(startCD-0.1f);
        cdBool = false;
        cdUI3.enabled = false;
        cdUI4.enabled = false;
        string roundName = "Round " + roundNum;
        StartCoroutine(PopUp(roundName, p1S));
        StartCoroutine(PopUp(roundName, p2S));
    }

    private IEnumerator RoundPlay()
    {
        yield return new WaitForSeconds(delay);
        cdUI1.enabled = true;
        cdUI2.enabled = true;
        ActivePlayers();
        cdBool = true;
        yield return new WaitForSeconds(countdown);
        cdBool = false;
        cdUI1.enabled = false;
        cdUI2.enabled = false;
    }

    private IEnumerator RoundEnd(int winner)
    {
        DisablePlayers();
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
        roundNum++;
        yield return new WaitForSeconds(delay);
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
        int min = tl / 60;
        int sec = tl % 60;
        if (sec >= 10)
        {
            cdUI1.text = min.ToString() + ":" + sec.ToString();
            cdUI2.text = min.ToString() + ":" + sec.ToString();
        }
        else
        {
            cdUI1.text = min.ToString() + ":" + "0" + sec.ToString();
            cdUI2.text = min.ToString() + ":" + "0" + sec.ToString();
        }
    }

    private void DisablePlayers()
    {
        crosshair.enabled = false;
        hunter.GetComponent<PlayerControl>().enabled = false;
        hunter.GetComponent<shooting>().enabled = false;
        ghost.GetComponent<PlayerControl>().enabled = false;
        ghost.GetComponent<Freeze>().enabled = false;
    }

    private void ActivePlayers()
    {
        crosshair.enabled = true;
        hunter.GetComponent<PlayerControl>().enabled = true;
        hunter.GetComponent<shooting>().enabled = true;
        ghost.GetComponent<PlayerControl>().enabled = true;
        ghost.GetComponent<Freeze>().enabled = true;
    }

    IEnumerator PopUp(string state, Text text)
    {
        text.text = state;
        text.enabled = true;
        yield return new WaitForSeconds(delay);
        text.enabled = false;
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
