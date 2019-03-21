using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    public int levelToLoad;
    public GameObject hunter;
    public GameObject ghost;
    public Image crosshair;
    private Animator an;
    public static bool caught = false;
    public GameObject sealSystem;
    private SealSystem ss;
    private PlayerStatus ps;

    public Text p1S;
    public Text p2S;
    public float delay;
    public Text cdUI3;
    public Text cdUI4;
    public float startCD;
    public float endCD;
    private bool cdBool;
    public Image board;
    public Text result;

    void Start()
    {
        p1S.enabled = false;
        p2S.enabled = false;
        board.enabled = false;
        result.enabled = false;
        an = gameObject.GetComponent<Animator>();
        ss = sealSystem.GetComponent<SealSystem>();
        ps = hunter.GetComponent<PlayerStatus>();
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

        //hunter wins
        if (caught)
        {
            if (ps.invincible)
            {
                StartCoroutine(Win(1));
            }
        }
        //ghost wins
        if (ss.allDestroyed)
        {
            StartCoroutine(Win(2));
        }
    }

    IEnumerator Win(int player)
    {
        DisablePlayers();
        board.enabled = true;
        result.enabled = true;
        if (player == 1)
        {
            result.text = "Hunter Wins";
        }
        else
        {
            result.text = "Ghost Wins";
        }
        yield return new WaitForSeconds(endCD);
        an.SetTrigger("FadeOut");
    }

    private void StandBy()
    {
        startCD -= Time.deltaTime;
        int tl = (int)(startCD + 1);
        cdUI3.text = tl.ToString();
        cdUI4.text = tl.ToString();
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
