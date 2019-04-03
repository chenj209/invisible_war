using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenuController : MonoBehaviour
{
    public GameObject hunterPanel;
    public GameObject ghostPanel;
    public Text text1;
    public Text text2;
    public GameObject blink1;
    public GameObject blink2;
    private bool blinking1;
    private bool blinking2;
    private float timer1;
    private float timer2;

    private bool player1Ready;
    private bool player2Ready;
    GameStateController gsc;

    public GameObject hunter;
    public GameObject ghost;
    public Image crosshair;
    public GameObject sealCounter;
    private void Awake()
    {
        gsc = gameObject.GetComponent<GameStateController>();
        if (gsc.GetRoundNum() == 1)
        {
            gsc.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gsc.GetRoundNum() == 1)
        {
            player1Ready = false;
            player2Ready = false;
            timer1 = 0.5f;
            timer2 = 0.5f;
            blinking1 = true;
            blinking2 = true;
            DisablePlayers();
        }
        else
        {
            hunterPanel.SetActive(false);
            ghostPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gsc.GetRoundNum() == 1)
        {
            // for player1
            if (blinking1)
            {
                timer1 -= Time.deltaTime;
            }
            if (timer1 <= 0)
            {
                blink1.SetActive(!blink1.active);
                timer1 += 0.5f;
            }
            if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("HunterContinue"))
            {
                player1Ready = true;
                if (player2Ready)
                {
                    blinking1 = false;
                    blinking2 = false;
                    blink1.SetActive(false);
                    blink2.SetActive(false);
                    hunterPanel.SetActive(false);
                    ghostPanel.SetActive(false);
                    text1.enabled = false;
                    text2.enabled = false;
                    gsc.enabled = true;
                    sealCounter.SetActive(true);
                }
                else
                {
                    blink1.SetActive(false);
                    text1.text = "Waiting for Player2";
                    blink1 = text1.gameObject;
                }
            }

            // for player2
            if (blinking2)
            {
                timer2 -= Time.deltaTime;
            }
            if (timer2 <= 0)
            {
                blink2.SetActive(!blink2.active);
                timer2 += 0.5f;
            }
            if (Input.GetButtonDown("Continue") || Input.GetKeyDown(KeyCode.L))
            {
                player2Ready = true;
                if (player1Ready)
                {
                    blinking1 = false;
                    blinking2 = false;
                    blink1.SetActive(false);
                    blink2.SetActive(false);
                    hunterPanel.SetActive(false);
                    ghostPanel.SetActive(false);
                    text1.enabled = false;
                    text2.enabled = false;
                    gsc.enabled = true;
                    sealCounter.SetActive(true);
                }
                else
                {
                    blink2.SetActive(false);
                    text2.text = "Waiting for Player1";
                    blink2 = text2.gameObject;
                }
            }
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
}
