using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    public static bool HunterTutOne = false;
    public static bool HunterTutTwo = false;
    public static bool HunterTutThree = false;
    public static bool GhostTutOne = false;
    public static bool GhostTutTwo = false;
    public static bool GhostTutThree = false;
    public static bool HunterTutDone = false;
    public static bool GhostTutDone = false;

    public Text p1S;
    public Text p2S;
    public float delay;

    private string player1State;
    private string player2State;
    private bool isBlinking;

    //public float timeLeft;
    //public float startCD;
    //public Text cdUI1;
    //public Text cdUI2;
    //public Text cdUI3;
    //public Text cdUI4;

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
        //cdUI1.text = "";
        //cdUI2.text = "";
        //cdUI3.text = "";
        //cdUI4.text = "";
        //player1State = "start menu";
        //player2State = "start menu";
    }

    // Update is called once per frame
    void Update()
    {
        /*if (startCD >= 0)
        {
            GameStart();
        }
        if (timeLeft >= 0)
        {
            CountDown();
        }*/

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
    }

    /*void GameStart()
    {
        if (player1State == "stand by" && player2State == "stand by")
        {
            startCD -= Time.deltaTime;
            int tl = (int)(startCD + 1);
            cdUI3.text = tl.ToString();
            cdUI4.text = tl.ToString();
            if (startCD < 0)
            {
                cdUI3.text = "";
                cdUI4.text = "";
                StartCoroutine(PopUp("Game Start", p1S));
                StartCoroutine(PopUp("Game Start", p2S));
                player1State = "Game";
                player2State = "Game";
            }
        }
    }

    void CountDown()
    {
        if (player1State == "Game" && player2State == "Game")
        {
            timeLeft -= Time.deltaTime;
            int tl = (int)(timeLeft + 1);
            cdUI1.text = tl.ToString();
            cdUI2.text = tl.ToString();
            if (timeLeft < 0)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        StartCoroutine(PopUp("Game Over", p1S));
        StartCoroutine(PopUp("Game Over", p2S));
        player1State = "Game Over";
        player2State = "Game Over";
        cdUI1.text = "";
        cdUI2.text = "";
    }*/
}
