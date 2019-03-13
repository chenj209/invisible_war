using System.Collections;
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

    public GameObject hunter;
    public GameObject ghost;
    public Canvas canvas;
    public Canvas tutorialCanvas;
    public Image background1;
    public Image background2;
    public Text p1R;
    public Text p2R;
    public Text blink1;
    public Text blink2;
    public Image crosshair;

    public Text p1S;
    public Text p2S;
    public float delay;

    public int levelToLoad;

    private string player1State;
    private string player2State;
    private bool isBlinking;
    private bool blinking1;
    private bool blinking2;
    private float timer1;
    private float timer2;

    private Animator an;

    // Start is called before the first frame update
    void Start()
    {
        p1S.text = "";
        p2S.text = "";
        p1R.text = "";
        p2R.text = "";
        timer1 = 0.5f;
        timer2 = 0.5f;
        isBlinking = true;
        blinking1 = true;
        blinking2 = true;
        an = gameObject.GetComponent<Animator>();
        player1State = "rule1";
        player2State = "rule1";
        p1R.text = "Welcome to Invisible Tag!";
        p2R.text = "Welcome to Invisible Tag!";
        DisablePlayers();
        HideTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        // for blink1
        if (blinking1)
        {
            timer1 -= Time.deltaTime;
        }
        if (timer1 <= 0)
        {
            blink1.enabled = !blink1.enabled;
            timer1 += 0.5f;
        }

        // for blink2
        if (blinking2)
        {
            timer2 -= Time.deltaTime;
        }
        if (timer2 <= 0)
        {
            blink2.enabled = !blink2.enabled;
            timer2 += 0.5f;
        }

        // for player1
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (player1State == "rule1")
            {
                p1R.text = "This is a player versus player game." +
                    "Your goal is to catch the invisible ghost " +
                    "controlled by the other player in the arena " +
                    "within two minutes time limit.";
                player1State = "rule2";
            }
            else if (player1State == "rule2")
            {
                p1R.text = "There are total three rounds and the player " +
                    "who wins the most rounds will win the game.";
                player1State = "rule3";
            }
            else if (player1State == "rule3")
            {
                p1R.text = "Now lets start with teaching you the basic machanics in the game.";
                player1State = "rule4";
            }
            else if (player1State == "rule4")
            {
                StartTutorial(1);
            }
        }
        else if (player1State == "tutorial1" && HunterTutOne)
        {
            StartCoroutine(PopUp("Enter Tutorial2", p1S));
            player1State = "tutorial2";
        }
        else if (player1State == "tutorial2" && HunterTutTwo)
        {
            crosshair.enabled = true;
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
        if (Input.GetButtonDown("Continue") || Input.GetKey(KeyCode.O))
        {
            if (player2State == "rule1")
            {
                p2R.text = "This is a player versus player game." +
                    "Your goal is to avoid being caught by the invisible hunter " +
                    "controlled by the other player in the arena " +
                    "for two minutes.";
                player2State = "rule2";
            }
            else if (player2State == "rule2")
            {
                p2R.text = "There are total three rounds and the player " +
                    "who wins the most rounds will win the game.";
                player2State = "rule3";
            }
            else if (player2State == "rule3")
            {
                p2R.text = "Now lets start with teaching you the basic machanics in the game.";
                player2State = "rule4";
            }
            else if (player2State == "rule4")
            {
                StartTutorial(2);
            }
        }
        else if (player2State == "tutorial1" && GhostTutOne)
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

    private void HideTutorial()
    {
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        tutorialCanvas.transform.GetChild(0).gameObject.SetActive(false);
        tutorialCanvas.transform.GetChild(1).gameObject.SetActive(false);
    }

    private void ShowTutorial(int player)
    {
        if (player == 1)
        {
            canvas.transform.GetChild(1).gameObject.SetActive(true);
            tutorialCanvas.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (player == 2)
        {
            canvas.transform.GetChild(0).gameObject.SetActive(true);
            tutorialCanvas.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void DisablePlayers()
    {
        hunter.GetComponent<PlayerControl>().enabled = false;
        hunter.GetComponent<shooting>().enabled = false;
        //hunter.GetComponent<CatchPlayer>().enabled = false;
        ghost.GetComponent<PlayerControl>().enabled = false;
        ghost.GetComponent<Freeze>().enabled = false;
    }

    private void ActivePlayer(int player)
    {
        if (player == 1)
        {
            hunter.GetComponent<PlayerControl>().enabled = true;
            //hunter.GetComponent<shooting>().enabled = true;
            //hunter.GetComponent<CatchPlayer>().enabled = true;
        }
        else if (player == 2)
        {
            ghost.GetComponent<PlayerControl>().enabled = true;
            //ghost.GetComponent<Freeze>().enabled = true;
        }
    }

    void StartTutorial(int player)
    {
        ShowTutorial(player);
        ActivePlayer(player);
        if (player == 1)
        {
            blinking1 = false;
            blink1.enabled = false;
            background1.enabled = false;
            p1R.enabled = false;
            StartCoroutine(PopUp("Enter Tutorial1", p1S));
            player1State = "tutorial1";
        }
        else if (player == 2)
        {
            blinking2 = false;
            blink2.enabled = false;
            background2.enabled = false;
            p2R.enabled = false;
            StartCoroutine(PopUp("Enter Tutorial1", p2S));
            player2State = "tutorial1";
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
