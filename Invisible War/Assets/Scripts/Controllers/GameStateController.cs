﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    public static int round = 3;
    private static int roundNum = 1;
    public int levelToLoad;
    public GameObject hunter;
    public GameObject ghost;
    public GameObject sealSystem;
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

    private bool cdBool;

    public Image scoreBoard;
    public Text name;
    public Text Player1Score;
    public Text Player2Score;
    public float scoreCD;
    public static int p1w;
    public static int p2w;

    void Start()
    {
        scoreBoard.enabled = false;
        name.enabled = false;
        Player1Score.enabled = false;
        Player2Score.enabled = false;
        caught = false;
        roundOver = false;
        cdUI3.enabled = true;
        cdUI4.enabled = true;
        p1S.enabled = false;
        p2S.enabled = false;
        an = gameObject.GetComponent<Animator>();
        cdBool = true;
        DisablePlayers();
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
            else
            {
                Game();
            }
        }
        if (!roundOver)
        {
            PlayerStatus ps1 = hunter.GetComponent<PlayerStatus>();
            SealSystem ss = sealSystem.GetComponent<SealSystem>();
            if (caught && ps1.invincible)
            {
                StopAllCoroutines();
                StartCoroutine(Win(1));
            }
            if (ss.allDestroyed)
            {
                StopAllCoroutines();
                StartCoroutine(Win(2));
            }
        }
    }

    private void Game()
    {
        ActivePlayers();
        cdUI3.enabled = false;
        cdUI4.enabled = false;
        cdBool = false;
        StartCoroutine(PopUp("Game Start", p1S));
        StartCoroutine(PopUp("Game Start", p2S));
    }

    IEnumerator Win(int winner)
    {
        DisablePlayers();
        roundOver = true;
        if (winner == 1)
        {
            p1w++;
            StartCoroutine(PopUp("You Win", p1S));
            StartCoroutine(PopUp("You Lose", p2S));
        }
        else
        {
            p2w++;
            StartCoroutine(PopUp("You Win", p2S));
            StartCoroutine(PopUp("You Lose", p1S));
        }
        round--;
        roundNum++;
        yield return new WaitForSeconds(delay);

        if (round >= 1)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        else
        {
            DisplayScore();
            yield return new WaitForSeconds(scoreCD);
            an.SetTrigger("FadeOut");
        }
    }

    private void DisplayScore()
    {
        scoreBoard.enabled = true;
        name.enabled = true;
        Player1Score.enabled = true;
        Player2Score.enabled = true;
        Player1Score.text = p1w.ToString();
        Player2Score.text = p2w.ToString();
    }

    void StandBy()
    {
        startCD -= Time.deltaTime;
        int tl = (int)(startCD + 1);
        cdUI3.text = tl.ToString();
        cdUI4.text = tl.ToString();
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
