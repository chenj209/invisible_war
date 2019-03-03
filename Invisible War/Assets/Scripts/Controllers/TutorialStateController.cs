﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialStateController : MonoBehaviour
{
    public static bool HunterTutOne = false;
    public static bool HunterTutTwo = false;
    public static bool HunterTutThree = false;
    public static bool HunterTutFour = false;
    public static bool GhostTutOne = false;
    public static bool GhostTutTwo = false;
    public static bool GhostTutThree = false;
    public static bool HunterTutDone = false;
    public static bool GhostTutDone = false;
    public static bool HunterCatchDone = false;
    public static bool HunterShootDone = false;
    public static bool GhostFreezeDone = false;

    public Text p1S;
    public Text p2S;
    public float delay;

    public int levelToLoad;

    private string player1State;
    private string player2State;
    private bool isBlinking;

    private Animator an;

    // Start is called before the first frame update
    void Start()
    {
        p1S.text = "";
        p2S.text = "";
        StartCoroutine(PopUp("Enter Tutorial1", p1S));
        player1State = "tutorial1";
        StartCoroutine(PopUp("Enter Tutorial1", p2S));
        player2State = "tutorial1";
        isBlinking = true;
        an = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // for player1
        if (player1State == "tutorial1" && HunterTutOne)
        { 
            StartCoroutine(PopUp("Enter Tutorial2", p1S));
            player1State = "tutorial2";
        }
        else if (player1State == "tutorial2" && HunterTutTwo)
        { 
            StartCoroutine(PopUp("Enter Tutorial3", p1S));
            player1State = "tutorial3";
        }
        else if (player1State == "tutorial3" && HunterTutThree)
        {
            StartCoroutine(PopUp("Enter Tutorial4", p1S));
            player1State = "tutorial4";
        }
        else if (player1State == "tutorial4" && HunterTutFour)
        {
            player1State = "stand by";
            if (player2State != "stand by")
            {
                StartCoroutine(Flash("Waiting for Player2", p1S));
            }
            else
            {
                isBlinking = false;
            }
        }

        // for player2
            if (player2State == "tutorial1" && GhostTutOne)
            {
                StartCoroutine(PopUp("Enter Tutorial2", p2S));
                player2State = "tutorial2";
            }
            else if (player2State == "tutorial2" && GhostTutTwo)
            {
                StartCoroutine(PopUp("Enter Tutorial3", p2S));
                player2State = "tutorial3";
            }
            else if (player2State == "tutorial3" && GhostTutThree)
            {
                player2State = "stand by";
                if (player1State != "stand by")
                {
                    StartCoroutine(Flash("Waiting for Player1", p2S));
                }
                else
                {
                    isBlinking = false;
                }
        }
        
    }

    IEnumerator PopUp(string state, Text text)
    {
        text.text = state;
        yield return new WaitForSeconds(delay);
        text.text = "";
    }

    IEnumerator Flash(string state, Text text)
    {
        text.text = state;
        while (isBlinking) 
        {
            text.enabled = false;
            yield return new WaitForSeconds(0.5f);
            text.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        an.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}