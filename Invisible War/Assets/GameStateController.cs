using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    public float timeLeft;
    public float startCD;
    public Text cdUI1;
    public Text cdUI2;
    public Text cdUI3;
    public Text cdUI4;
    public Text p1S;
    public Text p2S;
    public float delay;

    private string player1State;
    private string player2State;

    // Start is called before the first frame update
    void Start()
    {
        cdUI1.text = "";
        cdUI2.text = "";
        cdUI3.text = "";
        cdUI4.text = "";
        p1S.text = "";
        p2S.text = "";
        player1State = "start menu";
        player2State = "start menu";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))// for player1
        {
            if (player1State == "start menu")
            {
                StartCoroutine(PopUp("Enter Tutorial1", p1S));
                player1State = "tutorial1";
                EnterTutorial1(1);
            }
            else if (player1State == "tutorial1")
            {
                StartCoroutine(PopUp("Enter Tutorial2", p1S));
                player1State = "tutorial2";
                EnterTutorial2(1);
            }
            else if (player1State == "tutorial2")
            {
                player1State = "stand by";
                if (player2State != "stand by")
                {
                    StartCoroutine(PopUp("Stand By", p1S));
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.H))// for player2
        {
            if (player2State == "start menu")
            {
                StartCoroutine(PopUp("Enter Tutorial1", p2S));
                player2State = "tutorial1";
                EnterTutorial1(2);
            }
            else if (player2State == "tutorial1")
            {
                StartCoroutine(PopUp("Enter Tutorial2", p2S));
                player2State = "tutorial2";
                EnterTutorial2(2);
            }
            else if (player2State == "tutorial2")
            {
                player2State = "stand by";
                if (player1State != "stand by")
                {
                    StartCoroutine(PopUp("Stand By", p2S));
                }
            }
        }
        if (startCD >= 0)
        {
            GameStart();
        }
        if (timeLeft >= 0)
        {
            CountDown();
        }
    }

    void EnterTutorial1(int player)
    {

    }

    void EnterTutorial2(int player)
    {

    }

    void GameStart()
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
    }

    IEnumerator PopUp(string state, Text text)
    {
        text.text = state;
        yield return new WaitForSeconds(delay);
        text.text = "";
    }
}
