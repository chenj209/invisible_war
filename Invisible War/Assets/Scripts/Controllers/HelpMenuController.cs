using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenuController : MonoBehaviour
{
    public GameObject hunterPanel;
    public GameObject ghostPanel;
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

    // Start is called before the first frame update
    void Start()
    {
        player1Ready = false;
        player2Ready = false;
        timer1 = 0.5f;
        timer2 = 0.5f;
        blinking1 = true;
        blinking2 = true;
        gsc = gameObject.GetComponent<GameStateController>();
        DisablePlayers();
    }

    // Update is called once per frame
    void Update()
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
        if (Input.GetKeyDown(KeyCode.R))
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
                gsc.enabled = true;
            }
            else
            {
                blink1.transform.GetChild(0).gameObject.SetActive(false);
                blink1.GetComponent<Text>().text = "Waiting for Player2";
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
        if (Input.GetButtonDown("Continue"))
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
                gsc.enabled = true;
            }
            else
            {
                blink2.transform.GetChild(0).gameObject.SetActive(false);
                blink2.GetComponent<Text>().text = "Waiting for Player1";
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
